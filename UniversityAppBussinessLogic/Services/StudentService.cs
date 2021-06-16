using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAppBussinessLogic.DTO;
using UniversityAppBussinessLogic.Services.Interfaces;
using UniversityAppDataAccess.Repositories.Interfaces;
using UniversityDataAccess.Entities;

namespace UniversityAppBussinessLogic.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentsRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentsRepository = studentRepository;
        }

        public async Task<List<StudentDTO>> GetStudents()
        {
            var students = await _studentsRepository.GetStudents();

            var result = new List<StudentDTO>();

            foreach(var s in students)
            {
                var student = new StudentDTO
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Age = s.Age,
                    Gender = s.Gender
                };
                result.Add(student);
            }

            return result;
        }

        public StudentDTO GetStudent(int id)
        {
            throw new NotImplementedException();
        }

        public List<StudentDTO> Search(string name, int age, string gender)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddStudent(StudentDTO student)
        {
            var studendToAdd = new Student
            {
                Id = 0,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            return await _studentsRepository.AddStudent(studendToAdd);
        }

        public bool UpdateStudent(int id, StudentDTO student)
        {
            throw new NotImplementedException();
        }

        public bool RemoveStudent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
