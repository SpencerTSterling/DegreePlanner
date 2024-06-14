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
    }
}
