using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    public class Practice1ViewModel
    {
        [Range(0.01, 3.00, ErrorMessage = "身長は0より大きい数値(最大：3.00)、入力してください")]
        [Required(ErrorMessage = "身長を入力してください")]
        [Display(Name = "身長(m)：")]
        public string height { get; set; }

        [Range(0.1, 999.9, ErrorMessage = "体重は0より大きい数値(最大：999.9)で入力してください")]
        [Required(ErrorMessage = "体重を入力してください")]
        [Display(Name = "体重(kg)：")]
        public string weight { get; set; }

        [Display(Name = "BMI：")]
        public string bmi { get; set; }

    }
}
