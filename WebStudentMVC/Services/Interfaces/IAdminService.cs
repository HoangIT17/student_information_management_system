using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Services.Interfaces
{
    public interface IAdminService
    {
        // User Listing and Approval
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync(string searchTerm);
        Task<IEnumerable<UserViewModel>> GetUsersForApprovalAsync(); // Renamed this method
        Task<bool> ApproveTeacherAsync(string userId);
        Task<bool> RejectTeacherAsync(string userId);
        
        // CRUD Operations
        Task<bool> CreateUserAsync(CreateUserViewModel model);
        Task<EditUserViewModel> GetUserForEditAsync(string userId);
        Task<bool> UpdateUserAsync(EditUserViewModel model);
        Task<bool> DeleteUserAsync(string userId);
        Task<UserViewModel> GetUserDetailsAsync(string userId);
    }
}