using System.Security.Claims;
using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Services.Interfaces
{
    public interface IStudentService
    {
        Task<UserViewModel> GetStudentInfoAsync(ClaimsPrincipal userPrincipal);
        // Add other student-specific methods here, e.g., GetMyGradesAsync()
    }
}