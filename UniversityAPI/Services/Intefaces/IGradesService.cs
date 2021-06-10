using System.Collections.Generic;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Intefaces
{
    public interface IGradesService
    {
        List<Grade> GetAllGrades();
        List<Grade> GetGrades(int studentId);
        Grade AddGrade(int studentId, Grade grade);
        bool UpdateGrade(int studentId, int gradeId, Grade grade);
        bool DeleteGrade(int studentId, int gradeId);
    }
}
