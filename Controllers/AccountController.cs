using LinkShortner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortner.Controllers {
	public class AccountController : Controller {
		private readonly UserManager<UserModel> userManager;
		private readonly SignInManager<UserModel> signInManager;
		//private readonly RoleManager<IdentityRole> roleManager;

		public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager/*, RoleManager<IdentityRole> roleManager*/) {
			this.userManager = userManager;
			this.signInManager = signInManager;
			//this.roleManager = roleManager;
		}
		[HttpGet]
		public IActionResult Login() => View();
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model) {

			if (!ModelState.IsValid)
				return View();

			var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

			if (!result.Succeeded)
				return View();

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel model) {
			if (!ModelState.IsValid)
				return View();


			var result = await userManager.CreateAsync(model.User, model.Password);
			if (!result.Succeeded) {
				TempData["RegisterErrors"] = result.Errors.ToList();
				return View();
			}
			return RedirectToAction("Index", "Home");
		}


		[HttpGet]
		[Authorize]
		public IActionResult ManageAccount() => View();

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> UpdateDetails(ManageModel model) {
			if (!ModelState.IsValid)
				return RedirectToAction("ManageAccount");

			var user = await userManager.GetUserAsync(User);

			user.Email = model.Email;
			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
		
			await userManager.UpdateAsync(user);
			return RedirectToAction("ManageAccount");
		}

        [HttpGet]
		[Authorize]
        public IActionResult UpdatePassword() => View();

        [HttpPost]
		[Authorize]
		public async Task<IActionResult> UpdatePassword(PasswordModel model) {
			if (!ModelState.IsValid)
				return View(model);

			var user = await userManager.GetUserAsync(User);

			await userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

			return RedirectToAction("ManageAccount");
		}



		[HttpPost]
		public async Task<IActionResult> Logout() {
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

	}
}
