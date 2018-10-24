using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    public class AverageViewModel
    {

        [Display(Name = "打数")]
        public string vats { get; set; }
    }
}
