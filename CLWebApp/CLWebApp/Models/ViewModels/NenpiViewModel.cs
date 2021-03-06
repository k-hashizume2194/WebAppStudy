﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.ViewModels
{
    //[CustomValidation(typeof(NenpiViewModel), "CheckCurrentMileage")]

    /// <summary>
    /// 燃費計算画面ViewModel
    /// </summary>
    public class NenpiViewModel : IValidatableObject
    {

        /// <summary>
        /// 給油日
        /// </summary>
        [Required(ErrorMessage = "給油日を入力してください")]
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
		[Range(0.1, 9999999.9, ErrorMessage = "給油時走行距離は0より大きい数値(最大：9999999.9)で入力してください")]
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

        /// <summary>
        /// 計算完了判定
        /// </summary>
        public bool isCalculated { get; set; }

        //給油時走行距離入力チェックメソッド
        //public static ValidationResult CheckCurrentMileage(NenpiViewModel model)
        //{
        //    if (double.Parse(model.currentMileage) == 0)
        //    {
        //        return new ValidationResult("給油時走行距離は0より大きい数値で入力してください");
        //    }
        //    if (double.Parse(model.currentMileage) <= double.Parse(model.pastMileage))
        //    {
        //        return new ValidationResult("給油時総走行距離は前回の距離より大きな値で入力してください");
        //    }

        //    return ValidationResult.Success;
        //}

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (double.Parse(currentMileage) == 0)
            {
                yield return new ValidationResult("給油時走行距離は0より大きい数値で入力してください", new[] { "currentMileage" });
            }
            if (double.Parse(currentMileage) <= double.Parse(pastMileage))
            {
                yield return new ValidationResult("給油時総走行距離は前回の距離より大きな値で入力してください", new[] { "currentMileage" });
            }
        }

    }
}
