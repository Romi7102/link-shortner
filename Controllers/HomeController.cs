using LinkShortner.Context;
using LinkShortner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LinkShortner.Controllers {
	public class HomeController : Controller {
		private readonly LinkContext linkContext;
		private readonly ILogger<HomeController> _logger;
		private readonly UserManager<UserModel> userManager;
		private readonly SignInManager<UserModel> signInManager;

		public HomeController(LinkContext linkContext, ILogger<HomeController> logger, UserManager<UserModel> userManager,
			SignInManager<UserModel> SignInManager) {
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

			var tempLog = new LinkLog {
				Ip = HttpContext.Connection.RemoteIpAddress.ToString(),
			};

			link.Clicks++;

			if(link.Logs == null) 
				link.Logs = new List<LinkLog>();
			
			link.Logs.Add(tempLog);


			linkContext.Links.Update(link);
			linkContext.SaveChanges();
			return RedirectPermanent(link.FullUrl);



		}

		public IActionResult About() {

			return View();
		}
		[HttpGet("links")]
		[Authorize]
		public IActionResult Links() {
			if (!signInManager.IsSignedIn(User))
				return Redirect("/Identity/Account/Login");

			var userId = User.Identity.IsAuthenticated ? userManager.GetUserId(User) : null;
			var ret = linkContext.Links.Where(link => link.UserId == userId).ToList();
			return View(ret);
		}

		[HttpGet("links/{code}")]
        [Authorize]
        public IActionResult LinkLogs(string code) {
			var link = linkContext.GetLinkByCode(code);
			return View(link);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}