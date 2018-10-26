using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{

    /// <summary>
    /// 燃費計算画面ViewModel
    /// </summary>
    public class NenpiViewModel
    {

        /// <summary>
        /// 給油日
        /// </summary>
        [Display(Name = "給油日：")]
        public string dataTimePicker { get; set; }

        /// <summary>
        /// 給油量
        /// </summary>
        [Required(ErrorMessage = "給油量を入力してください")]
        [RegularExpression(@"^\d{1,3}(\.\d)?$", ErrorMessage = "給油量は正の数値(小数点以下一桁まで)で入力してください")]
        [Display(Name = "給油量：")]
        public string boxOilingQuantity { get; set; }

        /// <summary>
        /// 前回給油時走行距離
        /// </summary>
        [Display(Name = "前回給油時走行距離：")]
        public string pastMileage { get; set; }

        /// <summary>
        /// 給油時走行距離
        /// </summary>
        [RegularExpression(@"^\d{1,7}(\.\d)?$", ErrorMessage = "給油時走行距離は正の数値(小数点以下一桁まで)で入力してください")]
        [Display(Name = "給油時走行距離：")]
        public string currentMileage { get; set; }

        /// <summary>
        /// 区間走行距離
        /// </summary>
        [Display(Name = "区間走行距離：")]
        public string thisMileage { get; set; }

        /// <summary>
        /// 区間燃費
        /// </summary>
        //[Required(ErrorMessage = "記録処理は区間燃費の算出後に実行してください")]
        [Display(Name = "区間燃費：")]
        public string fuelConsumption { get; set; }


        /// <summary>
        /// 計算ボタンクリック状態管理
        /// </summary>
        public bool btnCalculationEnabled { get; set; }
    }
}
