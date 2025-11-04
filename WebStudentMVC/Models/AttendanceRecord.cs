// Models/AttendanceRecord.cs
using System;
using System.Collections.Generic;

namespace WebStudentMVC.Models
{
    // Model 9: Một buổi điểm danh
    public class AttendanceRecord
    {
        public int AttendanceRecordId { get; set; }
        public DateTime SessionDate { get; set; } // Ngày điểm danh

        // Mối quan hệ: Buổi điểm danh này của Lớp nào
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        // Mối quan hệ: Buổi này có nhiều trạng thái của sinh viên
        public virtual ICollection<AttendanceStatus> Statuses { get; set; }
    }
}