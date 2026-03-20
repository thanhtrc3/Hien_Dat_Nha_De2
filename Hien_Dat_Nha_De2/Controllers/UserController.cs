using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hien_Dat_Nha_De2.Models;

namespace Hien_Dat_Nha_De2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public IActionResult Delete(string id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}