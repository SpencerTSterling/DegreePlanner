using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

    }
}
