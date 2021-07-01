using AutoMapper;
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

            var result = await _studentsRepository.UpdateStudent(studentToUpdate);

            return result;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            return await _studentsRepository.DeleteStudent(id);
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

        public async Task<int> AddStudentGrade(int id, GradeDomainModel grade)
        {
            var gradeToAdd = _mapper.Map<Grade>(grade);

            var result = await _gradesRepository.AddGrade(id, gradeToAdd);

            return result;
        }

        public async Task<bool> UpdateStudentGarde(int id, int gradeId, GradeDomainModel grade)
        {
            var gradeToUpdate = _mapper.Map<Grade>(grade);

            await _gradesRepository.UpdateGrade(gradeToUpdate);

            return true;
        }
    }
}
