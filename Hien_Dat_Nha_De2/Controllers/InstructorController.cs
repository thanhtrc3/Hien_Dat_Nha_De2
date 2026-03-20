using Microsoft.AspNetCore.Mvc;
using Hien_Dat_Nha_De2.Models;
using Hien_Dat_Nha_De2.Repositories;

namespace Hien_Dat_Nha_De2.Controllers
{
    public class InstructorController : Controller
    {
        private IGenericRepository<Instructor> repo;

        public InstructorController(IGenericRepository<Instructor> _repo)
        {
            repo = _repo;
        }

        public IActionResult Index()
        {
            return View(repo.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instructor ins)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(ins);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(ins);
        }

        public IActionResult Edit(int id)
        {
            var data = repo.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Instructor ins)
        {
            repo.Update(ins);
            repo.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var data = repo.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);
            repo.Save();
            return RedirectToAction("Index");
        }
    }
}