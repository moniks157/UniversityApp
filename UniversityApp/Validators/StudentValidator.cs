using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.DTOs;

namespace UniversityApp.Validators
{
    public class StudentValidator:AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            RuleFor(student => student.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Matches(Constants.NAME_REGEX)
                .DependentRules(() =>
                {
                    RuleFor(student => student.FirstName)
                        .Cascade(CascadeMode.Stop)
                        .NotNull()
                        .NotEqual(student => student.LastName)
                        .Matches(Constants.NAME_REGEX);
                });

            RuleFor(student => student.Age)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .InclusiveBetween(Constants.MIN_AGE, Constants.MAX_AGE)
                .GreaterThan(Constants.AGE_OF_ADULTHOOD).When(student => student.IsAdult);

            RuleFor(student => student.Gender)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Matches(Constants.GENDER_REGEX);
        }
    }
}
