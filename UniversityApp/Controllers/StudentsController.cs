using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DTO;
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

            var result = new List<Student>();

            foreach (var student in students)
            {
                result.Add(new Student
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

            var result = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            var studentToAdd = new StudentDTO
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
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            var studentToUpdate = new StudentDTO
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
    }
}
