﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityApp.BussinessLogic.DTO;
using UniversityApp.BussinessLogic.Services.Interfaces;
using UniversityApp.DataAccess.Entities;
using UniversityApp.DataAccess.Repositories.Interfaces;

namespace UniversityApp.BussinessLogic.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentsService(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public async Task<List<StudentDTO>> GetStudents()
        {
            var students = await _studentsRepository.GetStudents();

            var result = new List<StudentDTO>();

            foreach(var student in students)
            {
                result.Add(new StudentDTO
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Age = student.Age,
                    Gender = student.Gender
                });
            }

            return result;
        }

        public async Task<StudentDTO> GetStudent(int id)
        {
            var student = await _studentsRepository.GetStudent(id);

            var result = new StudentDTO
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            return result;
        }

        public async Task<int> AddStudent(StudentDTO student)
        {
            var studentToAdd = new Student 
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            await _studentsRepository.AddStudent(studentToAdd);

            return studentToAdd.Id;
        }

        public async Task<bool> UpdateStudent(int id, StudentDTO student)
        {
            var studentToUpdate = new Student
            {
                Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Gender = student.Gender
            };

            var result = await _studentsRepository.UpdateStudent(studentToUpdate);

            return result;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            return await _studentsRepository.DeleteStudent(id);
        }
    }
}