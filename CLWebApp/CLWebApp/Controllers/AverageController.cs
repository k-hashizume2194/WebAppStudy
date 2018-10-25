using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models.ViewModels;
using CLWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{
	public class AverageController : Controller
	{

		/// <summary>
		/// 打率計算画面サービス
		/// </summary>
		private readonly AverageService service;

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="service"></param>
		public AverageController()
		{
			// 打率計算画面サービスのインスタンスを生成
			// TODO:DI(Depenency Injection[依存性の注入])設定できるはず
			service = new AverageService();
		}

		/// <summary>
		/// 画面表示処理
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			AverageViewModel model = new AverageViewModel();
			// ViewModelに画面初期表示時の内容をセット
			model = service.Clear(model);
			return View(model);
		}

		/// <summary>
		/// 打率計算処理
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Calc(AverageViewModel model)
		{
			string batsVal = model.Bats;
			string hitsVal = model.Hits;

			// 入力チェック結果を取得(※大小チェック以外は発生しない)
			string message = service.InputCheck(batsVal, hitsVal);

			if (!string.IsNullOrWhiteSpace(message))
			{
				// エラー表示領域にメッセージを設定
				ModelState.AddModelError(string.Empty, message);

				// ここまででエラーがあれば画面にメッセージを表示して処理中断
				if (!ModelState.IsValid)
				{
					// 画面を再表示
					return View("Index", model);
				}
			}

			// 打数がゼロの場合、"-" を表示
			if ("0".Equals(batsVal))
			{
				model.Average = "-";
			}
			else
			{
				// 打数,安打数を数値変換 ※入力値チェックが完了しているので数値変換時のエラーの心配なし
				double batsDoubleVal = double.Parse(batsVal);
				double hitsDoubleVal = double.Parse(hitsVal);

				double averageVal = service.CalcAverage(batsDoubleVal, hitsDoubleVal);

				// 打率表示整形メソッドの結果をstring型に代入
				string aveCharacterString = service.AveFormat(averageVal);
				model.Average = aveCharacterString;
			}

			// 画面を再表示
			return View("Index", model);
		}
	}
}