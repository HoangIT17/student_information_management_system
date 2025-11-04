// Models/StudentProgress.cs
namespace WebStudentMVC.Models
{
    // Model 6: Theo dõi Student đã "Hoàn thành" bài học
    public class StudentProgress
    {
        public int StudentProgressId { get; set; }
        public bool IsCompleted { get; set; } = false;

        // FK đến Student
        public string StudentId { get; set; }
        public virtual Users Student { get; set; }

        // FK đến Bài học
        public int ContentItemId { get; set; }
        public virtual ContentItem ContentItem { get; set; }
    }
}