using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
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

        [Route("{student_id}")]
        [HttpPost]
        public IActionResult PostGradeToStudent(int student_id, Grade grade)
        {
            var student = _studentService.GetStudents().Find(student => student.Id == student_id);

            grade.Id = student.Grades.Last().Id + 1;
            student.Grades.Add(grade);
            return Ok(student);
        }

        [Route("{student_id}/{grade_id}")]
        [HttpPut]
        public IActionResult PutGradeToStudent(int student_id, int grade_id, Grade grade)
        {
            var student = _studentService.GetStudents().Find(student => student.Id == student_id);
            
            if (student == null)
                return NotFound();


            return BadRequest();
        }

        [Route("{student_id}/{grade_id}")]
        [HttpDelete]
        public IActionResult DeleteGradeFromStudent(int student_id, int grade_id)
        {
            return BadRequest();
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

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var students = _studentService.GetStudents();

            var result = students.Find(s => s.Id == id);

            if (result == null)
                return NotFound();

            students.Remove(result);
            return Ok(students);
        }

    }
}
