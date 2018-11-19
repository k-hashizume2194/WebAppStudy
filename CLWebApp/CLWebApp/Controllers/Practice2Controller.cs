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
    /// 勝率計算画面コントローラー
    /// </summary>
    [Authorize]
    public class Practice2Controller : Controller
    {
        private Practice2Service _service;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Practice2Controller()
        {
            _service = new Practice2Service();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Winning()
        {
            Practice2ViewModel model = new Practice2ViewModel();
            // 画面初期化
            _service.Clear(model);

            return View(model);
        }


        // POST: Home/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Practice2ViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Victory == "")
                {
                    // エラーの場合
                    ModelState.AddModelError(string.Empty, "勝利数を入力してください");
                }
                else
                {
                    // 正常の場合
                    return RedirectToAction("Winning");
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
        public IActionResult Calc(Practice2ViewModel model)
        {
            if (ModelState.IsValid)
            {
                string victoryString = model.Victory;
                string defeatString = model.Defeat;
                string drawString = model.Draw;

                double victorydouble = double.Parse(model.Victory);
                double defeatdouble = double.Parse(model.Defeat);

                //// 入力チェックを行う
                //// 入力チェック結果を取得
                string message = _service.InputCheck(victoryString,defeatString);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    // エラーの場合
                    ModelState.AddModelError(string.Empty, message);
                }
                else
                {
                    // 勝利数、敗戦数、引き分け数がゼロの場合、"-" を表示
                    if (victorydouble == 0 && defeatdouble == 0 && defeatdouble == 0)
                    {
                        model.Winning = "-";
                    }
                    else
                    {
                        // 勝率を計算
                        double winningDouble = _service.WinPercentagealcalc(victorydouble, defeatdouble);

                        // 試合数があり、勝利数がゼロの場合、".000" を表示
                        if (victorydouble == 0)
                        {
                            model.Winning = ".000";
                        }
                        else
                        {
                            //// 計算結果をテキストボックスにセット
                            model.Winning = winningDouble.ToString("F3");

                        }
                    }
                }
            }
            ////// 計算結果をテキストボックスにセット
            return View("Winning", model);
        }





        /// <summary>
        /// 計算処理(Ajax)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CalcAjax(Practice2ViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                string victoryString = model.Victory;
                string defeatString = model.Defeat;
                string drawString = model.Draw;
            string winningStr = "";

                double victorydouble = double.Parse(model.Victory);
                double defeatdouble = double.Parse(model.Defeat);

                //// 入力チェックを行う
                //// 入力チェック結果を取得
                string message = _service.InputCheck(victoryString, defeatString);
                if (!string.IsNullOrWhiteSpace(message))
                {
                    // エラーの場合
                    // ModelState.AddModelError(string.Empty, message);

                    // 処理結果がエラーであることとexceptionメッセージをJsonで返却
                    return Json(new
                    {
                        status = "Error",
                        message = $"記録処理でエラーが発生しました\r\n({message})"
                    });

                }
                else
                {
                    // 勝利数、敗戦数、引き分け数がゼロの場合、"-" を表示
                    if (victorydouble == 0 && defeatdouble == 0 && defeatdouble == 0)
                    {
                        winningStr  = "-";
                    }
                    else
                    {
                        // 勝率を計算
                        double winningDouble = _service.WinPercentagealcalc(victorydouble, defeatdouble);

                        // 試合数があり、勝利数がゼロの場合、".000" を表示
                        if (victorydouble == 0)
                        {
                            winningStr = ".000";
                        }
                        else
                        {
                            //// 計算結果をテキストボックスにセット
                            winningStr = winningDouble.ToString("F3");

                        }
                    }
            }
        //}
            ////// 計算結果をテキストボックスにセット
            return Json(new
            {
                status = "success",
                message = "計算処理が完了しました",
                winning = winningStr
            });
        }

    }
}