using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DTO;

namespace UniversityApp.BussinessLogic.Services.Interfaces
{
    public interface IStudentsService
    {
        Task<List<StudentDTO>> GetStudents();
        Task<StudentDTO> GetStudent(int id);
        Task<int> AddStudent(StudentDTO student);
    }
}
