using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DegreePlanner.Models
{
    public class User:IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        public string Major { get; set; } = "Undecided";
    }
}
