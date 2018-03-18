using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc;
using SiennaBadger.Data.Models;
using SiennaBadger.Models;

namespace SiennaBadger.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Summary()
        {
            PageSummary pageSummary = new PageSummary();
            List<Image> images = new List<Image>();
            List<Word> words = new List<Word>();

            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            var address = "https://www.nhl.com";
            // Asynchronously get the document in a new context using the configuration
            var document = BrowsingContext.New(config).OpenAsync(address).Result;

            var headings = document.All
                .OfType<IHtmlHeadingElement>()
                .Select(h => h.TextContent.Trim())
                .ToList();
            foreach (var heading in headings)
            {
                words.Add(new Word() { Text = heading });
            }

            foreach (var item in document.Images)
            {
                images.Add(new Image() { Url = item.Source });
            }

            pageSummary.Words = words;
            pageSummary.Images = images;

            return View(pageSummary);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
