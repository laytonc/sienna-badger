using SiennaBadger.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.Extensions.Logging;

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

            pageSummary.StatusCode = document.StatusCode;

            // This CSS selector gets the desired content
            var cellSelector = "body";
            // Perform the query to get all cells with the content
            var elements = document.QuerySelectorAll(cellSelector);
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


           
            var selectedElement = elements.FirstOrDefault();
            if (selectedElement != null)
            {
                // get string of words
                var text = selectedElement.TextContent;

                // scrub it
                var scrubbedText = Regex.Replace(text, "[^a-zA-Z0-9% ._]", string.Empty).ToLower();

                // parse words
                var punctuation = scrubbedText.Where(Char.IsPunctuation).Distinct().ToArray();
                var words = scrubbedText.Split().Select(x => x.Trim(punctuation));
                //var words = scrubbedText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var scrubbedWords = words.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

                // count them
                pageSummary.WordCount = scrubbedWords.Count;
                var wordList = scrubbedWords
                    .GroupBy(word => word)
                    .Select(kvp => new Word() { Text = kvp.Key, Count = kvp.Count() })
                    .OrderByDescending(m => m.Count)
                    .Take(10)
                    .ToList();


                foreach (var item in document.Images)
                {
                    if (!string.IsNullOrEmpty(item.Source))
                    {
                        images.Add(new Image() { Url = item.Source });
                    }

                }

                pageSummary.Words = wordList;
                pageSummary.Images = images;
            }
            
            return pageSummary;
        }
    }
}
