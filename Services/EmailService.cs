using System.Net.Mail;

namespace LinkShortner.Services {
	public class EmailService {
		public bool IsValid(string email) {
			var valid = true;

			try {
				var emailAddress = new MailAddress(email);
			}
			catch {
				valid = false;
			}

			return valid;
		}
	}
}
