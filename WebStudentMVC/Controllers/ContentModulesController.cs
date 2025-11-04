using Microsoft.AspNetCore.Mvc;

namespace WebStudentMVC.Controllers
{
    public class ContentModulesController : Controller
    {
        // GET: ContentModules
        public IActionResult Index()
        {
            return View();
        }
    }
}