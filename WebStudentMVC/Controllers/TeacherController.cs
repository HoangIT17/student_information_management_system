using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStudentMVC.Services.Interfaces;

namespace WebStudentMVC.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var teacherInfo = await _teacherService.GetTeacherInfoAsync(User);
            return View("~/Views/Teacher/Dashboard/Dashboard.cshtml", teacherInfo); 
        }
    }
}