using CLWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Services
{
	/// <summary>
	/// 打率計算画面のサービスクラス
	/// </summary>
	public class AverageService
	{
		/// <summary>
		/// クリアメソッド
		/// </summary>
		public AverageViewModel Clear(AverageViewModel model)
		{
			model.Bats = "0";
			model.Hits = "0";
			model.Average = "";
			return model;
		}

		/// <summary>
		/// 打率表示整形メソッド
		/// </summary>
		/// <param name="averageVal">打率(小数値)</param>
		/// <returns>打率(小数値)を「x 割 y 分 z 厘」書式で整形した文字列</returns>
		public string AveFormat(double averageVal)
		{
			// 書式フォーマット
			string formatStr = "{0} 割 {1} 分 {2} 厘";

			// 打率数値をdouble型に代入
			double ave = averageVal;
			// ave == 1 の時、「10 割 0 分 0 厘」と表示
			if (ave == 1)
			{
				return string.Format(formatStr, 10, 0, 0);
			}
			// ave == 0 の時、「0 割 0 分 0 厘」と表示
			else if (ave == 0)
			{
				return string.Format(formatStr, 0, 0, 0);
			}

			// 打率数値をゼロ埋めし、string型に代入
			string aveText = string.Format("{0:f3}", ave);

			// 打率数値をテキストにし、文字列の配置で数値を取り出す
			char ave1 = aveText[2];
			char ave2 = aveText[3];
			char ave3 = aveText[4];
			//「x 割 y 分 z 厘」に表示整形し、打率表示整形メソッドの戻り値として返す
			return string.Format(formatStr, ave1, ave2, ave3);
		}

		/// <summary>
		/// 打率計算メソッド
		/// </summary>
		/// <param name="batNum">打数</param>
		/// <param name="hitNum">安打数</param>
		/// <returns>打率(小数点第四位で四捨五入)</returns>
		public double CalcAverage(double batNum, double hitNum)
		{
			// 打率を計算
			double aveRounding = hitNum / batNum;
			// 打率を小数点第四位で四捨五入し、打率計算メソッドの戻り値として返す
			return Math.Round(aveRounding, 3, MidpointRounding.AwayFromZero);
		}

		/// <summary>
		/// 入力チェックメソッド
		/// </summary>
		/// <param name="batsVal">打数値</param>
		/// <param name="hitsVal">安打数値</param>
		/// <returns>打数・安打数が空白の場合 メッセージ：打数、安打数を両方入力してください、
		///          打数の入力内容が正の整数以外の場合 メッセージ：打数、安打数は正の整数で入力してください
		///          マイナスのの数値の場合 メッセージ：打数、安打数は正の整数で入力してください
		///          「打数」＜「安打数」の場合 メッセージ：安打数は打数以下の値を入力してください</returns>
		public string InputCheck(string batsVal, string hitsVal)
		{
			// 未入力チェック
			//「打数」が空白の場合
			if (!string.IsNullOrWhiteSpace(batsVal))
			{
				//nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
			}
			else
			{
				// null、もしくは空文字列、もしくは空白文字列である
				// メッセージ：「打数、安打数を両方入力してください」をダイアログに表示して処理終了
				return "打数、安打数を両方入力してください";
			}

			//「安打数」が空白の場合
			if (!string.IsNullOrWhiteSpace(hitsVal))
			{
				// nullではなく、かつ空文字列でもなく、かつ空白文字列でもない
			}
			else
			{
				// null、もしくは空文字列、もしくは空白文字列である
				// メッセージ：「打数、安打数を両方入力してください」をダイアログに表示して処理終了
				return "打数、安打数を両方入力してください";
			}

			// 正の整数チェック
			//「打数」の入力内容が正の整数以外の場合
			// ※文字、小数点付きの数値をエラーにする
			// メッセージ：「打数、安打数は正の整数で入力してください」をダイアログに表示して処理終了
			int batsNumber = 0;
			bool canConvert = int.TryParse(batsVal, out batsNumber);
			if (!canConvert)
			{
				return "打数、安打数は正の整数で入力してください";
			}

			// 「安打数」の入力内容が正の整数以外の場合
			// ※文字、小数点付きの数値をエラーにする
			// メッセージ：「打数、安打数は正の整数で入力してください」をダイアログに表示して処理終了
			int hitsNumber = 0;

			canConvert = int.TryParse(hitsVal, out hitsNumber);
			if (!canConvert)
			{
				return "打数、安打数は正の整数で入力してください";
			}


			// ※マイナス数値をエラーにする
			// メッセージ：「打数、安打数は正の整数で入力してください」をダイアログに表示して処理終了

			if (batsNumber < 0 || hitsNumber < 0)
			{
				return "打数、安打数は正の整数で入力してください";

			}

			// 整合性チェック
			//「打数」＜「安打数」の場合
			// メッセージ：「安打数は打数以下の値を入力してください」

			if (batsNumber < hitsNumber)
			{
				return "安打数は打数以下の値を入力してください";
			}

			// ここまで来たのでエラーなし＝空白を返却
			return "";
		}
	}
}
