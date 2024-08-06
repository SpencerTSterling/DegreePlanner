using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegreePlanner.Models.ViewModels
{
    public class CourseItemVM
    {
        [ValidateNever]
        public CourseItem CourseItem { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CourseList { get; set; }

        // Predefined list of CourseItem types
        [ValidateNever]
        public List<SelectListItem> TypeList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "To-Do", Value = "To-Do" },
            new SelectListItem { Text = "Assignment", Value = "Assignment" },
            new SelectListItem { Text = "Quiz", Value = "Quiz" },
            new SelectListItem { Text = "Project", Value = "Project" },
            new SelectListItem { Text = "Exam", Value = "Exam" },
            new SelectListItem { Text = "Lecture", Value = "Lecture" },
            new SelectListItem { Text = "Lab", Value = "Lab" },
            new SelectListItem { Text = "Reading", Value = "Reading" },
            new SelectListItem { Text = "Discussion", Value = "Discussion" },
            new SelectListItem { Text = "Homework", Value = "Homework" },
            new SelectListItem { Text = "Practice", Value = "Practice" },
            new SelectListItem { Text = "Extra Credit", Value = "Extra Credit" }
        };
    }
}
