﻿using DegreePlanner.DataAccess.Repository;
using DegreePlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace capstone.DegreePlanner.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseItem> CourseItems { get; set; }

        // Seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create this default Term
            modelBuilder.Entity<Term>().HasData(
                new Term
                    {
                        Id = 1,
                        Name = "Term 1",
                        StartDate = new DateTime(2024, 9, 1), // Sept 1, 2024
                        EndDate = new DateTime(2024, 12, 31) // Dec 31, 2024
                    }
                );
        }


    }
}
