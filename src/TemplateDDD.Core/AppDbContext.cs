using Microsoft.EntityFrameworkCore;
using TemplateDDD.Data;
using TemplateDDD.Data.Entities;

namespace TemplateDDD.Core
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Just for development: Remove for production
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public DbSet<MonitoringFailedHNITxn> MonitoringFailedHNITxns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ModuleEntryPoint).Assembly);
        }

        private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}