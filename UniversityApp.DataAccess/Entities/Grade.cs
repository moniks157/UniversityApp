using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityApp.DataAccess.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
