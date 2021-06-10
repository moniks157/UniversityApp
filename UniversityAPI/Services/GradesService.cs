using System.Collections.Generic;
using System.Linq;
using UniversityAPI.Models;
using UniversityAPI.Services.Intefaces;

namespace UniversityAPI.Services
{
    public class GradesService : IGradesService
    {
        private readonly IStudentsService _studentService;

        public GradesService(IStudentsService studentService)
        {
            _studentService = studentService;
        }

        public Grade AddGrade(int studentId, Grade grade)
        {
            var student = _studentService.GetStudent(studentId);
            
            if(student == null)
            {
                return null;
            }

            grade.Id = student.Grades.DefaultIfEmpty(new Grade { Id = 0, Value = 2, Description = "" }).Last().Id + 1;

            student.Grades.Add(grade);
            return grade;
        }

        public bool DeleteGrade(int studentId, int gradeId)
        {
            var student = _studentService.GetStudent(studentId);

            if (student == null)
            {
                return false;
            }

            var gradeToRemove = student.Grades.Find(grade => grade.Id == gradeId);
            return student.Grades.Remove(gradeToRemove);
        }

        public List<Grade> GetGrades(int id)
        {
            var student = _studentService.GetStudent(id);

            if(student == null)
            {
                return null;
            }
            return student.Grades;
        }

        public bool UpdateGrade(int studentId, int gradeId, Grade grade)
        {
            var student = _studentService.GetStudent(studentId);

            if(student == null)
            {
                return false;
            }

            var gradeToUpdate = student.Grades.Find(grade => grade.Id == gradeId);
                
            if(gradeToUpdate != null)
            {
                gradeToUpdate.Value = grade.Value;
                gradeToUpdate.Description = grade.Description;

                return true;
            }
           
            return false;
        }
    }
}
