﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{
    public class NenpiController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "燃費計算アプリ";
            NenpiViewModel model = new NenpiViewModel();
            return View(model);
        }


        // POST: Home/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(NenpiViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.boxOilingQuantity == "")
                {
                    // エラーの場合
                    ModelState.AddModelError(string.Empty, "給油量を入力してください");
                }
                else
                {
                    // 正常の場合
                    return RedirectToAction("Index");
                }
            }
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Calc(NenpiViewModel model)
        {
            

            //string oiling = boxOilingQuantity.Text;
            //string mileageVal = txtThisMileage.Text;

            /////1.給油量の入力チェックを行う
            //// 入力チェック結果を取得
            //string message = CheckOilingQuantity(oiling);
            //if (!string.IsNullOrWhiteSpace(message))
            //{
            //    // ⇒入力チェックの結果、エラーがあれば
            //    //メッセージをダイアログに出し、給油量テキストボックスにフォーカスし、処理終了
            //    MessageBox.Show(message);
            //    this.ActiveControl = this.boxOilingQuantity;
            //    return;
            //}

            /////2-1.燃費計算
            /////区間燃費 ＝ 区間距離 / 給油量
            //double oilingdouble = double.Parse(oiling);
            //double valThisMileage = double.Parse(mileageVal);
            //double nenpi = Culcnenpi(oilingdouble, valThisMileage);

            //// 3.燃費計算結果をテキストボックスにセット
            //txtFuelConsumption.Text = nenpi.ToString("#0.0");

            /////4.「クリア」「記録」「終了」ボタン以外の入力部品を変更不可状態にする。
            //dateTimePicker.Enabled = false;
            //boxOilingQuantity.Enabled = false;
            //txtCurrentMileage.Enabled = false;
            //btnCalculation.Enabled = false;

            return View(model);
        }
        #region privateメソッド

        /// <summary>
        /// 入力値クリアメソッド
        /// </summary>
        private void Clear()
        {
            //給油日: 現在日付
            //給油量:空白
            //前回給油時総走行距離:DBに記録されている最後の給油時走行距離
            //給油時総走行距離:空白
            //区間走行距離:空白
            //区間燃費:空白
            //計算ボタン:クリック不可状態
            //dateTimePicker.Value = DateTime.Now;
            //boxOilingQuantity.Text = "";
            //txtCurrentMileage.Text = "";
            //txtThisMileage.Text = "";
            //txtFuelConsumption.Text = "";
            //btnCalculation.Enabled = false;
            ////計算時に変更不可にした給油日、給油量、給油時走行距離を入力可に戻す
            //dateTimePicker.Enabled = true;
            //boxOilingQuantity.Enabled = true;
            //txtCurrentMileage.Enabled = true;

            ////前回給油時総走行距離取得メソッドを実行
            //double zenkai = GetzenkaiFromdb();

            //// 前回給油時走行距離が"0"の場合、前回給油時走行距離に"0.0"を表示
            //if (zenkai == 0)
            //{
            //    txtPastMileage.Text = "0.0";
            //}
            //else
            //{
            //    txtPastMileage.Text = zenkai.ToString();
            //}
        }

        /// <summary>
        /// 前回走行距離取得メソッド
        /// </summary>
        /// <returns>DBから取得した前回走行距離</returns>
        private double GetzenkaiFromdb()
        {
            // 前回給油時走行距離変数
            string pastMileageStr = "0"; // TODO:スタブなのでゼロ固定

            //using (SQLiteConnection nenpiData = new SQLiteConnection("Data Source=" + db_file))
            //{
            //    //燃費記録DB(SQLite)を作成する
            //    nenpiData.Open();

            //    using (SQLiteCommand command = nenpiData.CreateCommand())
            //    {
            //        // 燃費記録DB(SQLite)に燃費記録用テーブルがなければ、燃費記録用テーブル「t_nenpi」を作成
            //        command.CommandText = "CREATE TABLE if not exists t_nenpi(id INTEGER  PRIMARY KEY AUTOINCREMENT, refuel_date TEXT, mileage REAL, trip_mileage REAL, fuel_cost REAL)";
            //        command.ExecuteNonQuery();


            //        // 燃費記録用テーブル「t_nenpi」から以下の条件でレコード抽出
            //        // 条件：給油日(refuel_date)が直近(一番最近)かつ給油時走行距離(mileage)が最新
            //        command.CommandText = "SELECT * FROM t_nenpi  order by refuel_date desc, mileage desc";
            //        using (SQLiteDataReader reader = command.ExecuteReader())
            //        {
            //            // 給油時走行距離(mileage)のデータを燃費記録DB(SQLite)から呼び出す
            //            if (reader.Read())
            //            {
            //                //燃費記録用テーブルから給油時走行距離(mileage)を取得できた場合、"pastMileageStr"に代入
            //                pastMileageStr = reader["mileage"].ToString();
            //            }
            //            else
            //            {
            //                // 燃費記録用テーブル内に登録データがない場合、前回給油時走行距離に「"0"」を返却
            //                pastMileageStr = "0";
            //            }
            //        }
            //    }
            //}
            //前回給油時走行距離テキストをdouble型に変換
            double latestMileage = double.Parse(pastMileageStr);

            //呼び出し元に戻り値として返す
            return latestMileage;
        }

        /// <summary>
        /// 燃費計算メソッド
        /// </summary>
        /// <param name="oilingdouble">給油量</param>
        /// <param name="valThisMileage">区間距離</param>
        /// <returns>区間燃費</returns>
        private double Culcnenpi(double oilingdouble, double valThisMileage)
        {
            ///燃費計算メソッド
            ///引数１：給油量 (double)
            ///引数２：区間距離 (doule)
            ///戻り値：区間燃費 (double) ※30.5のように小数値で返す ※小数点第二位で四捨五入する
            double nenpiDouble = valThisMileage / oilingdouble;
            Console.WriteLine(nenpiDouble);
            return Math.Round(nenpiDouble, 1, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 給油時走行距離入力チェックメソッド
        /// </summary>
        /// <param name="kyuyuzitMileage">給油時総走行距離</param>
        /// <param name="zenkaiMileage">前回給油時総走行距離</param>
        /// <returns>メッセージ</returns>
        private string CheckCurrentMileage(string kyuyuzitMileage, double zenkaiMileage)
        {
            //   1 - 2.正の数値チェック
            //	「給油時総走行距離」がゼロ以下のの数値以外の場合
            //　※数値以外の文字、マイナスの数値をエラーにする
            double kyuyuzitMileagenumber;
            bool canConvert = double.TryParse(kyuyuzitMileage, out kyuyuzitMileagenumber);
            if (!canConvert || kyuyuzitMileagenumber <= 0)
            {
                return "給油走行距離は0より大きい数値で入力してください";
            }

            ////   1 - 3.整合性チェック
            ////　「給油時総走行距離」＜=「前回給油時総走行距離」の場合
            if (kyuyuzitMileagenumber <= zenkaiMileage)
            {
                return "給油時総走行距離は前回の距離より大きな値で入力してください";
            }
            return "";
        }

        /// <summary>
        /// 区間距離計算メソッド
        /// </summary>
        /// <param name="kyuyuzitMileage">給油時総走行距離</param>
        /// <param name="zenkaiMileage">前回給油時総走行距離</param>
        /// <returns>区間距離</returns>
        private double KukanCul(double kyuyuzitMileage, double zenkaiMileage)
        {
            // 区間距離を計算して小数点第二位で四捨五入する
            double kukanMileage = kyuyuzitMileage - zenkaiMileage;
            Console.WriteLine(kukanMileage);
            return Math.Round(kukanMileage, 1, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 給油量未入力チェックメソッド
        /// </summary>
        /// <param name="oiling">給油量</param>
        /// <returns>メッセージ</returns>
        private string CheckOilingQuantity(string oiling)
        {
            ///※給油時総走行距離のチェックがOKの場合のみ計算ボタンはクリックできる
            ///1-1.未入力チェック
            ///※空白、ゼロの場合にエラーとする
            if (!string.IsNullOrWhiteSpace(oiling))
            {
                //nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
            }
            else
            {
                // null、もしくは空文字列、もしくは空白文字列
                return "給油量を入力してください";
            }

            //数値かどうかチェック
            ///1-2.正の数値チェック
            ///「給油量」が0以下の数値以外の場合エラーにする
            double oilingNumber;
            bool canConvert = double.TryParse(oiling, out oilingNumber);

            if (!canConvert || oilingNumber <= 0)
            {
                return "給油量は0より大きい数値で入力してください";
            }
            return "";
        }

        #endregion
    }
}