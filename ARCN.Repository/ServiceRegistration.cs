﻿

using ARCN.Application.Interfaces;
using ARCN.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ARCN.Repository
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContextPool<ARCNDbContext>((sp, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ARCNConnection"));
                options.EnableSensitiveDataLogging(true);
            }).AddTransient<ARCNDbSeeder>();


           // services.AddIdentityCore<ApplicationUser>
           // (options =>
           // {
           //     options.SignIn.RequireConfirmedAccount = true;
           //     options.User.RequireUniqueEmail = false;
           //     options.SignIn.RequireConfirmedEmail = true;
           //     options.Password.RequireDigit = false;
           //     options.Password.RequireNonAlphanumeric = true;
           //     options.Password.RequireUppercase = true;
           //     options.Password.RequireLowercase = true;
           // })
           //.AddUserManager<ApplicationUser>()
           //.AddRoles<IdentityRole>()
           //.AddRoleManager<RoleManager<IdentityRole>>()
           //.AddEntityFrameworkStores<ARCNDbContext>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork>(c => { return c.GetRequiredService<ARCNDbContext>(); });
            services.AddScoped<IUserProfileRepository, UserProfileRepository>(); ;
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IJournalRepository, JournalRepository>();
            services.AddScoped<INewsLetterRepository, NewsLetterRepository>();
            services.AddScoped<IReportsRepository, ReportsRepository>();
            services.AddScoped<INarisRepository, NarisRepository>();
            services.AddScoped<ICordinationReportRepository, CordinationReportRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IFCARepository, FCARepository>();
            services.AddScoped<IExtensionRepository, ExtensionRepository>();
            services.AddScoped<ILocalGovernmentAreaRepository, LocalGovernmentRepository>();

            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IEntrepreneurshipRepository, EntrepreneurshipRepository>();
            services.AddScoped<ISupervisionReportRepository, SupervisorReportRepository>();


            return services;
        }
    }
}
