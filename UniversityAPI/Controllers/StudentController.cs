using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Services;

namespace UniversityAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentService.GetStudents();
            return Ok(students);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            var students = _studentService.GetStudents();

            var result = students.Find(s => s.Id == id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostStudent(Student student)
        {
            var students = _studentService.GetStudents();

            student.Id = students.Last().Id + 1;
            students.Add(student);
            return Ok(students);
            
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult PutStudent(int id, Student student)
        {
            var students = _studentService.GetStudents();

            var result = students.Find(s => s.Id == id);
            
            if (result == null)
                return NotFound();

            result.FirstName = student.FirstName;
            result.LastName = student.LastName;
            result.Age = student.Age;
            result.Gender = student.Gender;

            return Ok(students);
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var students = _studentService.GetStudents();

            var result = students.Find(s => s.Id == id);

            if (result == null)
                return NotFound();

            students.Remove(result);

            students = _studentService.GetStudents();

            return Ok(students);
        }

    }
}
