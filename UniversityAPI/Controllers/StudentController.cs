using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private List<Student> students = new List<Student>()
            {new Student(){Id = 1, FirstName = "Jan", LastName = "Kowialski", Age = 22, Gender = "M", 
                Grades = new List<Grade>()
                {new Grade{Value = 3, Description = "nothing"},
                new Grade{Value = 4, Description = "nothing"},
                new Grade{Value = 5, Description = "nothing"}} },
            new Student(){Id = 2, FirstName = "Karol", LastName = "Nowak", Age = 23, Gender = "M"},
            new Student(){Id = 3, FirstName = "Anna", LastName = "Ptak", Age = 21, Gender = "K"}};

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
        public IActionResult PostStudent(Student student)
        {
            student.Id = students.Last().Id + 1;
            students.Add(student);
            return Ok(students);
            
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult PutStudent(int id, Student student)
        {
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
            var result = students.Find(s => s.Id == id);

            if (result == null)
                return NotFound();

            students.Remove(result);

            return Ok(students);
        }

    }
}
