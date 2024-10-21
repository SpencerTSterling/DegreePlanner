using DegreePlanner.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreePlanner.Models
{
    // This model includes data annotations for validation:
    // - Required attributes ensure essential fields are filled.

    public class CourseItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; } // Foreign key
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public string Type { get; set; } // To-Do, Assessment, etc

        [Required]
        [ForeignKey("CourseId")]
        public Course Course { get; set; } // Navigation property

    }


}
