using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;
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

        public async Task<List<GradeDomainModel>> GetAllGrades()
        {
            var grades = await _gradesRepository.GetAllStudentsGrades();

            if(grades == null)
            {
                return null;
            }

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

        public async Task<bool> UpdateGrade(int gradeId, GradeDomainModel grade)
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

        public async Task<bool> DeleteGrades()
        {
            return false;
        }
    }
}
