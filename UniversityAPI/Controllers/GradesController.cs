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
        //get all grades 
        [HttpGet("{studentId}/grades")]
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
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{gradeId}")]
        public IActionResult Put(int studentId, int gradeId, [FromBody] Grade grade)
        {
            if(!_gradesService.UpdateGrade(studentId,gradeId,grade))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{gradeId}")]
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
