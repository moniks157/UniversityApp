using Microsoft.Extensions.DependencyInjection;
using UniversityApp.BussinessLogic.Services;
using UniversityApp.BussinessLogic.Services.Interfaces;

namespace UniversityApp.BussinessLogic.Configuration
{
    public static class BusinessLogicDI
    {
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddTransient<IStudentsService, StudentsService>();
            services.AddTransient<IGradesService, GradesService>();

            return services;
        }
    }
}
