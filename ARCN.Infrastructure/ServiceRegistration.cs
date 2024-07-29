
using ARCN.Application.Interfaces;
using ARCN.Application.Interfaces.Services;
using ARCN.Infrastructure.Services.ApplicationServices;
using ARCN.Infrastructure.Services.RefitServices;

namespace ARCN.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IExternalEmailService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(configuration["EmailApiUrl"]);
                });

            services.AddMemoryCache();
            services.AddSingleton<FluidParser>();
            services.AddScoped<MailMessage>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IUserprofileService, UserprofileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICloudinaryFileUploadService, CloudinaryFileUploadService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();
            services.AddScoped<IStaffRegistrationService, StaffRegistrationService>();
            services.AddScoped<IJournalsService, JournalService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<INewsLetterService, NewsLetterService>();

            return services;
        }
    }
}
