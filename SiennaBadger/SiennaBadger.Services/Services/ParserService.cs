using SiennaBadger.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using SiennaBadger.Infrastructure.Extensions;

namespace SiennaBadger.Infrastructure.Services
{
    public class ParserService : IParserService
    {
        private readonly ILogger<ParserService> _logger;

        public ParserService(ILogger<ParserService> logger)
        {
            _logger = logger;
        }

        public async Task<PageSummary> ParsePageAsync(string url)
        {
            _logger.LogInformation($"Parse Url:{url}");
            PageSummary pageSummary = new PageSummary();
            List<Image> images = new List<Image>();

            var config = Configuration.Default.WithDefaultLoader();

            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(url);

            // return this so we can respond accordingly via our API
            pageSummary.StatusCode = document.StatusCode;

            // This CSS selector gets the desired content, for now we will select body element
            var cellSelector = "body";
            // Perform the query to get all cells with the content
            var elements = document.QuerySelectorAll(cellSelector);

            // exclude text from script and style elements.
            ScrubElements(elements);


            var selectedElement = elements.FirstOrDefault();
            if (selectedElement != null)
            {
                var words = selectedElement.TextContent.Words().ToList();
                // get doc word count
                pageSummary.WordCount = words.Sum(m=>m.Count);

                // trim down to top 10 words
                pageSummary.Words = words.OrderByDescending(m=>m.Count).Take(10);

                foreach (var item in document.Images)
                {
                    if (!string.IsNullOrEmpty(item.Source))
                    {
                        images.Add(new Image() { Url = item.Source });
                    }

                }

                pageSummary.Images = images;
            }
            
            return pageSummary;
        }

        private void ScrubElements(IHtmlCollection<IElement> elements)
        {
            foreach (var element in elements)
            {
                var scripts = element.GetElementsByTagName("script");

                foreach (var script in scripts)
                {
                    script.Parent.RemoveChild(script);
                }

                scripts = element.GetElementsByTagName("style");

                foreach (var script in scripts)
                {
                    script.Parent.RemoveChild(script);
                }
            }
        }
    }
}
