using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;

namespace UniversityApp.BussinessLogic.Services.Interfaces
{
    public interface IGradesService
    {
        Task<List<GradeDomainModel>> GetAllGrades();
        Task<bool> UpdateGrade(int gradeId, GradeDomainModel grade);
        Task<bool> DeleteAllGrades();
    }
}
