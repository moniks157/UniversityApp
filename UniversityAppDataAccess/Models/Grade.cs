﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDataAccess.Models
{
    class Grade
    {
        public int Id { get; set; }
        public int Vallue { get; set; }
        public string Description { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}