using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hien_Dat_Nha_De2.Models;
using Hien_Dat_Nha_De2.Repositories;

namespace Hien_Dat_Nha_De2.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IGenericRepository<Enrollment> _enrollRepo;
        private readonly IGenericRepository<Course> _courseRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentController(
            IGenericRepository<Enrollment> enrollRepo,
            IGenericRepository<Course> courseRepo,
            UserManager<ApplicationUser> userManager)
        {
            _enrollRepo = enrollRepo;
            _courseRepo = courseRepo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Yêu cầu 7: Dùng Include lấy Course và User
            var data = _enrollRepo.GetAll(e => e.Course, e => e.User);
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Yêu cầu đăng nhập nếu chưa
            }

            var enroll = new Enrollment
            {
                CourseId = courseId,
                UserId = user.Id,
                EnrollmentDate = DateTime.Now
            };

            _enrollRepo.Insert(enroll);
            _enrollRepo.Save();

            return RedirectToAction("Index");
        }
    }
}