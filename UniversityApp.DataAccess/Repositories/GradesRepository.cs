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
            var grades = await _context.Grades.Where(grade => grade.StudentId == studentId).ToListAsync();

            return grades;
        }

        public async Task<Grade> GetGrade(int id)
        {
            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.Id == id);

            return grade;
        }

        public async Task<bool> UpdateGrade(Grade grade)
        {
            var gradeToUpdate = await GetGrade(grade.Id);

            if(gradeToUpdate == null)
            {
                return false;
            }

            _context.Attach(grade);
            _context.Entry(grade).Property("Value").IsModified = true;
            _context.Entry(grade).Property("Description").IsModified = true;

            await _context.SaveChangesAsync();

            return true;
        }
        //by id
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
