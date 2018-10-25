using System.ComponentModel.DataAnnotations;

namespace CLWebApp.Models.ViewModels
{
	/// <summary>
	/// 打率計算画面ViewModel
	/// </summary>
	public class AverageViewModel
    {
		/// <summary>
		/// 打数
		/// </summary>
		[Required(ErrorMessage = "打数は空にできません")]
		[RegularExpression(@"[0-9]+", ErrorMessage = "打数は半角数字で入力してください")]
		[Display(Name = "打数")]
		public string Bats { get; set; }

		/// <summary>
		/// 安打数
		/// </summary>
		[Required(ErrorMessage = "安打数は空にできません")]
		[RegularExpression(@"[0-9]+", ErrorMessage = "安打は半角数字で入力してください")]
		[Display(Name = "安打数")]
		public string Hits { get; set; }

		/// <summary>
		/// 打率
		/// </summary>
		[Display(Name = "打率")]
		public string Average { get; set; }
	}
}
