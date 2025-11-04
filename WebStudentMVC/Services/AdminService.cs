using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStudentMVC.Models;
using WebStudentMVC.Services.Interfaces;
using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<Users> _userManager;

        public AdminService(UserManager<Users> userManager)
        {
            _userManager = userManager;
            
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync(string searchTerm)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => u.FullName.Contains(searchTerm) || u.Email.Contains(searchTerm));
            }
            var users = await query.ToListAsync();
            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault(),
                    IsApproved = user.IsApproved
                });
            }
            return userViewModels;
        }
        
        public async Task<IEnumerable<UserViewModel>> GetUsersForApprovalAsync()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            return teachers.Where(t => !t.IsApproved)
                           .Select(t => new UserViewModel
                           {
                               UserId = t.Id,
                               FullName = t.FullName,
                               Email = t.Email,
                               Role = "Teacher"
                           }).ToList();
        }

        public async Task<bool> ApproveTeacherAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            user.IsApproved = true;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> RejectTeacherAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> CreateUserAsync(CreateUserViewModel model)
        {
            var user = new Users
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                IsApproved = model.Role != "Teacher" // Teachers need approval
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            return result.Succeeded;
        }

        public async Task<EditUserViewModel> GetUserForEditAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new EditUserViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };
        }

        public async Task<bool> UpdateUserAsync(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return false;

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.Email;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.FirstOrDefault() != model.Role)
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded) return false;

                var addResult = await _userManager.AddToRoleAsync(user, model.Role);
                return addResult.Succeeded;
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<UserViewModel> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new UserViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = roles.FirstOrDefault(),
                IsApproved = user.IsApproved
            };
        }
    }
}