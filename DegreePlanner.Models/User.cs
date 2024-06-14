using System.ComponentModel.DataAnnotations;

namespace DegreePlanner.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        public string Major { get; set; } = "Undecided";
    }
}
