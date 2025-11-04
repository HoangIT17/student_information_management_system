using Microsoft.AspNetCore.Mvc;

namespace WebStudentMVC.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public IActionResult Index()
        {
            return View();
        }
    }
}