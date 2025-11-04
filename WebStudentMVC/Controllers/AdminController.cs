using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStudentMVC.Services.Interfaces;
using WebStudentMVC.ViewModels;

namespace WebStudentMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: /Admin/Dashboard
        public IActionResult Dashboard()
        {
            return View("~/Views/Admin/Dashboard/Dashboard.cshtml");
        }

        // GET: /Admin/AllUsersList
        public async Task<IActionResult> AllUsersList(string searchQuery)
        {
            ViewData["CurrentFilter"] = searchQuery;
            var users = await _adminService.GetAllUsersAsync(searchQuery);
            return View(users);
        }

        // GET: /Admin/ApprovedTeacher
        public async Task<IActionResult> ApprovedTeacher()
        {
            var teachers = await _adminService.GetUsersForApprovalAsync();
            return View(teachers);
        }

        // POST: /Admin/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(string userId)
        {
            await _adminService.ApproveTeacherAsync(userId);
            return RedirectToAction(nameof(ApprovedTeacher));
        }

        // POST: /Admin/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(string userId)
        {
            await _adminService.RejectTeacherAsync(userId);
            return RedirectToAction(nameof(ApprovedTeacher));
        }

        // GET: /Admin/CreateUser
        [HttpGet]
        public IActionResult CreateUser()
        {
            ViewBag.Roles = new List<string> { "Admin", "Teacher", "Student" };
            return View();
        }

        // POST: /Admin/CreateUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _adminService.CreateUserAsync(model);
                if (result)
                {
                    return RedirectToAction(nameof(AllUsersList));
                }
                ModelState.AddModelError("", "Failed to create user. The email may already be in use.");
            }
            ViewBag.Roles = new List<string> { "Admin", "Teacher", "Student" };
            return View(model);
        }
        
        // GET: /Admin/UserDetails/{userId}
        [HttpGet]
        public async Task<IActionResult> UserDetails(string userId)
        {
            var model = await _adminService.GetUserDetailsAsync(userId);
            if (model == null) return NotFound();
            
            return View(model);
        }

        // GET: /Admin/EditUser/{userId}
        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var model = await _adminService.GetUserForEditAsync(userId);
            if (model == null) return NotFound();
            
            ViewBag.Roles = new List<string> { "Admin", "Teacher", "Student" };
            return View(model);
        }

        // POST: /Admin/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _adminService.UpdateUserAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(AllUsersList));
                }
                ModelState.AddModelError("", "An error occurred while updating the user.");
            }
            ViewBag.Roles = new List<string> { "Admin", "Teacher", "Student" };
            return View(model);
        }
        
        // GET: /Admin/DeleteUser/{userId}
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _adminService.GetUserDetailsAsync(userId);
            if (user == null) return NotFound();
            
            return View(user);
        }

        // POST: /Admin/DeleteUser
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string userId)
        {
            await _adminService.DeleteUserAsync(userId);
            return RedirectToAction(nameof(AllUsersList));
        }
    }
}