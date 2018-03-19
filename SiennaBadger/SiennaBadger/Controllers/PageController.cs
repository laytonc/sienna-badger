using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using Microsoft.AspNetCore.Mvc;
using SiennaBadger.Data.Models;

namespace SiennaBadger.Web.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/page")]
    public class PageController : Controller
    {
        [HttpPost]
        [Route("parse")]
        public PageSummary Parse(string parseUrl)
        {
            PageSummary pageSummary = new PageSummary();
            List<Image> images = new List<Image>();
            List<Word> wordList = new List<Word>();
            
            // TODO: validate parseUrl
            // TODO: refactor to service

            var config = Configuration.Default.WithDefaultLoader();

            // TODO return async
            // Asynchronously get the document in a new context using the configuration
            var document = BrowsingContext.New(config).OpenAsync(parseUrl).Result;

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


            // We are only interested in the text - select it with LINQ
            var text = elements.FirstOrDefault().TextContent;

            // get string of words

            // scrub it
            var scrubbedText = Regex.Replace(text, "[^a-zA-Z0-9% ._]", string.Empty).ToLower();

            // parse words
            var punctuation = scrubbedText.Where(Char.IsPunctuation).Distinct().ToArray();
            var words = scrubbedText.Split().Select(x => x.Trim(punctuation));
            //var words = scrubbedText.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var scrubbedWords = words.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            
            // count them
            pageSummary.WordCount = scrubbedWords.Count();
            wordList = scrubbedWords
                .GroupBy(word => word)
                .Select(kvp => new Word (){ Text = kvp.Key, Count = kvp.Count() })
                .OrderByDescending(m=>m.Count)
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
            return pageSummary;
        }
    }
}
