using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAppBussinessLogic.DTO;
using UniversityAppBussinessLogic.Services.Interfaces;

namespace UniversityAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();

            var result = new List<Student>();
            return Ok(students);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetStudent(int id)
        //{
        //    var student = _studentService.GetStudent(id);

        //    if(student == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(student);
        //}

        //[HttpGet("search")]
        //public IActionResult Search([FromQuery]string name, [FromQuery] int age, [FromQuery] string gender)
        //{
        //    var result = _studentService.Search(name, age, gender);

        //    if (result == null || !result.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Student student)
        {
            var s = new StudentDTO 
            { 
            FirstName = student.FirstName,
            LastName = student.LastName,
            Age = student.Age,
            Gender = student.Gender
            };

            await _studentService.AddStudent(s);

            return Ok();
        }

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] Student student)
        //{
        //    if(!_studentService.UpdateStudent(id, student))
        //    {
        //        return BadRequest();
        //    }
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    if (!_studentService.RemoveStudent(id))
        //    {
        //        return BadRequest();
        //    }
        //    return NoContent();
        //}

    }
}
