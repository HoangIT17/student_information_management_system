// Models/ContentModule.cs
using System.Collections.Generic;

namespace WebStudentMVC.Models
{
    // Model 4: Module/Chương học
    public class ContentModule
    {
        public int ContentModuleId { get; set; }
        public string Title { get; set; }
        public int Order { get; set; } // Để sắp xếp thứ tự

        // Mối quan hệ: Một Module thuộc về MỘT Class
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        // Mối quan hệ: Một Module có nhiều Bài học/Tài liệu
        public virtual ICollection<ContentItem> ContentItems { get; set; }
    }
}