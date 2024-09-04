using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreePlanner.Models
{
    public class Term
    {
        [Key]
        public int Id { get; set; }
        [Required, DisplayName("Term Name"), MaxLength(30)]

        public string Name { get; set; }
        [Required(ErrorMessage = "Start date is required"), DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }


        [Required(ErrorMessage = "End date is required"), DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Navigation property for related courses
        public ICollection<Course>? Courses { get; set; }


        // Foreign key for the User
        [Required]
        [ForeignKey("UserId")]// Make it required to ensure a Term is always linked to a User
        public string UserId { get; set; }

        // Navigation property for the User
        public User User { get; set; }

    }
}
