using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Models;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.BussinessLogic.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IGradesRepository _gradesRepository;
        private readonly IMapper _mapper;

        public StudentsService(IStudentsRepository studentsRepository, IGradesRepository gradesRepository, IMapper mapper)
        {
            _studentsRepository = studentsRepository;
            _gradesRepository = gradesRepository;
            _mapper = mapper;
        }

        public async Task<List<StudentDomainModel>> GetStudents()
        {
            var students = await _studentsRepository.GetStudents();

            var result = _mapper.Map<List<StudentDomainModel>>(students);

            return result;
        }

        public async Task<(List<StudentDomainModel> Students, int TotalRecordCount)> SearchStudents(StudentSearchModel student, int pageNo, int pageSize)
        {
            int skip = (pageNo - 1) * pageSize;

            Expression<Func<Student, bool>> ContainsFirstname = s => s.FirstName.Contains(student.FirstName);
            Expression<Func<Student, bool>> ContainsLastName = s => s.LastName.Contains(student.LastName);
            Expression<Func<Student, bool>> IsAge = s => s.Age == student.Age;
            Expression<Func<Student, bool>> IsGender = s => s.Gender.Contains(student.Gender);

            var predicates = new List<Expression<Func<Student, bool>>>();

            if(!String.IsNullOrEmpty(student.FirstName))
            {
                predicates.Add(ContainsFirstname);
            }
            if (!String.IsNullOrEmpty(student.LastName))
            {
                predicates.Add(ContainsLastName);
            }
            if (student.Age != null)
            {
                predicates.Add(IsAge);
            }
            if (!String.IsNullOrEmpty(student.Gender))
            {
                predicates.Add(IsGender);
            }

            var data = await _studentsRepository.GetStudents(predicates, skip, pageSize);

            var students = _mapper.Map<List<StudentDomainModel>>(data.Students);

            var result = (students, data.TotalRecordCount);
            
            return result;
        }

        public async Task<StudentDomainModel> GetStudent(int id)
        {
            var student = await _studentsRepository.GetStudent(id);

            var result = _mapper.Map<StudentDomainModel>(student);

            return result;
        }

        public async Task<int> AddStudent(StudentDomainModel student)
        {
            var studentToAdd = _mapper.Map<Student>(student);

            await _studentsRepository.AddStudent(studentToAdd);

            return studentToAdd.Id;
        }

        public async Task<bool> UpdateStudent(int id, StudentDomainModel student)
        {
            var studentToUpdate = _mapper.Map<Student>(student);

            studentToUpdate.Id = id;

            var result = await _studentsRepository.UpdateStudent(studentToUpdate);

            return result;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var result = await _studentsRepository.DeleteStudent(id);

            return result;
        }

        public async Task<List<GradeDomainModel>> GetStudentGrades(int id)
        {
            var student = await _studentsRepository.GetStudent(id);

            if(student == null)
            {
                return null;
            }

            var grades = await _gradesRepository.GetStudentGrades(id);

            var result = _mapper.Map<List<GradeDomainModel>>(grades);

            return result;
        }

        public async Task<int?> AddStudentGrade(int id, GradeDomainModel grade)
        {
            var student = await _studentsRepository.GetStudent(id);

            if(student == null)
            {
                return null;
            }

            var gradeToAdd = _mapper.Map<Grade>(grade);

            gradeToAdd.StudentId = id;

            var result = await _gradesRepository.AddGrade(gradeToAdd);

            return result;
        }

        public async Task<bool> UpdateStudentGarde(int id, int gradeId, GradeDomainModel grade)
        {
            var gradeToUpdate = _mapper.Map<Grade>(grade);

            gradeToUpdate.Id = gradeId;
            gradeToUpdate.StudentId = id;

            var result = await _gradesRepository.UpdateGrade(gradeToUpdate);

            return result;
        }
    }
}
