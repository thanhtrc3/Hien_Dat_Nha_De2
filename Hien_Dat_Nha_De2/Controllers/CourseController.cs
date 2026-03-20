using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hien_Dat_Nha_De2.Models;
using Hien_Dat_Nha_De2.Repositories;

namespace Hien_Dat_Nha_De2.Controllers
{
    public class CourseController : Controller
    {
        private IGenericRepository<Course> courseRepo;
        private IGenericRepository<Instructor> insRepo;

        public CourseController(
            IGenericRepository<Course> cRepo,
            IGenericRepository<Instructor> iRepo)
        {
            courseRepo = cRepo;
            insRepo = iRepo;
        }

        public IActionResult Index()
        {
            var courses = courseRepo.GetAll(c => c.Instructor);
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Instructors = new SelectList(insRepo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course c)
        {
            if (ModelState.IsValid)
            {
                courseRepo.Insert(c);
                courseRepo.Save();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        public IActionResult Edit(int id)
        {
            var data = courseRepo.GetById(id);
            ViewBag.Instructors = new SelectList(insRepo.GetAll(), "Id", "Name");
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Course c)
        {
            courseRepo.Update(c);
            courseRepo.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var data = courseRepo.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            courseRepo.Delete(id);
            courseRepo.Save();
            return RedirectToAction("Index");
        }
    }
}