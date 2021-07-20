using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.DTOs;
using UniversityApp.Validators;

namespace UniversityApp.Configuration
{
    public static class ServiceCollectionExtentions
    {   
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<StudentDto>, StudentValidator>();
            services.AddScoped<IValidator<GradeDto>, GradeValidatior>();

            return services;
        }
    }
}
