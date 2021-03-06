﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Data;
using CLWebApp.Models;
using CLWebApp.Models.ViewModels;
using CLWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CLWebApp.Controllers
{
    /// <summary>
    /// 勝率計算画面コントローラー
    /// </summary>
    [Authorize]
    public class Practice2Controller : CLBaseController
    {
        private readonly ApplicationDbContext _context;
        private Practice2Service _service;
        private List<ParkingInfo> _parkingInfoList = new List<ParkingInfo>();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Practice2Controller(ApplicationDbContext context)
        {
            _service = new Practice2Service();
            _context = context;
            // パーキング情報DBからデータを取り出す
            _parkingInfoList = _context.ParkingInfos.ToList();
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


        /// <summary>
        /// 駐車場情報初期表示
        /// </summary>
        /// <returns></returns>
        public IActionResult ParkingRecordCreate()
        {
            // 駐車場情報リスト設定
            SetParkingInfoSelectListToViewBag();
            return View();
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
                string victoryString = model.Victory;
                string defeatString = model.Defeat;
                string drawString = model.Draw;
                string winningStr = "";

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
                        message = "入力してください"
                    });

                }
                else
                {
                    double victorydouble = double.Parse(model.Victory);
                    double defeatdouble = double.Parse(model.Defeat);

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
            ////// 計算結果をテキストボックスにセット
            return Json(new
            {
                status = "success",
                message = "計算処理が完了しました",
                winning = winningStr
            });
        }


        /// <summary>
        /// 勝率判定処理(Ajax)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult WinAjax(Practice2ViewModel model)
        {
            string victoryString = model.Victory;
            string defeatString = model.Defeat;
            string drawString = model.Draw;
            string winningStr = "";

            //// 入力チェックを行う
            //// 入力チェック結果を取得
            string message = _service.InputCheck(victoryString, defeatString);
            if (!string.IsNullOrWhiteSpace(message))
            {
                // 処理結果がエラーであることとexceptionメッセージをJsonで返却
                return Json(new
                {
                    //status = "Error",
                    message = "入力してください"
                });
            }
            else
            {
                // 勝利数、敗戦数、引き分け数がゼロの場合、"-" を表示
                double victorydouble = double.Parse(model.Victory);
                double defeatdouble = double.Parse(model.Defeat);
                if (victorydouble == 0 && defeatdouble == 0 && defeatdouble == 0)
                {
                    winningStr = "-";
                    return Json(new
                    {
                        message = "試合数がありません",
                        winning = winningStr
                    });
                }
                else
                {
                    // 勝率を計算
                    double winningDouble = _service.WinPercentagealcalc(victorydouble, defeatdouble);

                    // 試合数があり、勝利数がゼロの場合
                    if (victorydouble == 0)
                    {
                        winningStr = ".000";
                        return Json(new
                        {
                            message = "まだ勝ちがありません",
                            winning = winningStr
                        });

                    }
                    else
                    {
                        //// 計算結果をテキストボックスにセット
                        winningStr = winningDouble.ToString("F3");
                        // 勝率5割未満の場合
                        if(winningDouble < 0.500)
                        return Json(new
                        {
                            message = "勝率5割以下です",
                            winning = winningStr
                        });
                        // 勝率5割以上,7割未満
                        if (winningDouble >=0.500 && winningDouble < 0.700)
                            return Json(new
                            {
                                message = "良い成績です",
                                winning = winningStr
                            });
                        // 勝率7割以上
                        if (winningDouble >= 0.700)
                            return Json(new
                            {
                                message = "すばらしい成績です",
                                winning = winningStr
                            });
                    }
                }
            }
            //}
            ////// 計算結果をテキストボックスにセット


            return Json(new
            {
                //message = "勝率判定",
                winning = winningStr
            });
        }


        /// <summary>
        /// 記録処理
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Record(Practice2ViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //トランザクション開始
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // ユーザー情報をとる
                        var user = GetLoginUser(_context);

                        // 追加する燃費レコードのモデルを作成
                        WinningRecord model = new WinningRecord();
                        model.Victory = int.Parse(viewModel.Victory);
                        model.Defeat = int.Parse(viewModel.Defeat);
                        model.Draw = int.Parse(viewModel.Draw);
                        model.WinningPercentage = double.Parse(viewModel.Winning);

                        // 引数に指定したエンティティをデータベースに追加
                        _context.Add(model);

                        // データベースに変更が反映
                        _context.SaveChanges();

                        // データベースの更新内容が有効
                        transaction.Commit();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, "エラーが発生しました。");
                    }
                }
            }

            return View("Winning", viewModel);
        }

        /// <summary>
        /// 駐車場情報情報をViewBagに設定
        /// </summary>
        /// <param name="context"></param>
        private void SetParkingInfoSelectListToViewBag()
        {
            var parkingInfo = _parkingInfoList.OrderBy(c => c.ParkingName).Select(x => new { Id = x.Id, Value = x.ParkingName });
            ViewBag.ParkInfo = new SelectList(parkingInfo, "Id", "Value");
        }


        /// <summary>
        /// 駐車場情報を表示(Ajax)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult ParkingInfoDisplayAjax(long? userid)
        {
            // ユーザー情報がない場合
            if (userid == null)
            {
                return NotFound();
            }

            // パーキング情報リストから、選択された"userid"と同じパーキング情報リストの"Id"のデータを取得
            var parkingInfo = _parkingInfoList.Find(x => x.Id == userid);

            // パーキング情報がない場合
            if (parkingInfo == null)
            {
                return NotFound();
            }

            // 取得データをテキストボックスにセット
            return Json(new
            {
                status = "success",
                parkingInfomation = parkingInfo
            });
        }


        /// <summary>
        /// 駐車場情報を各ボタンの条件指定で表示 (Ajax)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult ParkingCalc(string btn)
        {
            var parkingInfo = new ParkingInfo();

            // btnデータが "null"の場合
            if (btn == null)
            {
                return NotFound();
            }

            // 最小値ボタンを押下した場合
            if ("minBtn".Equals(btn))
            {
                // パーキング情報リストから、"TimeRate"の最小値を取得
                var minRate = _parkingInfoList.Min(record => record.TimeRate);
                // パーキング情報リストの"TimeRate"の値と、パーキング情報の"TimeRate"の最小値が一致するレコードを取得
                parkingInfo = _parkingInfoList.Find(x => x.TimeRate == minRate);
            }
            // 最大値ボタンを押下した場合
            if ("maxBtn".Equals(btn))
            {
                // パーキング情報リストから、"MaxFee"の最大値を取得
                parkingInfo = _parkingInfoList.OrderByDescending(record => record.MaxFee).First();
            }
            // 平均ボタンを押下した場合
            if ("aveBtn".Equals(btn))
            {
                // パーキング情報リストから、"TimeRate"の平均値を取得
                var aveTimeRate = _parkingInfoList.Average(record => record.TimeRate);
                // パーキング情報リストから、"Fee"の平均値を取得
                var aveFee = _parkingInfoList.Average(record => record.Fee);
                // パーキング情報リストから、"MaxFee"の平均値を取得
                var aveMaxFee = _parkingInfoList.Average(record => record.MaxFee);

                // parkingInfoの各項目にデータをセット
                parkingInfo.ParkingName = "平均データ";
                parkingInfo.TimeRate = (int)aveTimeRate;
                parkingInfo.Fee = (int)aveFee;
                parkingInfo.MaxFee = (int)aveMaxFee;
                parkingInfo.Location = "";
            }
            // 取得データをテキストボックスにセット
            return Json(new
            {
                status = "success",
                parkingInfomation = parkingInfo
            });
        }
    }
}