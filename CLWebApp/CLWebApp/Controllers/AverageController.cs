using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{
    public class AverageController : Controller
    {
        public IActionResult Index()
        {
            AverageViewModel model = new AverageViewModel();
            // 画面初期化
            Clear(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Calc(AverageViewModel model)
        {
            //string batsVal = batsText.Text;
            //string hitsVal = hitsText.Text;
            //bool chkMou = checkBoxMou.Checked;

            //// 入力チェック結果を取得
            //string message = InputCheck(batsVal, hitsVal);

            //// ⇒入力チェックの結果、エラーがあればメッセージをダイアログに出して処理終了
            //if (!string.IsNullOrWhiteSpace(message))
            //{
            //    MessageBox.Show(message);
            //    return;
            //}

            //// 打数がゼロの場合、"-" を表示
            //if ("0".Equals(batsVal))
            //{
            //    averageText.Text = "-";
            //}
            //// 打数がゼロ以外の場合
            //// 打率計算を実施       
            //else
            //{
            //    // 打数,安打数を数値変換 ※入力値チェックが完了しているので数値変換時のエラーの心配なし
            //    double batsDoubleVal = double.Parse(batsVal);
            //    double hitsDoubleVal = double.Parse(hitsVal);

            //    // 打率計算結果を取得
            //    double averageVal = CalcAverage(batsDoubleVal, hitsDoubleVal, chkMou);

            //    // 打率表示整形メソッドの結果をstring型に代入
            //    string aveCharacterString = AveFormat(averageVal, chkMou);
            //    // 打率表示テキストボックスのテキストに代入
            //    averageText.Text = aveCharacterString;
            //}
            return View("Index",model);
        }
        #region privateメソッド

        /// <summary>
        /// クリアメソッド
        /// </summary>
        /// <param name="model">打率計算画面ViewModel</param>
        private void Clear(AverageViewModel model)
        {
            model.bats = "0";
            model.hits = "0";
            model.average = "";
            model.Mou = false;
        }

        /// <summary>
        /// 入力チェックメソッド
        /// </summary>
        /// <param name="batsVal">打数値</param>
        /// <param name="hitsVal">安打数値</param>
        /// <returns>打数・安打数が空白の場合 メッセージ：打数、安打数を両方入力してください、
        ///          打数の入力内容が正の整数以外の場合 メッセージ：打数、安打数は正の整数で入力してください
        ///          マイナスのの数値の場合 メッセージ：打数、安打数は正の整数で入力してください
        ///          「打数」＜「安打数」の場合 メッセージ：安打数は打数以下の値を入力してください</returns>
        private string InputCheck(string batsVal, string hitsVal)
        {
            // 未入力チェック
            //「打数」が空白の場合
            if (string.IsNullOrWhiteSpace(batsVal))
            {
                // null、もしくは空文字列、もしくは空白文字列である
                // メッセージ：「打数、安打数を両方入力してください」をダイアログに表示して処理終了
                return "打数、安打数を両方入力してください";
            }

            //「安打数」が空白の場合
            if (string.IsNullOrWhiteSpace(hitsVal))
            {
                // null、もしくは空文字列、もしくは空白文字列である
                // メッセージ：「打数、安打数を両方入力してください」をダイアログに表示して処理終了
                return "打数、安打数を両方入力してください";
            }

            // 正の整数チェック
            //「打数」の入力内容が正の整数以外の場合
            // ※文字、小数点付きの数値をエラーにする
            // メッセージ：「打数、安打数は正の整数で入力してください」をダイアログに表示して処理終了
            int batsNumber = 0;
            bool canConvert = int.TryParse(batsVal, out batsNumber);
            if (!canConvert)
            {
                return "打数、安打数は正の整数で入力してください";
            }

            // 「安打数」の入力内容が正の整数以外の場合
            // ※文字、小数点付きの数値をエラーにする
            // メッセージ：「打数、安打数は正の整数で入力してください」をダイアログに表示して処理終了
            int hitsNumber = 0;

            canConvert = int.TryParse(hitsVal, out hitsNumber);
            if (!canConvert)
            {
                return "打数、安打数は正の整数で入力してください";
            }

            // ※マイナス数値をエラーにする
            // メッセージ：「打数、安打数は正の整数で入力してください」をダイアログに表示して処理終了

            if (batsNumber < 0 || hitsNumber < 0)
            {
                return "打数、安打数は正の整数で入力してください";

            }

            // 整合性チェック
            //「打数」＜「安打数」の場合
            // メッセージ：「安打数は打数以下の値を入力してください」

            if (batsNumber < hitsNumber)
            {
                return "安打数は打数以下の値を入力してください";
            }

            // ここまで来たのでエラーなし＝空白を返却
            return "";
        }

        /// <summary>
        /// 打率計算メソッド
        /// </summary>
        /// <param name="batNum">打数</param>
        /// <param name="hitNum">安打数</param>
        /// <param name="chkMou">毛まで計算するかのチェック有無</param>
        /// <returns>打率(小数点第四or五位で四捨五入)</returns>
        private double CalcAverage(double batNum, double hitNum, bool chkMou)
        {
            // 打率を計算
            double aveRounding = hitNum / batNum;

            if (chkMou)
            {
                // 毛チェックボックスにチェックがある場合、
                // 打率を小数点第五位で四捨五入し、打率計算メソッドの戻り値として返す
                return Math.Round(aveRounding, 4, MidpointRounding.AwayFromZero);
            }
            else
            {
                // 毛チェックボックスにチェックがない場合
                // 打率を小数点第四位で四捨五入し、打率計算メソッドの戻り値として返す
                return Math.Round(aveRounding, 3, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// 打率表示整形メソッド
        /// </summary>
        /// <param name="averageVal">打率(小数値)</param>
        /// <param name="chkMou">毛まで計算するかのチェック有無</param>
        /// <returns>
        ///     <para>chkMou:trueの場合、打率(小数値)を「x 割 y 分 z 厘 a 毛」書式で整形した文字列</para>     
        ///     <para>chkMou:falseの場合、打率(小数値)を「x 割 y 分 z 厘」書式で整形した文字列</para>
        /// </returns>
        private string AveFormat(double averageVal, bool chkMou)
        {
            if (chkMou)
            {
                // 毛チェックボックスにチェックが入っているとき
                // 書式フォーマット
                string formatStr = "{0} 割 {1} 分 {2} 厘 {3} 毛";

                // 打率数値をdouble型に代入
                double ave = averageVal;
                // ave == 1 の時、「10 割 0 分 0 厘 0 毛」と表示
                if (ave == 1)
                {
                    return string.Format(formatStr, 10, 0, 0, 0);
                }
                // ave == 0 の時、「0 割 0 分 0 厘 0 毛」と表示
                else if (ave == 0)
                {
                    return string.Format(formatStr, 0, 0, 0, 0);
                }

                // 打率数値をゼロ埋めし、string型に代入
                string aveText = string.Format("{0:f4}", ave);

                // 打率数値をテキストにし、文字列の配置で数値を取り出す
                char ave1 = aveText[2];
                char ave2 = aveText[3];
                char ave3 = aveText[4];
                char ave4 = aveText[5];
                //「x 割 y 分 z 厘 a 毛」に表示整形し、打率表示整形メソッドの戻り値として返す
                return string.Format(formatStr, ave1, ave2, ave3, ave4);
            }
            else
            {
                // 毛チェックボックスにチェックが入っていないとき
                // 書式フォーマット
                string formatStr = "{0} 割 {1} 分 {2} 厘";

                // 打率数値をdouble型に代入
                double ave = averageVal;
                // ave == 1 の時、「10 割 0 分 0 厘」と表示
                if (ave == 1)
                {
                    return string.Format(formatStr, 10, 0, 0);
                }
                // ave == 0 の時、「0 割 0 分 0 厘」と表示
                else if (ave == 0)
                {
                    return string.Format(formatStr, 0, 0, 0);
                }

                // 打率数値をゼロ埋めし、string型に代入
                string aveText = string.Format("{0:f3}", ave);

                // 打率数値をテキストにし、文字列の配置で数値を取り出す
                char ave1 = aveText[2];
                char ave2 = aveText[3];
                char ave3 = aveText[4];
                //「x 割 y 分 z 厘」に表示整形し、打率表示整形メソッドの戻り値として返す
                return string.Format(formatStr, ave1, ave2, ave3);
            }
        }

        #endregion
    }
}