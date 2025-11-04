using Microsoft.AspNetCore.Mvc;

namespace WebStudentMVC.Controllers
{
    public class AttendanceRecordsController : Controller
    {
        // GET: AttendanceRecords
        public IActionResult Index()
        {
            return View();
        }
    }
}