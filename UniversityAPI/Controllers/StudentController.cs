using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private List<Student> students = new List<Student>()
            {new Student(){Id = 1, FirstName = "Jan", LastName = "Kowialski", Age = 22, Gender = Gender.M},
            new Student(){Id = 2, FirstName = "Karol", LastName = "Nowak", Age = 23, Gender = Gender.M},
            new Student(){Id = 3, FirstName = "Anna", LastName = "Ptak", Age = 21, Gender = Gender.K}};

        [Route("")]
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(students);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetStudent(int id)
        {
            var result = students.Find(s => s.Id == id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(int id, string firstName, string lastName, int age, int gender)
        {
            var student = new Student() { Id = id, FirstName = firstName, LastName = lastName, Age = age, Gender = (Gender)gender };
            //todo validation
            students.Add(student);
            return Ok(students);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Put()
        {
            //todo
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete()
        {
            //todo
            return Ok();
        }

    }
}
