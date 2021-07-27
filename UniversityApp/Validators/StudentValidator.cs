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
                .NotNull()
                .Matches(Constants.LASTNAME_REGEX).WithMessage("LastName must start with capital letter and be compose of letters only")
                .DependentRules(() =>
                {
                    RuleFor(student => student.FirstName)
                        .NotNull()
                        .NotEqual(student => student.LastName)
                        .Matches(Constants.FIRSTNAME_REGEX);
                });

            RuleFor(student => student.Age)
                .NotNull()
                .InclusiveBetween(Constants.MIN_AGE, Constants.MAX_AGE);

            RuleFor(student => student.IsAdult)
                .Equal(true).When(student => student.Age >= Constants.AGE_OF_ADULTHOOD)
                .WithMessage($"IsAdult must be true if Age is grater than or equal to {Constants.AGE_OF_ADULTHOOD}"); ;

            RuleFor(student => student.IsAdult)
                .Equal(false).When(student => student.Age < Constants.AGE_OF_ADULTHOOD)
                .WithMessage($"IsAdult must be true if Age is grater than or equal to {Constants.AGE_OF_ADULTHOOD}");

            RuleFor(student => student.Gender)
                .NotNull()
                .Matches(Constants.GENDER_REGEX);
        }
    }
}
