using Microsoft.AspNetCore.Identity;
using WebStudentMVC.Models;
using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Services.Interfaces
{
    public interface IAccountService
    {
        Task<(IdentityResult Result, Users User)> RegisterUserAsync(RegisterViewModel model);
        Task<LoginResult> LoginUserAsync(LoginViewModel model);
        Task<string> GetUserRoleAsync(Users user);
        Task LogoutAsync();
        Task<Users> GetUserByEmailAsync(string email);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model);
    }
}
