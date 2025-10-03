using DeloitteIntegration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DeloitteIntegration.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.State).HasMaxLength(100);
                entity.Property(c => c.Country).IsRequired().HasMaxLength(100);
            });
        }
    }
}
