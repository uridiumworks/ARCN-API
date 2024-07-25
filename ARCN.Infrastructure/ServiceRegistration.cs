﻿
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

            return services;
        }
    }
}