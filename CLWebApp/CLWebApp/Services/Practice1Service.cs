using CLWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Services
{
    public class Practice1Service
    {

        public void Clear(Practice1ViewModel model)
        {
            model.measuringdate = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            model.height = "";
            model.weight = "";
            model.bmi = "";
            model.btnCalculationEnabled = false;
            model.btnResultEnabled = false;
        }

        /// <summary>
        /// BMI計算メソッド
        /// </summary>
        /// <param name="heightDouble"></param>
        /// <param name="weightDouble"></param>
        /// <returns></returns>
        public double CalcBmi(double heightDouble, double weightDouble)
        {
            double bmiDouble = weightDouble / (heightDouble * heightDouble);
            return Math.Round(bmiDouble, 1, MidpointRounding.AwayFromZero);
        }
    }
}
