using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WebStudentMVC.Models;
using WebStudentMVC.Services.Interfaces;
using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly UserManager<Users> _userManager;

        public TeacherService(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserViewModel> GetTeacherInfoAsync(ClaimsPrincipal userPrincipal)
        {
            var user = await _userManager.GetUserAsync(userPrincipal);
            if (user == null)
            {
                return null;
            }

            return new UserViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = "Teacher"
            };
        }
    }
}