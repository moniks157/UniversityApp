using Microsoft.EntityFrameworkCore;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.DataAccess
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=UniversityAppData");
        }
    }
}
