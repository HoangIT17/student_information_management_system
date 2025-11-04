using Microsoft.AspNetCore.Mvc;

namespace WebStudentMVC.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public IActionResult Index()
        {
            return View();
        }
    }
}