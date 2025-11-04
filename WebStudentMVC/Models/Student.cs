using Microsoft.AspNetCore.Identity;


namespace WebStudentMVC.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StudentId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ClassName { get; set; }
    }

}
