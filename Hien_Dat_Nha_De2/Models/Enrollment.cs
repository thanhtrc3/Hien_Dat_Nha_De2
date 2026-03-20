using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hien_Dat_Nha_De2.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khóa học")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; } // Thêm dấu ?

        [Required(ErrorMessage = "Thiếu thông tin người dùng")]
        [MaxLength(450)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; } // Thêm dấu ?

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
    }
}