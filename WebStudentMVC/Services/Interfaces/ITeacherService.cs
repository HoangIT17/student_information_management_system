using System.Security.Claims;
using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<UserViewModel> GetTeacherInfoAsync(ClaimsPrincipal userPrincipal);
        // Add other teacher-specific methods here later, e.g., GetMyClassesAsync()
    }
}