using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{
    public class NenpiController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "燃費計算アプリ";
            NenpiViewModel model = new NenpiViewModel();
            return View(model);
        }
    }
}