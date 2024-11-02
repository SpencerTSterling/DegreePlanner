using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreePlanner.Models
{
    // This model includes data annotations for validation:
    // - Required attributes ensure essential fields are filled.
    // - MaxLength limits the length of the Name property.
    // - DataType specifies the expected format for date fields.

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
        public string UserId { get; set; }

        // Navigation property for the User
        public User User { get; set; }

    }
}
