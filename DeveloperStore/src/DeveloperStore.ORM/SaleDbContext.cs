using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DeveloperStore.ORM
{
    public class SaleDbContext : DbContext
    {
        public SaleDbContext(DbContextOptions<SaleDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class SaleDbContextFactory : IDesignTimeDbContextFactory<SaleDbContext>
    {
        public SaleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SaleDbContext>();
            optionsBuilder.UseNpgsql(
                "Server=localhost;Database=DeveloperStore;User Id=admin;Password=admin;TrustServerCertificate=True",
                b => b.MigrationsAssembly("DeveloperStore.ORM")
            );

            return new SaleDbContext(optionsBuilder.Options);
        }
    }
}
