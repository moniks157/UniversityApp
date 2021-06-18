using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DTO;
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

        [HttpGet("{studentId}")]
        public async Task<IActionResult> Get(int studentId)
        {
            var result = await _gradesService.GetAllGrades(studentId);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("{studentId}")]
        public async Task<IActionResult> Post(int studentId, [FromBody]Grade grade)
        {
            var gradeToAdd = new GradeDTO
            {
                Value = grade.Value,
                Description = grade.Description
            };

            var result = await _gradesService.AddGrade(studentId, gradeToAdd);

            return Ok(result);
        }
    }
}
