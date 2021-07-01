using AutoMapper;
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
        private readonly IMapper _mapper;

        public GradesService(IGradesRepository gradesRepository, IMapper mapper)
        {
            _gradesRepository = gradesRepository;
            _mapper = mapper;
        }

        public async Task<List<GradeDomainModel>> GetAllGrades()
        {
            var grades = await _gradesRepository.GetAllStudentsGrades();

            if(grades == null)
            {
                return null;
            }

            var result = _mapper.Map<List<GradeDomainModel>>(grades);

            return result;
        }

        public async Task<bool> UpdateGrade(int gradeId, GradeDomainModel grade)
        {
            var gradeToUpdate = _mapper.Map<Grade>(grade);

            await _gradesRepository.UpdateGrade(gradeToUpdate);

            return true;
        }

        public async Task<bool> DeleteGrades()
        {
            return false;
        }
    }
}
