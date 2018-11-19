using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Data;
using CLWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CLWebApp.Controllers
{
    public class CLBaseController : Controller
    {
		/// <summary>
		/// ログインユーザー情報取得
		/// </summary>
		/// <param name="_context">DBコンテキスト</param>
		/// <returns>ログイン中のユーザー情報</returns>
		public ApplicationUser GetLoginUser(ApplicationDbContext _context)
		{
			// ログイン中のユーザー情報
			string userName = User.Identity.Name;
			var user = _context.Users.Where(p => p.UserName.Equals(userName)).First();
			return user;
		}
	}
}