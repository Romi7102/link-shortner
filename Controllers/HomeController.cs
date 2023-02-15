using LinkShortner.Context;
using LinkShortner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LinkShortner.Controllers {
    public class HomeController : Controller {
        private readonly LinkContext linkContext;
		private readonly ILogger<HomeController> _logger;

        public HomeController(LinkContext urlContext , ILogger<HomeController> logger) {
            this.linkContext = urlContext;
			_logger = logger;
        }

        public IActionResult Index() {
            return View();
        }
        [HttpGet("s/{code}")]
        public IActionResult GetUrl([FromRoute] string code) {
            var link = linkContext.GetLinkByCode(code);
            if (link != null) {
				link.Clicks++;
				linkContext.Links.Update(link);
				linkContext.SaveChanges();
				return RedirectPermanent(link.FullUrl);
            }
            else {
                return BadRequest();
            }
        }

        public IActionResult About() {
            
            return View();
        }
        [HttpGet("links")]
        public IActionResult Links() {
            var ret = linkContext.Links.ToList();
            return View(ret);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}