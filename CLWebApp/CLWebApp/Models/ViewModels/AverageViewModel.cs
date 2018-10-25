using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    public class AverageViewModel
    {
        [Required(ErrorMessage = "打数を入力してください")]
        [Display(Name = "打数：")]
        public string bats { get; set; }

        [Required(ErrorMessage = "安打数を入力してください")]
        [Display(Name = "安打数：")]
        public string hits { get; set; }

        [Display(Name = "毛まで算出：")]
        public bool Mou { get; set; }

        [Display(Name = "打率：")]
        public string average { get; set; }
    }
}
