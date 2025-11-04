// Models/Users.cs
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebStudentMVC.Models
{
    // Đây là model bạn đã tạo, kế thừa từ IdentityUser
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsApproved { get; set; } = false;

        // --- Navigation Properties (Thuộc tính điều hướng) ---

        // Mối quan hệ: Một Teacher có thể dạy nhiều Lớp học
        // Dùng cho mối quan hệ Class -> Teacher
        public virtual ICollection<Class> ClassesTaught { get; set; }

        // Mối quan hệ: Một Student có thể tham gia nhiều Lớp học
        // Dùng cho bảng nối Enrollment
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        // Mối quan hệ: Một Student có nhiều Bài nộp
        public virtual ICollection<Submission> Submissions { get; set; }

        // Mối quan hệ: Một Student có nhiều Trạng thái điểm danh
        public virtual ICollection<AttendanceStatus> AttendanceStatuses { get; set; }

        // Mối quan hệ: Một Student có nhiều Tiến độ học
        public virtual ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}