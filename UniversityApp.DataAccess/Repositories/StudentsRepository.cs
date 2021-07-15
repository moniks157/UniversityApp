using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Models;
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

        public async Task<(List<Student> Students, int TotalRecordCount)> GetStudents(List<Expression<Func<Student, bool>>> predicates, int skip, int amount)
        {
            var students = _context.Students.AsQueryable();

            if(predicates != null)
            {
                foreach(var predicate in predicates)
                {
                    students = students.Where(predicate);
                }
            }

            var data = await students.Skip(skip).Take(amount).ToListAsync();

            var totalRecordsCount = students.Count();

            var result = (data, totalRecordsCount);

            return result;
        }

        public async Task<Student> GetStudent(int id)
        {
            var result = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

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
            var studentToUpdate = await GetStudent(student.Id);

            if(studentToUpdate == null)
            {
                return false;
            }

            studentToUpdate.FirstName = student.FirstName;
            studentToUpdate.LastName = student.LastName;
            studentToUpdate.Age = student.Age;
            studentToUpdate.Gender = student.Gender;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await GetStudent(id);

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
