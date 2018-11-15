using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models.ViewModels;

namespace CLWebApp.Services
{
    public class Practice2Service
    {
        public void Clear(Practice2ViewModel model)
        {
            model.Victory = "";
            model.Defeat = "";
            model.Draw = "";
            model.Winning = "";
        }


        /// <summary>
        /// 勝率計算メソッド
        /// </summary>
        /// <param name="victory"></param>
        /// <param name="defeat"></param>
        /// <returns></returns>
        public double WinPercentagealcalc(double victory, double defeat)
        {
            double WinningDouble = victory / (victory + defeat);
            // return Math.Round(aveRounding, 3, MidpointRounding.AwayFromZero);
            return Math.Round(WinningDouble, 3, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 入力チェックメソッド
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string InputCheck(string victory, string defeat)
        {
            /// 未入力チェック
            /// ※空白、ゼロの場合にエラーとする
            if (string.IsNullOrWhiteSpace(victory))
            {
                return "勝利数を入力してください";
            }
            if (string.IsNullOrWhiteSpace(defeat))
            {
                // null、もしくは空文字列、もしくは空白文字列
                return "敗戦数を入力してください";
            }

            //数値かどうかチェック
            /// 正の数値チェック
            //「勝利数」が0以下の数値以外の場合エラーにする
            //double victoryNumber;
            //bool canConvert = double.TryParse(victory, out victoryNumber);

            //if (!canConvert || victoryNumber <= 1)
            //{
            //    return "勝利数は0より大きい数値で入力してください";
            //}
            return "";
        }


    }
}
