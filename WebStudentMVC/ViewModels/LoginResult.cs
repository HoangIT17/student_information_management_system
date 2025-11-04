using Microsoft.AspNetCore.Identity;
using WebStudentMVC.Models;

namespace WebStudentMVC.ViewModels;

public class LoginResult
{
    public SignInResult SignInResult { get; set; }
    public Users User { get; set; }
    public string ErrorMessage { get; set; }
    public bool RequiresApproval { get; set; }
}