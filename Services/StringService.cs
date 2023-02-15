using System;

namespace LinkShortner.Services {
	public class StringService {
		public Random random { get; set; }

		public StringService() {
			random = new Random();
		}
		public string RandomString(int length) {
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
