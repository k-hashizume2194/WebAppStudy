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
    /// 打率計算画面コントローラー
    /// </summary>
    [Authorize]
    public class AverageController : Controller
    {
        // 打率計算サービス
        private AverageService _service;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public AverageController()
        {
           _service = new AverageService();
        }

        /// <summary>
        /// 画面初期表示
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            AverageViewModel model = new AverageViewModel();
            // 画面初期化
            _service.Clear(model);
            return View(model);
        }

        /// <summary>
        /// 計算処理
        /// </summary>
        /// <param name="model">打率計算画面ViewModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calc(AverageViewModel model)
        {
            if (ModelState.IsValid)
            {
                string batsVal = model.Bats;
                string hitsVal = model.Hits;
                bool chkMou = model.Mou;

                // 入力チェック結果を取得
                string message = _service.InputCheck(batsVal, hitsVal);

                // ⇒入力チェックの結果、エラーがあればメッセージをダイアログに出して処理終了
                if (!string.IsNullOrWhiteSpace(message))
                {
                    //MessageBox.Show(message);
                    //return;
                    // エラーの場合
                    ModelState.AddModelError(string.Empty, message);
                }
                else
                {
                    // 打数がゼロの場合、"-" を表示
                    if ("0".Equals(batsVal))
                    {
                        model.Average = "-";
                    }
                    // 打数がゼロ以外の場合
                    // 打率計算を実施       
                    else
                    {
                        // 打数,安打数を数値変換 ※入力値チェックが完了しているので数値変換時のエラーの心配なし
                        double batsDoubleVal = double.Parse(batsVal);
                        double hitsDoubleVal = double.Parse(hitsVal);

                        // 打率計算結果を取得
                        double averageVal = _service.CalcAverage(batsDoubleVal, hitsDoubleVal, chkMou);

                        // 打率表示整形メソッドの結果をstring型に代入
                        string aveCharacterString = _service.AveFormat(averageVal, chkMou);
                        // 打率表示テキストボックスのテキストに代入
                        model.Average = aveCharacterString;
                    }
                }
            }
            return View("Index",model);
        }
    }
}