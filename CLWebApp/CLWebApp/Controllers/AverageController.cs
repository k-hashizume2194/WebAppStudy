using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{
    public class AverageController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "打率計算アプリ";
            AverageViewModel model = new AverageViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Calc(AverageViewModel model)
        {
            return View(model);
        }
    }
}