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
            var students = _context.Students.AsQueryable();

            students = SearchStudentByFirstName(studentSearchParameters.FirstName, students);
            students = SearchStudentByLastName(studentSearchParameters.LastName, students);
            students = SearchStudentByAge(studentSearchParameters.Age, students);
            students = SearchStudentByGender(studentSearchParameters.Gender, students);

            int recordsToBeSkipped = (studentSearchParameters.PageNumber - 1) * studentSearchParameters.PageSize;

            var data = await students.Skip(recordsToBeSkipped).Take(studentSearchParameters.PageSize).ToListAsync();

            var totalRecordsCount = students.Count();

            var result = (data, totalRecordsCount);

            return result;
        }

        private IQueryable<Student> SearchStudentByFirstName(string firstName, IQueryable<Student> students)
        {
            if (!String.IsNullOrEmpty(firstName))
            {
                students = students.Where(s => s.FirstName.Contains(firstName));
            }
            return students;
        }

        private IQueryable<Student> SearchStudentByLastName(string lastName, IQueryable<Student> students)
        {
            if (!String.IsNullOrEmpty(lastName))
            {
                students = students.Where(s => s.LastName.Contains(lastName));
            }
            return students;
        }

        private IQueryable<Student> SearchStudentByAge(int? age, IQueryable<Student> students)
        {
            if (age != null)
            {
                students = students.Where(s => s.Age == age);
            }
            return students;
        }

        private IQueryable<Student> SearchStudentByGender(string gender, IQueryable<Student> students)
        {
            if (!String.IsNullOrEmpty(gender))
            {
                students = students.Where(s => s.Gender.Contains(gender));
            }
            return students;
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
