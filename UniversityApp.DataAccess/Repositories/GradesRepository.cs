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

        public async Task<List<Grade>> GetAllStudentsGrades()
        {
            var result = await _context.Grades.ToListAsync();

            return result;
        }

        public async Task<List<Grade>> GetStudentGrades(int studentId)
        {
            var studentsWithGrades = await _context.Students.Include(g => g.Grades).ToListAsync();

            var grades = studentsWithGrades.Find(student => student.Id == studentId).Grades;

            return grades;
        }

        public async Task<bool> UpdateGrade(Grade grade)
        {
            _context.Attach(grade);
            _context.Entry(grade).Property("Value").IsModified = true;
            _context.Entry(grade).Property("Description").IsModified = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGrades()
        {
            var grades = _context.Grades;

            foreach(var grade in grades)
            {
                _context.Grades.Remove(grade);
            }

            await _context.SaveChangesAsync();

            return false;
        }
    }
}
