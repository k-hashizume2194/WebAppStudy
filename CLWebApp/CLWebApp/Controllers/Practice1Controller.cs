using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Data;
using CLWebApp.Models;
using CLWebApp.Models.ViewModels;
using CLWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{
    public class Practice1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private Practice1Service _service;


        /// <summary>
        /// コンストラクター
        /// </summary>
        public Practice1Controller(ApplicationDbContext context)
        {
            _service = new Practice1Service();
            _context = context;
        }

        public IActionResult Index()
        {
            Practice1ViewModel model = new Practice1ViewModel();
            _service.Clear(model);

            return View(model);
        }


        /// <summary>
        /// 計算処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Calc(Practice1ViewModel model)
        {
            // POST後の画面再描画のため状態をクリア
            // TODO:if (ModelState.IsValid) で囲む
            ModelState.Clear();

            double heightDouble = double.Parse(model.height);
            double weightDouble = double.Parse(model.weight);
            double bmi = _service.CalcBmi(heightDouble, weightDouble);

            model.bmi = bmi.ToString();

            model.btnCalculationEnabled = true;
            model.isCalculated = true;

            return View("Index", model);

        }
        /// <summary>
        /// 記録処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        //[HttpPost]
        //public IActionResult Record(Practice1ViewModel viewModel)
        //{
        //    // TODO:処理の実装
        //    return View(viewModel);
        //}


        [HttpPost]
        public async Task<IActionResult> Record(Practice1ViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //トランザクション開始
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                    BmiRecord model = new BmiRecord();

                        // 記録処理を記述              
                        // TODO ユーザー情報は後で

                        //areaOrDivision.Id = Guid.NewGuid();
                        // viewModelからModelに値を渡す
                        model.Bmi = double.Parse(viewModel.bmi);

                        // 引数に指定したエンティティをデータベースに追加
                        _context.Add(model);

                        // データベースに変更が反映
                        _context.SaveChanges();

                        // データベースの更新内容が有効
                        transaction.Commit();

                        // Indexに戻る
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        // データベースの更新内容が無効
                        transaction.Rollback();
                        //_context.RevertChanges();

                        //上部にエラーを赤文字で表示
                        ModelState.AddModelError(string.Empty, "エラーが発生しました。");
                        
                        return View("Index");
                    }
                }
            }
            return View("Index", viewModel);
        }


        /// <summary>
        /// 診断結果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Result(Practice1ViewModel model)
        {
            // ResultViewModelのインスタンス生成
            ResultViewModel resultViewModel = new ResultViewModel();
            // Practice1の値をResultに当てはめる
            resultViewModel.yourbmi = model.bmi;
            // 値を渡す
            return View(resultViewModel);
        }
    }
}