﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SiennaBadger.Web.Models;

namespace SiennaBadger.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
           
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
