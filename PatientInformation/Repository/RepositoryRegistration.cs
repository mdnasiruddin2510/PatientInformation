using PatientInformation.Repository.Interface;

namespace PatientInformation.Repository
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INCDRepository, NCDRepository>();
            return services;
        }
    }
}
