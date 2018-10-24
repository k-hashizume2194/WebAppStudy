using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "メールアドレスは空にできません")]
        [EmailAddress(ErrorMessage = "メールアドレスの形式が不正です")]
		[Display(Name ="メールアドレス")]
        public string Email { get; set; }

		[Required(ErrorMessage = "パスワードは空にできません")]
		[DataType(DataType.Password)]
		[Display(Name = "パスワード")]
		public string Password { get; set; }

        [Display(Name = "ログイン状態を保持する")]
        public bool RememberMe { get; set; }
    }
}
