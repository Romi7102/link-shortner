using LinkShortner.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShortner.Context {
    public class LinkContext : DbContext {
        public LinkContext(DbContextOptions options) : base(options) {
        }
        public virtual DbSet<Link> Links{ get; set; }

		public string GetUrlByCode(string code) => Links.FirstOrDefault(link => link.Code == code).FullUrl;
		public Link GetLinkByCode(string code) => Links.FirstOrDefault(link => link.Code == code);
		public Link GetUrlByFullUrl(string fullUrl) => Links.FirstOrDefault(url => url.FullUrl == fullUrl);
		public bool IsValid(string code) {
            foreach (var link in Links.ToList()) {
                if (link.Code == code)
                    return false;
            }
            return true;
        }
        public bool LinkExists(string url) {
            foreach (var link in Links.ToList()) {
                if (link.FullUrl == url)
                    return false;
            }
            return true;
        }
    }
}
