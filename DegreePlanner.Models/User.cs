using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DegreePlanner.Models
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Major { get; set; } = "Undecided";

        // Navigation property for related terms
        public ICollection<Term>? Terms { get; set; }
    }
}
