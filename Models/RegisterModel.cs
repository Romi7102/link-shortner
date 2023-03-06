using System.ComponentModel.DataAnnotations;

namespace LinkShortner.Models {
    public class RegisterModel {
        [Required]
        public UserModel User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
        [Compare(nameof(Password) , ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
 
        
    }
}
