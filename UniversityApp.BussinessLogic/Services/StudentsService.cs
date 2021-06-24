using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.BussinessLogic.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IGradesRepository _gradesRepository;

        public StudentsService(IStudentsRepository studentsRepository, IGradesRepository gradesRepository)
        {
            _studentsRepository = studentsRepository;
            _gradesRepository = gradesRepository;
        }

        public async Task<List<StudentDomainModel>> GetStudents()
        {
            var students = await _studentsRepository.GetStudents();

            var result = new List<StudentDomainModel>();

            foreach(var student in students)
            {
                result.Add(new StudentDomainModel
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Gender = student.Gender
                });
            }

            return result;
        }

        public async Task<StudentDomainModel> GetStudent(int id)
        {
            var student = await _studentsRepository.GetStudent(id);

            var result = new StudentDomainModel
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            return result;
        }

        public async Task<int> AddStudent(StudentDomainModel student)
        {
            var studentToAdd = new Student 
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            await _studentsRepository.AddStudent(studentToAdd);

            return studentToAdd.Id;
        }

        public async Task<bool> UpdateStudent(int id, StudentDomainModel student)
        {
            var studentToUpdate = new Student
            {
                Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            var result = await _studentsRepository.UpdateStudent(studentToUpdate);

            return result;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            return await _studentsRepository.DeleteStudent(id);
        }

        public async Task<List<GradeDomainModel>> GetStudentGrades(int id)
        {
            var grades = await _gradesRepository.GetStudentGrades(id);

            var result = new List<GradeDomainModel>();

            foreach(var grade in grades)
            {
                result.Add(new GradeDomainModel
                {
                    Value = grade.Value,
                    Description = grade.Description
                });
            }

            return result;
        }

        public async Task<int> AddStudentGrade(int id, GradeDomainModel grade)
        {
            var gradeToAdd = new Grade
            {
                Value = grade.Value,
                Description = grade.Description,
                StudentId = id
            };

            var result = await _gradesRepository.AddGrade(id, gradeToAdd);

            return result;
        }

        public async Task<bool> UpdateStudentGarde(int id, int gradeId, GradeDomainModel grade)
        {
            var gradeToUpdate = new Grade
            {
                Id = gradeId,
                Value = grade.Value,
                Description = grade.Description
            };

            await _gradesRepository.UpdateGrade(gradeToUpdate);

            return true;
        }
    }
}
