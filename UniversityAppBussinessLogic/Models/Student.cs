using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAppBussinessLogic.Models
{
    class Student
    {
        public string FirstName;
        public string LastName;
        [Range(2,5)]
        public int Age;
        public string Gender;
    }
}
