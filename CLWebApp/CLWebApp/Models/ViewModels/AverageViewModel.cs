using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{

    /// <summary>
    /// 打率計算画面ViewModel
    /// </summary>
    public class AverageViewModel
    {
        /// <summary>
        /// 打数
        /// </summary>
        [Required(ErrorMessage = "打数を入力してください")]
        [Display(Name = "打数：")]
        public string bats { get; set; }

        /// <summary>
        /// 安打数
        /// </summary>
        [Required(ErrorMessage = "安打数を入力してください")]
        [Display(Name = "安打数：")]
        public string hits { get; set; }

        /// <summary>
        /// 毛まで算出
        /// </summary>
        [Display(Name = "毛まで算出：")]
        public bool Mou { get; set; }

        /// <summary>
        /// 打率
        /// </summary>
        [Display(Name = "打率：")]
        public string average { get; set; }
    }
}
