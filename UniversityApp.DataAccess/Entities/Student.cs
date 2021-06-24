﻿using System.Collections.Generic;

namespace UniversityApp.DataAccess.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; } = 18;
        public string Gender { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();
    }
}
