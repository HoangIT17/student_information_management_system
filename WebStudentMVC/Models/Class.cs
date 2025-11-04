// Models/Class.cs
// (Đây là file bạn đã cung cấp, đã nằm trong đúng namespace)
using System;
using System.Collections.Generic;

namespace WebStudentMVC.Models
{
    public class Class
    {
        public int ClassId { get; set; } // Khóa chính
        public string ClassCode { get; set; } // Mã lớp
        public int MaxStudents { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // --- MỐI QUAN HỆ 1: Một Class thuộc về MỘT Course ---
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        // --- MỐI QUAN HỆ 2: Một Class được dạy bởi MỘT Teacher ---
        // (Sử dụng model "Users" của bạn)
        public string TeacherId { get; set; }
        public virtual Users Teacher { get; set; }

        // --- MỐI QUAN HỆ 3: Một Class có nhiều Student (qua bảng Enrollment) ---
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        // Các mối quan hệ khác...
        public virtual ICollection<ContentModule> ContentModules { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<AttendanceRecord> AttendanceRecords { get; set; }
    }
}