using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiennaBadger.Data.Models;
using SiennaBadger.Infrastructure.Services;

namespace SiennaBadger.Web.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/page")]
    public class PageController : Controller
    {
        private readonly IParserService _parserService;
        private readonly ILogger<PageController> _logger;

        public PageController(IParserService parserService, ILogger<PageController> logger)
        {
            _parserService = parserService;
            _logger = logger;
        }

        /// <summary>
        /// Returns an Page Summary object.
        /// </summary>
        /// <remarks>This call will return summary data for a parsed website page.</remarks>
        /// <returns><see cref="PageSummary"/></returns>
        [HttpPost]
        [Route("parse")]
        [Produces(typeof(PageSummary))]
        public async Task<IActionResult> Parse(string parseUrl)
        {
            // validate parseUrl
            if (!Uri.IsWellFormedUriString(parseUrl, UriKind.Absolute))
            {
                return BadRequest("This service requires a well formed url to parse.");
            }

            try
            {
                var pageSummary = await _parserService.ParsePageAsync(parseUrl);

                return Ok(pageSummary);
            }
            catch (Exception ex)
            {
                //log error
                _logger.LogError($"Failed to parse page({parseUrl}): {ex}");
                return BadRequest("We've had a catastrophic error. Our minions are looking at it now. We appreciate your patience.");
            } 
        }
    }
}
