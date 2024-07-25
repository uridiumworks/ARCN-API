
using ARCN.API.EntityDataModels;
using ARCN.Application;
using ARCN.Application.Interfaces.Services;
using ARCN.Application.Interfaces;
using ARCN.Infrastructure;
using ARCN.Infrastructure.Services.ApplicationServices;
using ARCN.Repository;
using ARCN.Repository.Database;
using Serilog;
using Microsoft.AspNetCore.Identity;
using ARCN.Domain.Commons.Authorization;
using System.Reflection;
using ARCN.API.Permissions;

namespace ARCN.API.Extensions
{
    public static class WebApplicationExtension
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            try
            {
                #region CORs
                builder.Services.AddCors(opt =>
                {
                    opt.AddPolicy(name: "allowAllOrigins", policy =>
                    {
                        policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
                });

                #endregion

                #region OdataConfig
                var batchHandler = new DefaultODataBatchHandler();
                batchHandler.MessageQuotas.MaxNestingDepth = 3;
                batchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;

                IEdmModel edmModel = new ARCNEntityDataModel().GetEntityDataModel();
                builder.Services.AddControllers()
                     .AddOData(opt =>
                    opt
                    .AddRouteComponents("customer/odata", edmModel, batchHandler)
                    .Select()
                    .Expand()
                    .OrderBy()
                    .SetMaxTop(100)
                    .Count()
                    .Filter()).AddJsonOptions(x =>
                    {
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });

                #endregion

                #region Swagger
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c => c.ResolveConflictingActions(apides => apides.First()));
                builder.Services.AddSwaggerGen(c =>
                {
                    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        Description = "Input your Bearer Token in this format - Bearer {your token} to access the api"

                    });

                    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        }, new List<string>()
                    }

                });

                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "ARCN API",
                        License = new OpenApiLicense
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
                

                });
                #endregion


                #region Other service Registration
                builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<ARCNDbContext>()
                   .AddDefaultTokenProviders();

                builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
                builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

                builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
                builder.Services.AddScoped<IUserprofileService, UserprofileService>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<ICloudinaryFileUploadService, CloudinaryFileUploadService>();
                builder.Services.AddScoped<ITokenService, TokenService>();
                builder.Services.AddScoped<IUserIdentityService, UserIdentityService>();
                builder.Services.AddScoped<IStaffRegistrationService, StaffRegistrationService>();

                builder.Services.AddInfrastructureServices(builder.Configuration);
                builder.Services.AddApplicationServices();
                builder.Services.AddRepositoryServices(builder.Configuration);
                builder.Services.AddRepositories();
                builder.Services.AddDistributedMemoryCache();

   

                #endregion

                #region Authentication and Authorization scheme

                builder.Services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(30);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });

                builder.Services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //opt.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                 .AddJwtBearer(opt =>
                 {
                     opt.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                         ValidAudience = builder.Configuration["JwtConfig:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = false,
                         ValidateIssuerSigningKey = false,
                         RoleClaimType = ClaimTypes.Role
                     };
                 }).AddCookie();

                builder.Services.AddAuthorization(options =>
                {
                    foreach (var prop in typeof(AppPermissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
                    {
                        var propertyValue = prop.GetValue(null);
                        if (propertyValue is not null)
                        {
                            options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(AppClaim.Permission, propertyValue.ToString()));
                        }
                    }
                    var authPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                    authPolicyBuilder = authPolicyBuilder.RequireAuthenticatedUser();
                    options.DefaultPolicy = authPolicyBuilder.Build();
                });

                #endregion

                return builder.Build();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpLogging();
            app.UseSerilogRequestLogging();

            app.UseODataBatching();
            app.UseRouting();
            app.UseCors("allowAllOrigins");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllers().RequireAuthorization();


            return app;
        }
        internal static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var seeders = serviceScope.ServiceProvider.GetServices<ARCNDbSeeder>();

            foreach (var seed in seeders)
            {
                seed.SeedDatabaseAsync().GetAwaiter().GetResult();
            }
            return app;
        }
    }
}
