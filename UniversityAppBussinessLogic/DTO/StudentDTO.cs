using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAppBussinessLogic.DTO
{
    public class StudentDTO
    {
        private const string gender_regex = @"\b[kKmM]\b";

        public string FirstName;

        public string LastName;

        [Range(2,5)]
        public int Age;

        [StringLength(1)]
        [RegularExpression(gender_regex)]
        public string Gender;
    }
}
