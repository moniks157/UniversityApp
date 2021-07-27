using FluentValidation;
using UniversityApp.DTOs;

namespace UniversityApp.Validators
{
    public class StudentSearchParametersValidator : AbstractValidator<StudentSearchParametersDto>
    {
        public StudentSearchParametersValidator()
        {
            RuleFor(searchParams => searchParams.PageNumber)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Page number must be not null and greater than 0");
            
            RuleFor(searchParams => searchParams.PageSize)
                .NotNull()
                .InclusiveBetween(Constants.MIN_PAGE_SIZE, Constants.MAX_PAGE_SIZE)
                .WithMessage($"Page Parameters must be in between {Constants.MIN_PAGE_SIZE} and {Constants.MAX_PAGE_SIZE}");
        }
    }
}
