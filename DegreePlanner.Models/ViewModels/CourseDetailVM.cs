using DegreePlanner.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;

namespace DegreePlanner.Models.ViewModels
{
    public class CourseDetailVM
    {
        [ValidateNever]
        public Course Course { get; set; }
        [ValidateNever]
        public IEnumerable<CourseItem> CourseItems { get; set; }
    }
}
