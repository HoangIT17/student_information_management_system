using Microsoft.AspNetCore.Mvc;
using WebStudentMVC.ViewModels;
using WebStudentMVC.Services.Interfaces;

namespace WebStudentMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _accountService.LoginUserAsync(model);

                if (loginResult.RequiresApproval)
                {
                    ModelState.AddModelError(string.Empty, loginResult.ErrorMessage);
                    return View(model);
                }

                if (loginResult.SignInResult.Succeeded)
                {
                    if (loginResult.User != null)
                    {
                        var role = await _accountService.GetUserRoleAsync(loginResult.User);
                        switch (role)
                        {
                            case "Admin": return RedirectToAction("Dashboard", "Admin");
                            case "Teacher": return RedirectToAction("Dashboard", "Teacher");
                            case "Student": return RedirectToAction("Dashboard", "Student");
                        }
                    }
                    // This fallback is no longer relevant but is kept for safety
                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Roles = new List<string> { "Student", "Teacher" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (result, user) = await _accountService.RegisterUserAsync(model);

                if (result.Succeeded)
                {
                    if (!user.IsApproved)
                    {
                        return View("RegistrationConfirmation");
                    }
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewBag.Roles = new List<string> { "Student", "Teacher" };
            return View(model);
        }

        public IActionResult RegistrationConfirmation()
        {
            return View();
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
            }
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            return View(new ChangePasswordViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. Try again");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
