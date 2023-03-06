using System.ComponentModel.DataAnnotations;

namespace LinkShortner.Models {
	public class LinkLog {
        [Key]
        public int Id { get; set; }
        public string Ip { get; set; }
		public DateTime Date { get; set; } = DateTime.UtcNow;
	}
}
