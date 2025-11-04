using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStudentMVC.Services.Interfaces;

namespace WebStudentMVC.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var studentInfo = await _studentService.GetStudentInfoAsync(User);
            return View("~/Views/Student/Dashboard/Dashboard.cshtml", studentInfo);
        }
    }
}