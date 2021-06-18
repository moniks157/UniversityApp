using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DTO;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.BussinessLogic.Services
{
    public class GradesService : IGradesService
    {
        private readonly IGradesRepository _gradesRepository;

        public GradesService(IGradesRepository gradesRepository)
        {
            _gradesRepository = gradesRepository;
        }

        public async Task<List<GradeDTO>> GetAllGrades(int studentId)
        {
            var grades = await _gradesRepository.GetAllStudentGrades(studentId);

            if(grades == null)
            {
                return null;
            }

            var result = new List<GradeDTO>();
            foreach(var grade in grades)
            {
                result.Add(new GradeDTO
                {
                    Value = grade.Value,
                    Description = grade.Description
                });
            }

            return result;
        }

        public async Task<int> AddGrade(int studentId, GradeDTO grade)
        {
            var gradeToAdd = new Grade
            {
                Value = grade.Value,
                Description = grade.Description,
                StudentId = studentId,
            };

            await _gradesRepository.AddGrade(studentId, gradeToAdd);

            return gradeToAdd.Id;
        }
    }
}
