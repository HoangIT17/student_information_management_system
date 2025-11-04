// Models/Enrollment.cs
namespace WebStudentMVC.Models
{
    // Model 3: Bảng nối Many-to-Many giữa Class và Student
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        // FK đến Lớp học
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        // FK đến Student (Sử dụng model "Users" của bạn)
        public string StudentId { get; set; }
        public virtual Users Student { get; set; }
    }
}