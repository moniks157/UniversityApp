using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DTO;

namespace UniversityApp.BussinessLogic.Services.Interfaces
{
    public interface IGradesService
    {
        Task<List<GradeDTO>> GetAllGrades(int studentId);
        Task<int> AddGrade(int studentId, GradeDTO grade);
    }
}
