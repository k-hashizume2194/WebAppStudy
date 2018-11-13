using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Data;
using CLWebApp.Models;
using CLWebApp.Models.ViewModels;
using CLWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{

    /// <summary>
    /// 燃費計算画面コントローラー
    /// </summary>
    [Authorize]
    public class NenpiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private NenpiService _service;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">DBコンテキスト</param>
        public NenpiController(ApplicationDbContext context)
        {
            _service = new NenpiService();
            // コンテキストDI
            _context = context;
        }

        public IActionResult Index()
        {
            // ログイン中のユーザー情報
            string userName = User.Identity.Name;
            var user = _context.Users.Where(p => p.UserName.Equals(userName)).First();

            NenpiViewModel model = new NenpiViewModel();

            // 画面初期化
            _service.Clear(model, _context, user);

            return View(model);
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
				// POST後の画面再描画のため状態をクリア
				ModelState.Clear();

				string oiling = model.boxOilingQuantity;
                string mileageVal = model.thisMileage;

                /////1.給油量の入力チェックを行う
                //// 入力チェック結果を取得
                string message = _service.CheckOilingQuantity(oiling);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    // エラーの場合
                    ModelState.AddModelError(string.Empty, message);
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
                    model.isCalculated = true;
				}
            }

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
