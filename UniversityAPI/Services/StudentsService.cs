using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Services.Intefaces;

namespace UniversityAPI.Services
{
    public class StudentsService : IStudentsService
    {
        private static List<Student> students = new List<Student>()
        {
            new Student()
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowialski",
                Age = 22,
                Gender = "M",
                Grades = new List<Grade>()
                {
                    new Grade
                    {
                        Id = 1,
                        Value = 3, 
                        Description = "nothing"
                    },
                    new Grade
                    {
                        Id = 2,
                        Value = 4, 
                        Description = "nothing"
                    },
                    new Grade
                    {
                        Id = 3,
                        Value = 5, 
                        Description = "nothing"
                    }
                } 
            },
            new Student()
            {
                Id = 2, 
                FirstName = "Karol", 
                LastName = "Nowak", 
                Age = 23, 
                Gender = "M"
            },
            new Student()
            {
                Id = 3, 
                FirstName = "Anna", 
                LastName = "Ptak", 
                Age = 21, 
                Gender = "K"
            }
        };

        public IEnumerable<Student> GetStudents()
        {
            return students;
        }

        public Student GetStudent(int id)
        {
            var student = students.Find(student => student.Id == id);
            return student;
        }

        public void AddStudent(Student student)
        {
            if (students.Any())
            {
                student.Id = students.Last().Id + 1;
            }
            students.Add(student);
        }

        public bool UpdateStudent(int id, Student student)
        {
            var studentToUpdate = students.Find(student => student.Id == id);

            if (studentToUpdate != null)
            {
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.Age = student.Age;
                studentToUpdate.Gender = student.Gender;
            }

            return studentToUpdate != null;
        }
        public bool RemoveStudent(int id)
        {
            var studentToRemove = students.Find(student => student.Id == id);
            return students.Remove(studentToRemove);
        }

        public IEnumerable<Student> Search(string firstName, string lastName, int age, string gender)
        {
            var result = students;

            if(!string.IsNullOrEmpty(firstName))
            {
                result = result.Where(student => student.FirstName.Contains(firstName)).ToList();
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                result = result.Where(student => student.LastName.Contains(lastName)).ToList();
            }

            if (age >= 18)
            {
                result = result.Where(student => student.Age == age).ToList();
            }

            if (!string.IsNullOrEmpty(gender))
            {
                result = result.Where(student => student.Gender.ToLower().Contains(gender)).ToList();
            }

            return result;
        }

    }
}
