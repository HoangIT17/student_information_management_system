// Models/AttendanceStatus.cs
namespace WebStudentMVC.Models
{
    // Model 10: Trạng thái của 1 Student trong 1 buổi
    public class AttendanceStatus
    {
        public int AttendanceStatusId { get; set; }
        public string Status { get; set; } // "Present", "Absent"

        // FK đến Buổi điểm danh
        public int AttendanceRecordId { get; set; }
        public virtual AttendanceRecord AttendanceRecord { get; set; }

        // FK đến Student
        public string StudentId { get; set; }
        public virtual Users Student { get; set; }
    }
}