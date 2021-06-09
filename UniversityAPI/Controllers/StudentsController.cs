using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using UniversityAPI.Models;
using UniversityAPI.Services.Intefaces;

namespace UniversityAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentsService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentService.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentService.GetStudent(id);

            if(student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery]string name, [FromQuery] int age, [FromQuery] string gender)
        {
            var result = _studentService.Search(name, age, gender);

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            _studentService.AddStudent(student);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student student)
        {
            if(!_studentService.UpdateStudent(id, student))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_studentService.RemoveStudent(id))
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
