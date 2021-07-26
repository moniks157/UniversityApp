using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DTOs;

namespace UniversityApp.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradesService _gradesService;
        private readonly IMapper _mapper;
        private readonly IValidator<GradeDto> _gradeValidator;

        public GradesController(IGradesService gradesService, IMapper mapper, IValidator<GradeDto> gradeValidator)
        {
            _gradesService = gradesService;
            _mapper = mapper;
            _gradeValidator = gradeValidator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _gradesService.GetAllGrades();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GradeDto grade)
        {
            var gradeValidation = _gradeValidator.Validate(grade, options => {
                options
                .IncludeRuleSets(Constants.RULESET_REQUIRED);
                });

            if(!gradeValidation.IsValid)
            {
                return BadRequest(gradeValidation.Errors);
            }

            var gradeToUpdate = _mapper.Map<GradeDomainModel>(grade);

            var result = await _gradesService.UpdateGrade(id, gradeToUpdate);

            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _gradesService.DeleteGrade(id);

            if(!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
