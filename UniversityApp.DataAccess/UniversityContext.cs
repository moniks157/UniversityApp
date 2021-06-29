using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniversityApp.DataAccess.Configuration;
using UniversityApp.DataAccess.Entities;

namespace UniversityApp.DataAccess
{
    public class UniversityContext : DbContext
    {
        private readonly UniversityDatabaseConfig _config;

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public UniversityContext(IOptions<UniversityDatabaseConfig> config, DbContextOptions<UniversityContext> options) : base(options)
        {
            _config = config.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
