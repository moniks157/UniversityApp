using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;

namespace UniversityApp.BussinessLogic.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<List<StudentDomainModel>> GetStudents();
        Task<(List<StudentDomainModel> Students, int TotalRecordCount)> SearchStudents(StudentSearchParametersDomainModel studentSearchModel);
        Task<StudentDomainModel> GetStudent(int id);
        Task<int> AddStudent(StudentDomainModel student);
        Task<bool> UpdateStudent(int id, StudentDomainModel student);
        Task<bool> DeleteStudent(int id);
        Task<List<GradeDomainModel>> GetStudentGrades(int id);
        Task<int?> AddStudentGrade(GradeDomainModel grade);
        Task<bool> UpdateStudentGarde(int gradeId, GradeDomainModel grade);
        Task<bool> StudentExists(int id);
    }
}
