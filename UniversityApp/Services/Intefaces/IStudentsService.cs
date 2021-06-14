﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Intefaces
{
    public interface IStudentsService
    {
        List<Student> GetStudents();
        Student GetStudent(int id);
        void AddStudent(Student student);
        bool UpdateStudent(int id, Student student);
        bool RemoveStudent(int id);
        List<Student> Search(string name, int age, string gender);
    }
}