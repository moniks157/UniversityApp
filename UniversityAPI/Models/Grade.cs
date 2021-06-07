using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Models
{
    public class Grade
    {
        [Required]
        public int studentId { get; set; }
        [Required]
        [Range(2,5)]
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
