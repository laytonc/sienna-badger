using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc;
using SiennaBadger.Data.Models;
using SiennaBadger.Models;

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
