using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Services
{
    public class Practice1Service
    {

        /// <summary>
        /// BMI計算メソッド
        /// </summary>
        /// <param name="heightDouble"></param>
        /// <param name="weightDouble"></param>
        /// <returns></returns>
        public double CalcBmi(double heightDouble, double weightDouble)
        {
            double bmiDouble = (weightDouble / (heightDouble * heightDouble));
            return(bmiDouble);
            //return Math.Round(bmiDouble, 1, MidpointRounding.AwayFromZero);
        }
    }
}
