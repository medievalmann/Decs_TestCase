using ConfigurationManager.Core.Models;
using ConfigurationManager.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationManager.Data
{
    public class ConfigurationManagerDbContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Configuration> Configurations { get; set; }

        public ConfigurationManagerDbContext(DbContextOptions<ConfigurationManagerDbContext> options)
            : base(options)
        { }

        public ConfigurationManagerDbContext(string connectionString) : base(GetOptions(connectionString))
        { }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ApplicationConfiguration());

            builder
                .ApplyConfiguration(new ConfigurationConfiguration());
        }
    }
}
