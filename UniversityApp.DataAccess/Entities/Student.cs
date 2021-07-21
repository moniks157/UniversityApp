using System.Collections.Generic;

namespace UniversityApp.DataAccess.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsAdult { get; set; }
        public string Gender { get; set; }
        public virtual List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
