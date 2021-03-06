using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DTOs;

namespace UniversityApp.Validators
{
    public class GradeValidator : AbstractValidator<GradeDto>
    {
        IStudentsService _studentsService;

        public GradeValidator(IStudentsService studentsService)
        {
            _studentsService = studentsService;

            RuleSet(Constants.RULESET_REQUIRED, () =>
            {
                RuleFor(grade => grade.Value).NotNull();
                RuleFor(grade => grade.Description).NotNull();
            });

            RuleFor(grade => grade.Value)
                .InclusiveBetween(2.0, 5.5)
                .Custom((value, context) =>
                {
                    if(value % 0.5 != 0 && value != 2.5)
                    {
                        context.AddFailure("The value must be one of the following 2.0, 3.0, 3.5, 4.0, 4.5, 5.0, 5.5");
                    }
                });

            RuleFor(grade => grade.StudentId)
                .MustAsync(async (studentId, cancellation) =>
                {
                    var exists = await _studentsService.DoesStudentExists(studentId);
                    return exists;
                }).WithMessage("Student doesn't exist in this context");
        }
    }
}
