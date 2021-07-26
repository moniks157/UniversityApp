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

        public async Task<(List<Student> Students, int TotalRecordCount)> GetStudents(StudentSearchParameters studentSearchParameters)
        {
            var students = SearchStudentByParameters(studentSearchParameters);

            int recordsToBeSkipped = (studentSearchParameters.PageNumber - 1) * studentSearchParameters.PageSize;

            var data = await students
                .Skip(recordsToBeSkipped)
                .Take(studentSearchParameters.PageSize)
                .ToListAsync();

            return (data, students.Count());
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

        public async Task<bool> DoesStudentExist(int id)
        {
            var exists = await _context.Students.AnyAsync(student => student.Id == id);

            return exists;
        }

        private IQueryable<Student> SearchStudentByParameters(StudentSearchParameters searchParameters)
        {
            var students = _context.Students.AsQueryable();

            if (!String.IsNullOrEmpty(searchParameters.FirstName))
            {
                students = students.Where(s => s.FirstName.Contains(searchParameters.FirstName));
            }
            if (!String.IsNullOrEmpty(searchParameters.LastName))
            {
                students = students.Where(s => s.LastName.Contains(searchParameters.LastName));
            }
            if (searchParameters.Age != null)
            {
                students = students.Where(s => s.Age == searchParameters.Age);
            }
            if (!String.IsNullOrEmpty(searchParameters.Gender))
            {
                students = students.Where(s => s.Gender.Contains(searchParameters.Gender));
            }
            return students;
        }
    }
}
