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

        [Required(ErrorMessage = "給油量を入力してください")]
        [Display(Name = "給油量：")]
        public string boxOilingQuantity { get; set; }

        [Display(Name = "前回給油時走行距離：")]
        public string pastMileage { get; set; }

        [Display(Name = "給油時走行距離：")]
        public string currentMileage { get; set; }

        [Display(Name = "区間走行距離：")]
        public string thisMileage { get; set; }

        [Required(ErrorMessage = "記録処理は区間燃費の算出後に実行してください")]
        [Display(Name = "区間燃費：")]
        public string fuelConsumption { get; set; }
    }
}
