using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAppBussinessLogic.DTO;

namespace UniversityAppBussinessLogic.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetStudents();
        StudentDTO GetStudent(int id);
        Task<int> AddStudent(StudentDTO student);
        bool UpdateStudent(int id, StudentDTO student);
        bool RemoveStudent(int id);
        List<StudentDTO> Search(string name, int age, string gender);
    }
}
