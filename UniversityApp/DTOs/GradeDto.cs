using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApp.DTOs
{

    public class GradeDto
    {
        public double Value { get; set; }
        public string Description { get; set; }
        public int StudentId { get; set; }
    }
}
