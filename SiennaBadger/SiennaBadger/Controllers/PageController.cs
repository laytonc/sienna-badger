using System.Collections.Generic;
using System.Linq;
using AngleSharp;
using AngleSharp.Dom.Html;
using Microsoft.AspNetCore.Mvc;
using SiennaBadger.Data.Models;

namespace SiennaBadger.Web.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/page")]
    public class PageController : Controller
    {
        [HttpGet]
        [Route("parse")]
        public PageSummary Parse()
        {
            PageSummary pageSummary = new PageSummary();
            List<Image> images = new List<Image>();
            List<Word> words = new List<Word>();

            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            var address = "https://www.easi.com";
            // Asynchronously get the document in a new context using the configuration
            var document = BrowsingContext.New(config).OpenAsync(address).Result;

            var headings = document.All
                .OfType<IHtmlHeadingElement>()
                .Select(h => h.TextContent.Trim())
                .ToList();
            foreach (var heading in headings)
            {
                words.Add(new Word(){Text = heading});
            }

            foreach (var item in document.Images)
            {
                images.Add(new Image(){Url=item.Source});
            }

            pageSummary.Words = words;
            pageSummary.Images = images;
            return pageSummary;
        }
    }
}
