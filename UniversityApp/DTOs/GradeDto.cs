using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApp.DTOs
{
    public class GradeDto
    {
        [Range(2,5)]
        public int Value { get; set; }
        public string Description { get; set; }
    }
}
