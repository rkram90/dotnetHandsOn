
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyDotnetAPI.Models;

namespace HelloWorld.Data
{

    public class DataContextEF : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Computer> Computer { get; set; }
        private IConfiguration? _config;

        public DataContextEF(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default")
          ?? throw new ArgumentNullException(nameof(config), "Connection string 'default' not found.");
            _config = config;
        }

        //when we create instance of DataContextEF this method is called
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);
            //.ToTable("Computer", "TutorialAppSchema");
            //.ToTable("TableName", "Schema");
        }
    }
}