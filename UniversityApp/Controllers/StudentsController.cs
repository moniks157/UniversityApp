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

            var i = await _studentsService.AddStudent(studentToAdd);

            return Ok(i);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {

            return Ok();
        }
    
    }
}
