using LinkShortner.Context;
using LinkShortner.Models;
using LinkShortner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LinkShortner.Controllers {
    [Route("api")]
    [ApiController]
    public class LinkController : ControllerBase {
        private readonly LinkContext urlContext;
		private readonly StringService stringService;

		public LinkController(LinkContext urlContext , StringService stringService) {
            this.urlContext = urlContext;
			this.stringService = stringService;
			
		}

        [HttpPost("shorten")]
        public ActionResult<Link> Shorten([FromBody] string fullUrl) {
            Uri uriResult;
            bool result = Uri.TryCreate(fullUrl, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!result) {
                return BadRequest();
            }


            if (urlContext.GetUrlByFullUrl(fullUrl) != null) {
                return urlContext.GetUrlByFullUrl(fullUrl);
            }
            else {
                string code = stringService.RandomString(6);
                while (!urlContext.IsValid(code)) {
                    code = stringService.RandomString(6);
				}
                Link ret = new Link { Code = code, FullUrl = fullUrl , Clicks = 0 };
                urlContext.Links.Add(ret);
                urlContext.SaveChanges();
                return ret;
            }
        }

        

      
    }
}
