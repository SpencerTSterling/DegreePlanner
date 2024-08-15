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
                        EndDate = new DateTime(2024, 12, 31), // Dec 31, 2024

                        UserId = "9a59c8a6-5231-4647-96cb-b3fe84a85dfe" // ID of the user studenttester1@gmail.com
                }
                );
        }


    }
}
