using Microsoft.AspNetCore.Identity;
using WebStudentMVC.Models;
using WebStudentMVC.Services.Interfaces;
using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AccountService(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<(IdentityResult Result, Users User)> RegisterUserAsync(RegisterViewModel model)
        {
            Users user = new()
            {
                FullName = model.Name,
                Email = model.Email,
                UserName = model.Email,
                IsApproved = model.Role == "Student"
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            return (result, user);
        }

        public async Task<LoginResult> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var isTeacher = await _userManager.IsInRoleAsync(user, "Teacher");
                if (isTeacher && !user.IsApproved)
                {
                    return new LoginResult { RequiresApproval = true, ErrorMessage = "Your account is pending admin approval." };
                }
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            return new LoginResult
            {
                SignInResult = result,
                User = user
            };
        }

        public async Task<string> GetUserRoleAsync(Users user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                return removePasswordResult;
            }

            return await _userManager.AddPasswordAsync(user, model.NewPassword);
        }
    }
}