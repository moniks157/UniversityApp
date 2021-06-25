using Microsoft.EntityFrameworkCore;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.DataAccess
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {

        }
    }
}
