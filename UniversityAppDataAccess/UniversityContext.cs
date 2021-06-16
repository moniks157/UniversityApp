using Microsoft.EntityFrameworkCore;
using UniversityDataAccess.Entities;

namespace UniversityDataAccess
{
    public class UniversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=C:\Users\mmaczkowiak\source\repos\university.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>().ToTable("Grades");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
