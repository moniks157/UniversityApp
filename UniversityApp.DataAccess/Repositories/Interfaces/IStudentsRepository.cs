using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.DataAccess.Repositories.Interfaces
{
    public interface IStudentsRepository
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudent(int id);
        Task<int> AddStudent(Student student);
        Task<bool> UpdateStudent(int id, Student student);
    }
}
