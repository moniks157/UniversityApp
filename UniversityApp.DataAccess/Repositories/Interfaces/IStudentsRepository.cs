using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.DataAccess.Repositories.Interfaces
{
    public interface IStudentsRepository
    {
        Task<List<Student>> GetStudents();
        Task<(List<Student> Students, int TotalRecordCount)> GetStudents(List<Expression<Func<Student, bool>>> predicate, int skip, int amount);
        Task<Student> GetStudent(int id);
        Task<int> AddStudent(Student student);
        Task<bool> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);
    }
}
