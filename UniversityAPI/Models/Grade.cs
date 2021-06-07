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
        [Range(2,5)]
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
