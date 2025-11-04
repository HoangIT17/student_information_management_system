// Models/Course.cs
using System.Collections.Generic;

namespace WebStudentMVC.Models
{
    // Model 1: Course (Môn học - "Khuôn mẫu")
    public class Course
    {
        public int CourseId { get; set; } // Khóa chính
        public string CourseName { get; set; }
        public string CourseCode { get; set; } // Mã môn học
        public string Description { get; set; }

        // Mối quan hệ: Một Course có nhiều Class
        public virtual ICollection<Class> Classes { get; set; }
    }
}