﻿using DegreePlanner.DataAccess.Repository;
using DegreePlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace capstone.DegreePlanner.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }

        // Define the DbSets for the entities to manage them in the database
        public DbSet<Term> Terms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseItem> CourseItems { get; set; }

        // Seeding datas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Create default user
            Guid userId = Guid.NewGuid(); // Generate a new GUID
            
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = userId.ToString(), // Use the GUID as a string for the Id
                    UserName = "studenttester1@gmail.com",
                    NormalizedUserName = "STUDENTTESTER1@GMAIL.COM",
                    Email = "studenttester1@gmail.com",
                    NormalizedEmail = "STUDENTTESTER1@GMAIL.COM",

                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAIAAYagAAAAEE8qTehN67DNoAM/JbRrzB62HT9mvPxZCyXdMmfeSwavCnwaULe/hFmDVRWNSzBZIg==",
                    SecurityStamp = "CULID4DV2H7E6SHABGQOE27Y7JCATJLE",
                    FirstName = "",
                    LastName = "",
                    Major = ""

                }
            );

            // Seed Term data with FK relationship to the User
            modelBuilder.Entity<Term>().HasData(
                new Term

                    {
                        Id = 1,
                        Name = "Term 1",
                        StartDate = new DateTime(2024, 9, 1), // Sept 1, 2024
                        EndDate = new DateTime(2024, 12, 31), // Dec 31, 2024

                        UserId = userId.ToString() // Assign the GUID to UserId

                }
            );


        }


    }
}
