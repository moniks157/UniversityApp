using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAppDataAccess.Repositories.Interfaces;
using UniversityDataAccess;
using UniversityDataAccess.Entities;

namespace UniversityAppDataAccess.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityContext _context;

        public StudentRepository(UniversityContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();

            return students;
        }

        public async Task<int> AddStudent(Student student)
        {
            student.Id = await _context.Students.AnyAsync() ? _context.Students.OrderBy(s => s.Id).LastAsync().Id + 1 : 1;

            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();

            return student.Id;
        }
    }
}
