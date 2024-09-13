using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.Models.ViewModels
{
    public class DashboardVM
    {
        // Property to hold the list of terms
        [ValidateNever]
        public IEnumerable<Term> TermsList { get; set; }

        // Property to hold the selected term ID (for dropdown selection)
        [ValidateNever]
        public int? SelectedTermId { get; set; }

        // Property to hold the list of courses for the selected term
        [ValidateNever]
        public IEnumerable<Course> Courses { get; set; }

        // Property to hold the list of course items for the selected course
        [ValidateNever]
        public IEnumerable<CourseItem> CourseItems { get; set; }

    }
}
