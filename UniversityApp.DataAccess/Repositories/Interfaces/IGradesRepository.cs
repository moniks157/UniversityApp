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
        public Task<List<Grade>> GetAllStudentGrades(int studentId);
        public Task<int> AddGrade(int studentId, Grade grade);
    }
}
