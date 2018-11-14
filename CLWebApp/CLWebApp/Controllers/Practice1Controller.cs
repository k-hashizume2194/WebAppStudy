using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLWebApp.Data;
using CLWebApp.Models.ViewModels;
using CLWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CLWebApp.Controllers
{
    public class Practice1Controller : Controller
    {
        //private readonly ApplicationDbContext _context;


        private Practice1Service _service;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public Practice1Controller()
        {
            _service = new Practice1Service();
        }

        //public Practice1Controller(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

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
        [HttpPost]
        public IActionResult Record(Practice1ViewModel viewModel)
        {
            // TODO:処理の実装
            return View(viewModel);
        }
    }
}