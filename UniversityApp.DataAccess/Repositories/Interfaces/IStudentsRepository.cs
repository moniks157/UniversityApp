using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.DataAccess.Repositories.Interfaces
{
    public interface IStudentsRepository
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<int> AddStudent(Student student);
        Task<bool> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);
    }
}
