// Models/Assignment.cs
using System;
using System.Collections.Generic;

namespace WebStudentMVC.Models
{
    // Model 7: Bài tập hoặc Quiz
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; } // Hạn nộp
        public double MaxPoints { get; set; } // Điểm tối đa
        public string AssignmentType { get; set; } // "Assignment" hoặc "Quiz"

        // Mối quan hệ: Một Bài tập thuộc về MỘT Class
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        // Mối quan hệ: Một Bài tập có nhiều Bài nộp
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}