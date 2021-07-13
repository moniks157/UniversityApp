using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UniversityApp.DataAccess.Repositories
{
    public class GradesRepository : IGradesRepository
    {
        private readonly UniversityContext _context;

        public GradesRepository(UniversityContext context)
        {
            _context = context;
        }

        public async Task<int> AddGrade(Grade grade)
        {
            _context.Grades.Add(grade);

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
            var grades = await _context.Grades.Where(grade => grade.StudentId == studentId).ToListAsync();

            return grades;
        }

        public async Task<Grade> GetGrade(int id)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.Id == id);

            return grade;
        }

        public async Task<Grade> GetGrade(int id, int studentId)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.Id == id && g.StudentId == studentId);

            return grade;
        }

        public async Task<bool> UpdateGrade(Grade grade)
        {
            var gradeToUpdate = await GetGrade(grade.Id, grade.StudentId);

            if(gradeToUpdate == null)
            {
                return false;
            }

            gradeToUpdate.Value = grade.Value;
            gradeToUpdate.Description = grade.Description;

            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<bool> DeleteGrade(int id)
        {
            var grade = await GetGrade(id);

            if(grade == null)
            {
                return false;
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
