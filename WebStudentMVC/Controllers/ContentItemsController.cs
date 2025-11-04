using Microsoft.AspNetCore.Mvc;

namespace WebStudentMVC.Controllers
{
    public class ContentItemsController : Controller
    {
        // GET: ContentItems
        public IActionResult Index()
        {
            return View();
        }
    }
}