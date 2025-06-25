using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LtuUniversity.Models.Entities;

namespace LtuUniversity.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext (DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasData(
                new Student { Id=1, LastName = "Kalle", FirstName = "Anka"},
                new Student { Id=2, LastName = "Nissa", FirstName = "Anka"},
                new Student { Id=3, LastName = "Olof", FirstName = "Anka"},
                new Student { Id=4, LastName = "Anna", FirstName = "Anka"}
                );

            //modelBuilder.Entity<Student>().ToTable("Banan");
        }
    }
}
