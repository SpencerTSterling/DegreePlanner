using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.Models.ViewModels
{
    public class DegreePlanVM
    {
        [ValidateNever]
        public IEnumerable<TermWithCourses> TermsWithCourses { get; set; }
    }

    public class TermWithCourses
    {
        [ValidateNever]
        public Term Term { get; set; }
        [ValidateNever]
        public IEnumerable<Course> Courses { get; set; }
    }
}
