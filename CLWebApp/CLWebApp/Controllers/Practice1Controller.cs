﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Data;
using CLWebApp.Models;
using CLWebApp.Models.ViewModels;
using CLWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLWebApp.Controllers
{
    public class Practice1Controller : CLBaseController
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


        /// <summary>
        /// BMI計算ページ
        /// </summary>
        /// <returns></returns>
        public IActionResult Bmi()
        {
            Practice1ViewModel model = new Practice1ViewModel();
            _service.Clear(model);

            return View(model);
        }


        /// <summary>
        /// Indexページ(橋詰練習)
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 計算処理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Calc(Practice1ViewModel model)
        {
            if (ModelState.IsValid)
            {
                // POST後の画面再描画のため状態をクリア
                ModelState.Clear();

                double heightDouble = double.Parse(model.height);
                double weightDouble = double.Parse(model.weight);
                double bmi = _service.CalcBmi(heightDouble, weightDouble);

                model.bmi = bmi.ToString();

                //model.btnCalculationEnabled = true;
                model.isCalculated = true;
            }
            return View("Bmi", model);
        }

        /// <summary>
        /// 計算処理(Ajax)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CalcAjax(Practice1ViewModel model)
        {
                double heightDouble = double.Parse(model.height);
                double weightDouble = double.Parse(model.weight);
                double bmi = _service.CalcBmi(heightDouble, weightDouble);

            // BMIの値をstring型へ変換
            var bmiStr = bmi.ToString();

            return Json(new
            {
                //計算結果とhiddenの値を返す
                status = "success",
                result = bmiStr,
                hiddenCalc = false,
                hiddenRec = true
            });
        }


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
                        // ログイン中のユーザー情報をとる
                        //string userName = User.Identity.Name;
                        //var user = _context.Users.Where(p => p.UserName.Equals(userName)).First();
                        var user = GetLoginUser(_context);
                        // viewModelからModelに値を渡す
                        model.User = user;
                        model.BmiDate = DateTime.Parse(viewModel.measuringdate);
                        model.Height = double.Parse(viewModel.height);
                        model.Weight = double.Parse(viewModel.weight);
                        model.Bmi = double.Parse(viewModel.bmi);

                        // 引数に指定したエンティティをデータベースに追加
                        _context.Add(model);

                        // データベースに変更が反映
                        _context.SaveChanges();

                        // データベースの更新内容が有効
                        transaction.Commit();

                        // Bmiに戻る
                        return RedirectToAction(nameof(Bmi));
                    }
                    catch (Exception ex)
                    {
                        // データベースの更新内容が無効
                        transaction.Rollback();

                        //上部にエラーを赤文字で表示
                        ModelState.AddModelError(string.Empty, "エラーが発生しました。");                      
                    }
                }
            }
            return View("Bmi", viewModel);
        }

        /// <summary>
		/// 記録処理(Ajax)
		/// </summary>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[HttpPost]
        public IActionResult RecordAjax(Practice1ViewModel viewModel)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // ログイン中のユーザー情報
                    var user = GetLoginUser(_context);

                    // 追加する燃費レコードのモデルを作成
                    BmiRecord model = new BmiRecord();
                    model.User = user;
                    model.BmiDate = DateTime.Parse(viewModel.measuringdate);
                    model.Height = double.Parse(viewModel.height);
                    model.Weight = double.Parse(viewModel.weight);
                    model.Bmi = double.Parse(viewModel.bmi);

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


        // GET: BmiRecords
        /// <summary>
        /// BMIリストページ
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> BmiList()
        {
            return View(await _context.BmiRecords.ToListAsync());
        }

    }
}