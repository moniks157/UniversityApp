using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;

namespace UniversityAPI.Services.Intefaces
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(int id);
        void AddStudent(Student student);
        bool UpdateStudent(int id, Student student);
        bool RemoveStudent(int id);
        IEnumerable<Student> Search(string firstName, string lastName, int age, string gender);
    }
}
