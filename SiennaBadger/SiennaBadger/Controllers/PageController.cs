using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiennaBadger.Web.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/page")]
    public class PageController : Controller
    {
        [HttpGet]
        [Route("parse")]
        public ActionResult Parse()
        {
            return Ok();
        }
    }
}
