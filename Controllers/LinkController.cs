using LinkShortner.Context;
using LinkShortner.Models;
using LinkShortner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;


namespace LinkShortner.Controllers {
	[Route("api")]
	[ApiController]
	public class LinkController : ControllerBase {
		private readonly LinkContext urlContext;
		private readonly StringService stringService;
		private readonly UserManager<UserModel> userManager;
		private readonly SignInManager<UserModel> signInManager;

		public LinkController(LinkContext urlContext, StringService stringService, UserManager<UserModel> userManager , SignInManager<UserModel> signInManager) {
			this.urlContext = urlContext;
			this.stringService = stringService;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpPost("shorten")]
		public ActionResult<Link> Shorten([FromBody] string fullUrl) {
			Uri uriResult;
			bool result = Uri.TryCreate(fullUrl, UriKind.Absolute, out uriResult)
				&& (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

			if (!result)
				return BadRequest();


			var userId = signInManager.IsSignedIn(User) ? userManager.GetUserId(User) : null;

			var tempLink = urlContext.GetUrlByFullUrl(fullUrl);

			if (tempLink != null && tempLink.UserId == userId) {
				return urlContext.GetUrlByFullUrl(fullUrl);
			}
			else {
				string code = stringService.RandomString(6);
				while (!urlContext.IsValid(code)) {
					code = stringService.RandomString(6);
				}
				Link ret = new Link {
					Code = code,
					FullUrl = fullUrl,
					Clicks = 0,
					UserId = userId,
					Logs = new List<LinkLog>()
				};
				urlContext.Links.Add(ret);
				urlContext.SaveChanges();
				return ret;
			}
		}




	}
}
