using Microsoft.AspNetCore.Mvc;

namespace WebStudentMVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        // GET: Enrollments
        public IActionResult Index()
        {
            return View();
        }
    }
}