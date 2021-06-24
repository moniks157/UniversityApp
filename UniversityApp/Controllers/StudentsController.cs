using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.Models;

namespace UniversityApp.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentsService.GetStudents();

            var result = new List<StudentDto>();

            foreach (var student in students)
            {
                result.Add(new StudentDto
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Gender = student.Gender
                });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentsService.GetStudent(id);

            var result = new StudentDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDto student)
        {
            var studentToAdd = new StudentDomainModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            var id = await _studentsService.AddStudent(studentToAdd);

            return Created($"~api/students/{id}",id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentDto student)
        {
            var studentToUpdate = new StudentDomainModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            var result = await _studentsService.UpdateStudent(id, studentToUpdate);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentsService.DeleteStudent(id);
            
            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{id}/grades")]
        public async Task<IActionResult> GetGrades(int id)
        {
            var grades = await _studentsService.GetStudentGrades(id);

            var result = new List<GradeDto>();

            foreach(var grade in grades)
            {
                result.Add(new GradeDto
                {
                    Value = grade.Value,
                    Description = grade.Description
                });
            }

            return Ok(result);
        }

        [HttpPost("{id}/grades")]
        public async Task<IActionResult> PostGrade(int id, GradeDto grade)
        {
            var gradeToAdd = new GradeDomainModel
            {
                Value = grade.Value,
                Description = grade.Description
            };

            await _studentsService.AddStudentGrade(id, gradeToAdd);

            return Ok();
        }

        [HttpPut("{id}/grades/{gradeId}")]
        public async Task<IActionResult> PutGrade(int id, int gradeId, GradeDto grade)
        {
            return Ok();
        }
    }
}
