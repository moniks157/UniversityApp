using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityApp.BussinessLogic.DomainModels
{
    public class GradeDomainModel
    {
        public double Value { get; set; }
        public string Description { get; set; }
        public int StudentId { get; set; }
    }
}
