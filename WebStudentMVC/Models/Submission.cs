// Models/Submission.cs
using System;

namespace WebStudentMVC.Models
{
    // Model 8: Bài nộp của Student
    public class Submission
    {
        public int SubmissionId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string FileUrl { get; set; } // File Student nộp

        public double? Grade { get; set; } // Điểm (có thể null)
        public string TeacherFeedback { get; set; } // Nhận xét

        // FK đến Bài tập
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        // FK đến Student
        public string StudentId { get; set; }
        public virtual Users Student { get; set; }
    }
}