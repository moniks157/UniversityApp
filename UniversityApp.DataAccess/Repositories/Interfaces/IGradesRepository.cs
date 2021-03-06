using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.DataAccess.Repositories.Interfaces
{
    public interface IGradesRepository
    {
        Task<List<Grade>> GetAllStudentsGrades();
        Task<List<Grade>> GetStudentGrades(int studentId);
        Task<Grade> GetGrade(int id);
        Task<int> AddGrade(Grade grade);
        Task<bool> UpdateGrade(Grade grade);
        Task<bool> DeleteGrade(int id);
    }
}
