using System.ComponentModel.DataAnnotations;

namespace DegreePlanner.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Role { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
