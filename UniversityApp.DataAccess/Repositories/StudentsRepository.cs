using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.DataAccess.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        UniversityContext _context;

        public StudentsRepository(UniversityContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudents()
        {
            var result = await _context.Students.ToListAsync();

            return result;
        }

        public async Task<Student> GetStudent(int id)
        {
            var result = await _context.Students.FindAsync(id);

            return result;
        }

        public async Task<int> AddStudent(Student student)
        {
            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();

            return student.Id;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            _context.Attach(student);
            _context.Entry(student).Property("FirstName").IsModified = true;
            _context.Entry(student).Property("LastName").IsModified = true;
            _context.Entry(student).Property("Age").IsModified = true;
            _context.Entry(student).Property("Gender").IsModified = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return false;
            }
            
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
