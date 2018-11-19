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
    public class NenpiController : CLBaseController
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
            var user = GetLoginUser(_context);

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
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // ログイン中のユーザー情報
                        var user = GetLoginUser(_context);

                        // 追加する燃費レコードのモデルを作成
                        NenpiRecord model = new NenpiRecord();
                        model.RefuelDate = DateTime.Parse(viewModel.dataTimePicker);
                        model.Mileage = double.Parse(viewModel.currentMileage);
                        model.TripMileage = double.Parse(viewModel.thisMileage);
                        model.FuelCost = Double.Parse(viewModel.fuelConsumption);
                        model.User = user;

                        // コンテキストに追加
                        _context.Add(model);
                        _context.SaveChanges();
                        transaction.Commit();

                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        //_context.RevertChanges();
                        ModelState.AddModelError(string.Empty, "エラーが発生しました。");
                    }
                }
            }

            return View("Index", viewModel);
        }

		/// <summary>
		/// 記録処理(Ajax)
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult RecordAjax(NenpiViewModel viewModel)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
                    // ログイン中のユーザー情報
                    var user = GetLoginUser(_context);

                    // 追加する燃費レコードのモデルを作成
                    NenpiRecord model = new NenpiRecord();
					model.RefuelDate = DateTime.Parse(viewModel.dataTimePicker);
					model.Mileage = double.Parse(viewModel.currentMileage);
					model.TripMileage = double.Parse(viewModel.thisMileage);
					model.FuelCost = Double.Parse(viewModel.fuelConsumption);
					model.User = user;

					// コンテキストに追加
					_context.Add(model);
					_context.SaveChanges();
					transaction.Commit();
				}
				catch (Exception ex)
				{
					// 処理結果がエラーであることとexceptionメッセージをJsonで返却
					return Json(new
					{
						status = "dbError",
						message = $"記録処理でエラーが発生しました\r\n({ex.Message})"
					});
				}
			}

			// 処理結果が成功であることとメッセージをJsonで返却
			return Json(new
			{
				status = "success",
				message = "記録処理が完了しました"
			});
		}
	}
}
