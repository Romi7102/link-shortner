using System.ComponentModel.DataAnnotations;

namespace LinkShortner.Models {
	public class PasswordModel {
		[DataType(DataType.Password)]
		[Required]
        public string OldPassword { get; set; }

		[DataType(DataType.Password)]
		[StringLength(16, ErrorMessage = "Must be between 5 and 16 characters", MinimumLength = 4)]
		[Required]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[StringLength(16, ErrorMessage = "Must be between 5 and 16 characters", MinimumLength = 4)]
		[Compare("Password")]
        [Required]
		public string ConfirmPassword { get; set; }
	}
}
