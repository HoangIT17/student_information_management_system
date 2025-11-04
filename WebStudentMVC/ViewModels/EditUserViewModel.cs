using System.ComponentModel.DataAnnotations;

namespace WebStudentMVC.ViewModels
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}