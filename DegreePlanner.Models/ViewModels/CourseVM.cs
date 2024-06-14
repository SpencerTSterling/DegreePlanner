using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DegreePlanner.Models.ViewModels
{
    public class CourseVM
    {
        [ValidateNever]
        public Course Course { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> TermList { get; set; }
    }
}
