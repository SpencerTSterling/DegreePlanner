using DegreePlanner.DataAccess.Repository;
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

            // Define a GUID for the seeded user
            string userId = Guid.NewGuid().ToString();
            // Seed User data
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = userId,
                    UserName = "studenttester1@gmail.com",
                    NormalizedUserName = "STUDENTTESTER1@GMAIL.COM",
                    Email = "studenttester1@gmail.com",
                    NormalizedEmail = "STUDENTTESTER1@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "TestPassword123!"), // Seed with hashed password
                    SecurityStamp = Guid.NewGuid().ToString("D"),
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
                    UserId = userId // FK to the seeded user
                }
            );


        }


    }
}
