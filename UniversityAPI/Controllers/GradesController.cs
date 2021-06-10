using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models;
using UniversityAPI.Services.Intefaces;

namespace UniversityAPI.Controllers
{
    [Route("api/studentgrades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradesService _gradesService;

        public GradesController(IGradesService gradesService)
        {
            _gradesService = gradesService;
        }
        
        [HttpGet]
        public IActionResult GetAllGrades()
        {
            var result = _gradesService.GetAllGrades();
            return Ok(result);
        }

        [HttpGet("{studentId}/grades")]
        public IActionResult GetStudentGrades(int studentId)
        {
            var result = _gradesService.GetGrades(studentId);
            return Ok(result);
        }

        [HttpPost("{studentId}")]
        public IActionResult Post(int studentId, [FromBody] Grade grade)
        {
            var result = _gradesService.AddGrade(studentId, grade);

            if(result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{studentId}/grades/{gradeId}")]
        public IActionResult Put(int studentId, int gradeId, [FromBody] Grade grade)
        {
            if(!_gradesService.UpdateGrade(studentId,gradeId,grade))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{studentId}/grades/{gradeId}")]
        public IActionResult Delete(int studentId, int gradeId)
        {
            if(!_gradesService.DeleteGrade(studentId, gradeId))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
