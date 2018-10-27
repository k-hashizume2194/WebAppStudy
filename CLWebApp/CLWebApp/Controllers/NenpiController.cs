using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Data;
using CLWebApp.Models.ViewModels;
using CLWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{

    /// <summary>
    /// 燃費計算画面コントローラー
    /// </summary>
    [Authorize]
    public class NenpiController : Controller
    {
        private nenpiService _service;
        private readonly ApplicationDbContext _context;

        public NenpiController(ApplicationDbContext context)
        {
            _service = new nenpiService();
            _context = context;
        }

        public IActionResult Index()
        {
            NenpiViewModel model = new NenpiViewModel();

            model.boxOilingQuantity = "123";

            Console.WriteLine("(before)model.boxOilingQuantity：" + model.boxOilingQuantity);

            // 画面初期化
            _service.Clear(model, _context);


            Console.WriteLine("(after)model.boxOilingQuantity：" + model.boxOilingQuantity);

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

        /// <summary>
        /// 計算処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Calc(NenpiViewModel model)
        {
            string oiling = model.boxOilingQuantity;
            string mileageVal = model.thisMileage;

            if (ModelState.IsValid)
            {
                /////1.給油量の入力チェックを行う
                //// 入力チェック結果を取得
                string message = _service.CheckOilingQuantity(oiling);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    //    // ⇒入力チェックの結果、エラーがあれば
                    //    //メッセージをダイアログに出し、給油量テキストボックスにフォーカスし、処理終了
                    //    MessageBox.Show(message);
                    // エラーの場合
                    ModelState.AddModelError(string.Empty, message);
                    //ActiveControl = model.boxOilingQuantity;
                    //    return;
                }
            }
            else
            {
                ///2-1.燃費計算
                ///区間燃費 ＝ 区間距離 / 給油量
                double oilingdouble = double.Parse(oiling);
                double valThisMileage = double.Parse(mileageVal);
                double nenpi = _service.Calcnenpi(oilingdouble, valThisMileage);

                //// 3.燃費計算結果をテキストボックスにセット
                model.fuelConsumption = nenpi.ToString("#0.0");

                /////4.「クリア」「記録」「終了」ボタン以外の入力部品を変更不可状態にする。
                //dateTimePicker.Enabled = false;
                //boxOilingQuantity.Enabled = false;
                //txtCurrentMileage.Enabled = false;
                //btnCalculation.Enabled = false;
            }
            return View("Index", model);
        }

        /// <summary>
        /// 区間走行距離算出
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetKukan(NenpiViewModel model)
        {

            //1.給油時総走行距離の入力チェックを行う																																	
            string kyuyuzitMileage = model.currentMileage;
            double zenkaiMileage = double.Parse(model.pastMileage);

            //   1 - 1.未入力チェック
            //   ・未入力の場合：以下の処理を実行して処理終了
            if (!string.IsNullOrWhiteSpace(kyuyuzitMileage))
            {
                //nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
                //	・入力がある場合：1 - 2へ
            }
            else
            {
                //「区間距離」テキストボックスに空白を設定
                //「計算」ボタンをクリック不可状態にする
                model.currentMileage = "";
                model.btnCalculationEnabled = false;
                return View("Index", model);
            }
            // 0以上の数値、整合性チェック
            string messagekyuyuzi = _service.CheckCurrentMileage(kyuyuzitMileage, zenkaiMileage);

            if (!string.IsNullOrWhiteSpace(messagekyuyuzi))
            {
                //エラーだと給油時走行距離にフォーカスを設定して空白セット、
                //区間距離空白と計算ボタン不可設定してメッセージ表示
                model.currentMileage = "";
                model.btnCalculationEnabled = false;
                //this.ActiveControl = this.txtCurrentMileage;
                //txtCurrentMileage.Text = "";
                //// フォーカスイベントなのでメッセージボックスを最後に配置
                //MessageBox.Show(messagekyuyuzi);
                ModelState.AddModelError(string.Empty, messagekyuyuzi);
                return View("Index", model);
            }

            //区間距離を算出して「区間距離」テキストボックスに設定
            double kyuyuzidouble = double.Parse(kyuyuzitMileage);
            double kukankyori = _service.KukanCul(kyuyuzidouble, zenkaiMileage);
            model.currentMileage = kukankyori.ToString();
            ////計算ボタンにフォーカス
            //this.ActiveControl = this.btnCalculation;

            //2 - 2.計算ボタンをクリック可能にする
            model.btnCalculationEnabled = true;

            return View("Index", model);
        }

        /// <summary>
        /// 記録処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Record(NenpiViewModel model)
        {

            /////1.区間燃費算出済チェックを行う
            /////1-1.未算出チェック
            /////「区間燃費」が空白の場合にエラーとする
            /////メッセージ：「記録処理は区間燃費の算出後に実行してください」
            /////をダイアログに表示して処理終了
            /////⇒入力チェックの結果、エラーが無ければ2へ
            //if (string.IsNullOrWhiteSpace(txtFuelConsumption.Text))
            //{
            //    MessageBox.Show("記録処理は区間燃費の算出後に実行してください");
            //    return;
            //}

            /////2.記録処理実行確認ダイアログ表示
            /////メッセージ：「記録処理を実行します。よろしいですか？」
            /////の確認ダイアログを表示
            /////→「はい」をクリックした場合、3へ
            /////→「いいえ」をクリックした場合処理終了
            //DialogResult result = MessageBox.Show("記録処理を実行します。よろしいですか？", "確認", MessageBoxButtons.YesNo);
            //if (result == DialogResult.Yes)
            //{
            //    //TODO:「はい」をクリックした場合、3へ
            //}
            //else
            //{
            //    return;
            //}

            //// 燃費データ保存
            //// 燃費記録DB(SQLite)の燃費記録用テーブルに対してINSERT文を実行してデータを登録

            ////データ保存
            ////DBファイルの燃費記録テーブルに対してINSERT文を実行してデータを登録
            //string db_file = "nenpi.db";

            /////給油日付 「給油日入力部品」の入力値をYYYYMMDD形式に変換
            //string fuelDay = dateTimePicker.Value.ToString("yyyyMMdd");
            /////給油時総走行距離	「給油時総走行距離」の値
            //double dCurrentMileage = double.Parse(txtCurrentMileage.Text);
            /////区間走行距離 「区間走行距離」の値
            //double dThisMileage = double.Parse(txtThisMileage.Text);
            /////区間燃費 「区間燃費」の値
            //double dFuelConsumption = double.Parse(txtFuelConsumption.Text);

            //using (SQLiteConnection nenpiData = new SQLiteConnection("Data Source=" + db_file))
            //{
            //    nenpiData.Open();

            //    using (var transaction = nenpiData.BeginTransaction())
            //    {
            //        using (SQLiteCommand command = nenpiData.CreateCommand())
            //        {
            //            command.CommandText = "insert into t_nenpi(refuel_date, mileage, trip_mileage, fuel_cost) values (@refuel_date, @mileage, @trip_mileage, @fuel_cost)";
            //            command.Parameters.Add(new SQLiteParameter("@refuel_date", fuelDay));
            //            command.Parameters.Add(new SQLiteParameter("@mileage", dCurrentMileage));
            //            command.Parameters.Add(new SQLiteParameter("@trip_mileage", dThisMileage));
            //            command.Parameters.Add(new SQLiteParameter("@fuel_cost", dFuelConsumption));

            //            command.ExecuteNonQuery();
            //        }
            //        transaction.Commit();
            //    }
            //}


            /////・記録処理完了メッセージの表示
            /////メッセージ：「記録処理が完了しました」													
            /////をダイアログに表示して処理終了
            //MessageBox.Show("記録処理が完了しました", "");
            ////画面をクリアする
            //Clear();

            // TODO:結果を返す
            return Json(new { status = "success" });
        }
    }
}