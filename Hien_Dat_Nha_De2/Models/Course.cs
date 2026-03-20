using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_Dat_Nha_De2.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khóa học")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giảng viên")]
        public int InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        // Thêm dấu ? để tránh lỗi Validation
        public Instructor? Instructor { get; set; }

        // Thêm dấu ? để tránh lỗi Validation
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}