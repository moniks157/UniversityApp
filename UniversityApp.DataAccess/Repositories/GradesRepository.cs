using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.DataAccess.Repositories
{
    public class GradesRepository : IGradesRepository
    {
        private readonly UniversityContext _context;

        public GradesRepository(UniversityContext context)
        {
            _context = context;
        }

        public async Task<int> AddGrade(int studentId, Grade grade)
        {
            var student = await _context.Students.FindAsync(studentId);

            _context.Attach(student);

            student.Grades.Add(grade);

            await _context.SaveChangesAsync();

            return grade.Id;
        }

        public async Task<List<Grade>> GetAllStudentGrades(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            if (student == null)
            {
                return null;
            }

            return student.Grades;
        }
    }
}
