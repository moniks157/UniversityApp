using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services
{
    public interface IStudentService
    {
        List<Student> GetStudents();
    }
    public class StudentService : IStudentService
    {
        private List<Student> students = new List<Student>()
            {new Student(){Id = 1, FirstName = "Jan", LastName = "Kowialski", Age = 22, Gender = "M",
                Grades = new List<Grade>()
                {new Grade{Value = 3, Description = "nothing"},
                new Grade{Value = 4, Description = "nothing"},
                new Grade{Value = 5, Description = "nothing"}} },
            new Student(){Id = 2, FirstName = "Karol", LastName = "Nowak", Age = 23, Gender = "M"},
            new Student(){Id = 3, FirstName = "Anna", LastName = "Ptak", Age = 21, Gender = "K"}};

        public List<Student> GetStudents()
        {
            return students;
        }
    }
}
