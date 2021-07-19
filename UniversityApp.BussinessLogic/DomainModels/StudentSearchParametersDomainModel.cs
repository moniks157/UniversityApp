using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityApp.BussinessLogic.DomainModels
{
    public class StudentSearchParametersDomainModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
