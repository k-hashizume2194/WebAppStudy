using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    public class ResultViewModel
    {

        [Display(Name = "あなたのBMI:")]
        public string bmi { get; set; }

        [Display(Name = "あなたの診断結果")]
        public string diagnosis { get; set; }

    }
}
