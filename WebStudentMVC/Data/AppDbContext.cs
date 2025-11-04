using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStudentMVC.Models;

namespace WebStudentMVC.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        // 1. Khai báo tất cả các Bảng (DbSet)
        public DbSet<Course> Courses { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<ContentModule> ContentModules { get; set; }
        public DbSet<ContentItem> ContentItems { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<AttendanceStatus> AttendanceStatuses { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        // 2. Cấu hình Mối quan hệ (PHẦN BẠN CÒN THIẾU)
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Dòng này RẤT QUAN TRỌNG, phải gọi đầu tiên
            base.OnModelCreating(builder);

            // Cấu hình mối quan hệ 1-Nhiều: Teacher (Users) -> Class
            builder.Entity<Class>()
                .HasOne(c => c.Teacher) // Một Class có MỘT Teacher
                .WithMany(u => u.ClassesTaught) // Một Teacher (User) dạy NHIỀU Class
                .HasForeignKey(c => c.TeacherId) // Khóa ngoại là TeacherId
                .OnDelete(DeleteBehavior.Restrict); // QUAN TRỌNG: Ngăn xóa Teacher nếu họ đang dạy 1 lớp

            // Cấu hình mối quan hệ 1-Nhiều: Student (Users) -> Enrollment
            builder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(u => u.Enrollments) // Một Student (User) có NHIỀU Enrollment
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // Tự động xóa Enrollment nếu Student bị xóa

            // Cấu hình mối quan hệ 1-Nhiều: Class -> Enrollment
            builder.Entity<Enrollment>()
                .HasOne(e => e.Class)
                .WithMany(c => c.Enrollments) // Một Class có NHIỀU Enrollment
                .HasForeignKey(e => e.ClassId)
                .OnDelete(DeleteBehavior.Cascade); // Tự động xóa Enrollment nếu Class bị xóa
        }
    }
}
