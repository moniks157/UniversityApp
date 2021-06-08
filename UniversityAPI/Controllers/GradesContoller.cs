using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models;
using UniversityAPI.Services.Intefaces;

namespace UniversityAPI.Controllers
{
    [Route("api/students/{studentId}/grades")]
    [ApiController]
    public class GradesContoller : ControllerBase
    {
        private readonly IGradesService _gradesService;

        public GradesContoller(IGradesService gradesService)
        {
            _gradesService = gradesService;
        }

        [HttpGet]
        public IActionResult Get(int studentId)
        {
            var result = _gradesService.GetGrades(studentId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(int studentId, [FromBody] Grade grade)
        {
            var result = _gradesService.AddGrade(studentId, grade);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{gradeId}")]
        public IActionResult Put(int studentId, int gradeId, [FromBody] Grade grade)
        {
            if(!_gradesService.UpdateGrade(studentId,gradeId,grade))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{gradeId}")]
        public IActionResult Delete(int studentId, int gradeId)
        {
            if(!_gradesService.DeleteGrade(studentId, gradeId))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
