using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public NenpiController()
        {
            _service = new nenpiService();
        }

        public IActionResult Index()
        {
            NenpiViewModel model = new NenpiViewModel();

            // 画面初期化
            _service.Clear(model);

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
            if (ModelState.IsValid)
            {
                string oiling = model.boxOilingQuantity;
                string mileageVal = model.thisMileage;

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
                else
                {
                    /////2-1.燃費計算
                    /////区間燃費 ＝ 区間距離 / 給油量
                    double oilingdouble = double.Parse(model.boxOilingQuantity);
                    double valThisMileage = double.Parse(model.currentMileage);
                    double nenpi = _service.Culcnenpi(oilingdouble, valThisMileage);


                    //// 3.燃費計算結果をテキストボックスにセット
                    model.fuelConsumption = nenpi.ToString("#0.0");

                    /////4.「クリア」「記録」「終了」ボタン以外の入力部品を変更不可状態にする。   
                    //dateTimePicker.Enabled = false;
                    //boxOilingQuantity.Enabled = false;
                    //txtCurrentMileage.Enabled = false;
                    //btnCalculation.Enabled = false;

                    ///windows formのものをviewmodelに置き換える
                    model.isCalculated = true;
                }
            }

            // TODO:区間燃費が正しく反映されないのでreturnの方法を再考する

            return View("Index", model);
        }

        /// <summary>
        /// 記録処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Record(NenpiViewModel viewModel)
        {
            // TODO:処理の実装は別課題で実施
            return View(viewModel);
        }
    }
}
