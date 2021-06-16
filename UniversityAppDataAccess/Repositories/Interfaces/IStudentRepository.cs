using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityDataAccess.Entities;

namespace UniversityAppDataAccess.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
        Task<int> AddStudent(Student student);
    }
}
