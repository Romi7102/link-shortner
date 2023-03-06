using System.ComponentModel.DataAnnotations;

namespace LinkShortner.Models {
	public class ManageModel {
        [Required]
        public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		
    }
}
