using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_Dat_Nha_De2.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        // THÊM DẤU CHẤM HỎI VÀO ĐÂY (?)
        public ICollection<Course>? Courses { get; set; }
    }
}