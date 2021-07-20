using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityApp.DTOs
{
    public class StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsAdult { get; set; }
        public string Gender { get; set; }
    }
}
