using LinkShortner.Context;
using LinkShortner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LinkShortner.Controllers {
    public class HomeController : Controller {
        private readonly LinkContext linkContext;
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public HomeController(LinkContext linkContext , ILogger<HomeController> logger , UserManager<IdentityUser> userManager ,
			SignInManager<IdentityUser> SignInManager) {
            this.linkContext = linkContext;
			this._logger = logger;
			this.userManager = userManager;
			this.signInManager = SignInManager;
		}

        public IActionResult Index() {
            // Use this redirect to not allow to shorten links without an account
			//if (!signInManager.IsSignedIn(User))
			//	return Redirect("/Identity/Account/Login");
			return View();
        }
        [HttpGet("s/{code}")]
        public IActionResult GetUrl([FromRoute] string code) {
            var link = linkContext.GetLinkByCode(code);
            if (link == null) {
                return BadRequest();
            }

			link.Clicks++;
			linkContext.Links.Update(link);
			linkContext.SaveChanges();
			return RedirectPermanent(link.FullUrl);
            
            
            
        }

        public IActionResult About() {
            
            return View();
        }
        [HttpGet("links")]
        public IActionResult Links() {
            if (!signInManager.IsSignedIn(User))
                return Redirect("/Identity/Account/Login");

			var userId = User.Identity.IsAuthenticated ? userManager.GetUserId(User) : null;
			var ret = linkContext.Links.Where(link => link.UserId == userId).ToList();
            return View(ret);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}