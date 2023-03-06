using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LinkShortner.Models {
    public class UserModel : IdentityUser {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }
}
