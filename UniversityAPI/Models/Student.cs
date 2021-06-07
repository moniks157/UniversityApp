using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniversityAPI.Models
{
    public class Student
    {
        private const string gender_regex = @"\b[kKmM]\b";

        [Required]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        [Range(18,Int32.MaxValue)]
        public int Age { get; set; }

        [StringLength(1)]
        [RegularExpression(gender_regex)]
        public string Gender { get; set; }

        public ICollection<Grade> Grades { get; set; }
    }
}
