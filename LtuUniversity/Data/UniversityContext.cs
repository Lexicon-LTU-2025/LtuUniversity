using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LtuUniversity.Models.Entities;
using LtuUniversity.Data.Configurations;

namespace LtuUniversity.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext (DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;
       // public DbSet<Assignment> Assignments { get; set; } = default!;
        //public DbSet<Address> Address { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfigurations());
            modelBuilder.ApplyConfiguration(new EnrollmentConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries<Student>().Where(e => e.State == EntityState.Modified))
            {
                entry.Property("Edited").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
