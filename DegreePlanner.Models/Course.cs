using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DegreePlanner.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TermId { get; set; } // Foreign key
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayName("End  Date")]
        public DateTime EndDate { get; set; }
        [Required]
        public string Status { get; set; }
        public int? FacultyId { get; set; } // Foreign key

        [ForeignKey("TermId")]
        public Term Term { get; set; } // Navigation property
        [ForeignKey("FacultyId")]
        public Faculty? Faculty { get; set; } // Navigation property
        public ICollection<CourseItem>? CourseItems { get; set; }
    }
}
