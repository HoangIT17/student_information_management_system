// Models/ContentItem.cs
using System.Collections.Generic;

namespace WebStudentMVC.Models
{
    // Model 5: Bài học/Tài liệu cụ thể (PDF, Video, Text...)
    public class ContentItem
    {
        public int ContentItemId { get; set; }
        public string Title { get; set; }
        public string ItemType { get; set; } // "File", "Video", "Text"
        public string Url { get; set; } // Đường dẫn file, link video

        // Mối quan hệ: Một Bài học thuộc về MỘT Module
        public int ContentModuleId { get; set; }
        public virtual ContentModule ContentModule { get; set; }

        // Mối quan hệ: Nhiều Student có thể xem bài này
        public virtual ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}