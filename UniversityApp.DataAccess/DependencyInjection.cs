using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Repositories;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IGradesRepository, GradesRepository>();
            services.AddTransient<IStudentsRepository, StudentsRepository>();
            services.AddDbContext<UniversityContext>(o => o.UseSqlServer(configuration.GetConnectionString("UniversityDB")));
            
            return services;
        }
    }
}
