

using ARCN.Application.Interfaces;
using ARCN.Repository.Repositories;

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
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork>(c => { return c.GetRequiredService<ARCNDbContext>(); });
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();;
            services.AddScoped<IStateRepository, StateRepository>();
           

            return services;
        }
    }
}
