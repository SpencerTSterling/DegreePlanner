using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.Models.ViewModels
{
    public class TermVM
    {
        [ValidateNever]
        public Term Term { get; set; }

    }
}
