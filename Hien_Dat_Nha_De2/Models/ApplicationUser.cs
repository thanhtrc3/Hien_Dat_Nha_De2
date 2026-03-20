using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hien_Dat_Nha_De2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [MaxLength(100)]
        public string FullName { get; set; }

        // Quan hệ 1-n với Enrollments
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}