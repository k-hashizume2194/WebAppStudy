using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    public class Practice2ViewModel
    {
        /// <summary>
        /// 勝利数
        /// </summary>
        [Required(ErrorMessage = "勝利数を入力してください")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "勝利数は正の整数を入力してください")]
        [Display(Name = "勝利数：")]
        public string Victory { get; set; }

        /// <summary>
        /// 敗戦数
        /// </summary>
        [Required(ErrorMessage = "敗戦数を入力してください")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "敗戦数は正の整数を入力してください")]
        [Display(Name = "敗戦数：")]
        public string Defeat { get; set; }

        /// <summary>
        /// 引き分け
        /// </summary>
        // [Required(ErrorMessage = "引き分け数を入力してください")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "引き分け数は正の整数を入力してください")]
        [Display(Name = "引き分け数：")]
        public string Draw { get; set; }

        /// <summary>
        /// 勝率
        /// </summary>
        [Display(Name = "勝率：")]
        public string Winning { get; set; }


    }
}
