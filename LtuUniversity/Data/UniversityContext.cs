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
       // public DbSet<Assignment> Assignments { get; set; } = default!;
        //public DbSet<Address> Address { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Student>().HasData(
            //    new Student { Id=1, LastName = "Kalle", FirstName = "Anka", Avatar = "123" },
            //    new Student { Id=2, LastName = "Nissa", FirstName = "Anka", Avatar = "123" },
            //    new Student { Id=3, LastName = "Olof", FirstName = "Anka" , Avatar = "123"  },
            //    new Student { Id=4, LastName = "Anna", FirstName = "Anka" , Avatar = "123"  }
            //    );

            //modelBuilder.Entity<Address>().HasData(
            //    new Address { Id = 1,  City="Stockholm", Street = "Gatan1" , ZipCode = "123", StudentId = 1},
            //    new Address { Id = 2,  City="Stockholm2", Street = "Gatan2" , ZipCode = "123", StudentId = 2},
            //    new Address { Id = 3,  City="Stockholm3", Street = "Gatan3" , ZipCode = "123", StudentId = 3},
            //    new Address { Id = 4,  City="Stockholm4", Street = "Gatan4" , ZipCode = "123", StudentId = 4}
            //    );

            //modelBuilder.Entity<Enrollment>().HasKey(x => new {x.StudentId, x.CourseId});

            modelBuilder.Entity<Student>()
                        .HasOne(s => s.Address)
                        .WithOne(a => a.Student)
                        .HasForeignKey<Address>(a => a.StudentId);

            modelBuilder.Entity<Address>()
                        .HasIndex(a => a.StudentId)
                        .IsUnique(); //This ensures one-to-one

            modelBuilder.Entity<Student>()
               .HasMany(s => s.Courses)
               .WithMany(c => c.Students)
               .UsingEntity<Enrollment>(
               e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
               e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments),
               e => e.HasKey(e => new { e.StudentId, e.CourseId }));
        }
    }
}
