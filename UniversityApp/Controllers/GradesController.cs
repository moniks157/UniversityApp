using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.Models;

namespace UniversityApp.Controllers
{
    [Route("api/grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradesService _gradesService;

        public GradesController(IGradesService gradesService)
        {
            _gradesService = gradesService;
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
            var gradeToUpdate = new GradeDomainModel
            {
                Value = grade.Value,
                Description = grade.Description
            };

            await _gradesService.UpdateGrade(id, gradeToUpdate);

            return Ok();
        }
    }
}
