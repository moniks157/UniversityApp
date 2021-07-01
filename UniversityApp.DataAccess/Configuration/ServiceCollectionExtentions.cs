using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversityApp.DataAccess.Repositories;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.DataAccess.Configuration
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddDataAccessConnector(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UniversityDatabaseConfig>(configuration.GetSection(nameof(UniversityDatabaseConfig)));
            services.AddDbContext<UniversityContext>
                (options => options
                    .UseLazyLoadingProxies());
            
            return services;
        }

        public static IServiceCollection AddDataAccessRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGradesRepository, GradesRepository>();
            services.AddTransient<IStudentsRepository, StudentsRepository>();

            return services;
        }
    }
}
