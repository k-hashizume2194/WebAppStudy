using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    public class NenpiViewModel
    {
        [Display(Name = "給油日：")]
        public string dataTimePicker { get; set; }

        [Display(Name = "給油量：")]
        public string boxOilingQuantity { get; set; }
    }
}
