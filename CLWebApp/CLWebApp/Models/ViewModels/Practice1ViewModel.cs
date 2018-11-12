using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    public class Practice1ViewModel
    {
        [Required(ErrorMessage = "身長を入力してください")]
        [Display(Name = "身長：")]
        public string height { get; set; }

        [Required(ErrorMessage = "体重を入力してください")]
        [Display(Name = "体重：")]
        public string weight { get; set; }

        [Display(Name = "BMI：")]
        public string bmi { get; set; }

    }
}
