using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApp.DTOs
{
    public class StudentSearchParametersDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }

        [Required()]
        public int PageNumber { get; set; }
        [Required()]
        [Range(Constants.MIN_PAGE_SIZE, Constants.MAX_PAGE_SIZE)]
        public int PageSize { get; set; }
    }
}
