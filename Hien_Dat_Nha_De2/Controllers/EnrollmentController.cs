using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hien_Dat_Nha_De2.Data;
using Hien_Dat_Nha_De2.Models;

namespace Hien_Dat_Nha_De2.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var data = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.User)
                .ToList();

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);

            var enroll = new Enrollment
            {
                CourseId = courseId,
                UserId = user.Id,
                EnrollmentDate = DateTime.Now
            };

            _context.Enrollments.Add(enroll);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}