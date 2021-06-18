using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var studentsWithGrades = await _context.Students.Include(g => g.Grades).ToListAsync();

            var grades = studentsWithGrades.Find(student => student.Id == studentId).Grades;

            return grades;
        }
    }
}
