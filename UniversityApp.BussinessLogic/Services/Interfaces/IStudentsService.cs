using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DomainModels;

namespace UniversityApp.BussinessLogic.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<List<StudentDomainModel>> GetStudents();
        Task<StudentDomainModel> GetStudent(int id);
        Task<int> AddStudent(StudentDomainModel student);
        Task<bool> UpdateStudent(int id, StudentDomainModel student);
        Task<bool> DeleteStudent(int id);
        Task<List<GradeDomainModel>> GetStudentGrades(int id);
        Task<int> AddStudentGrade(int id, GradeDomainModel grade);
        Task<bool> UpdateStudentGarde(int id, int gradeId, GradeDomainModel grade);
    }
}
