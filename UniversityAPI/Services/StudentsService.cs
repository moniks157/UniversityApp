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
                        Value = 3, 
                        Description = "nothing"
                    },
                    new Grade
                    {
                        Value = 4, 
                        Description = "nothing"
                    },
                    new Grade
                    {
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

        public List<Student> GetStudents()
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

    }
}
