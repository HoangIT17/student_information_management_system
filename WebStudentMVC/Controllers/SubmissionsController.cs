using Microsoft.AspNetCore.Mvc;

namespace WebStudentMVC.Controllers
{
    public class SubmissionsController : Controller
    {
        // GET: Submissions
        public IActionResult Index()
        {
            return View();
        }
    }
}