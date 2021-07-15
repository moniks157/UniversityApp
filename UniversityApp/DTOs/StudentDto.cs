using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityApp.DTOs
{
    public class StudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Range(Constants.MIN_AGE, Constants.MAX_AGE)]
        public int Age { get; set; }
        [StringLength(1)]
        [RegularExpression(Constants.GENDER_REGEX)]
        public string Gender { get; set; }
    }
}
