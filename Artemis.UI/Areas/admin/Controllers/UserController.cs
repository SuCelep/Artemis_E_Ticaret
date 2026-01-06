using Artemis.BL.Repositories;
using Artemis.DAL.Entities;
using Artemis.UI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artemis.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class UserController : Controller
    {
        IRepository<User> repoUser;
        public UserController(IRepository<User> _repoUser)
        {
            repoUser = _repoUser;
        }

        public IActionResult Index()
        {
            return View(repoUser.GetAll().OrderByDescending(x => x.Id));
        }

        public IActionResult NewUser()
        {

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(User model)
        {
            model.Password= GeneralTools.GetMD5(model.Password);
            if (ModelState.IsValid)
            {
                
                repoUser.Add(model);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("New");
        }

        public IActionResult EditUser(int id)
        {
            User User = repoUser.GetBy(x => x.Id == id);
            if (User != null) return View(User);
            else return RedirectToAction("Index");


        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(User model)
        {
            model.Password = GeneralTools.GetMD5(model.Password);
            if (ModelState.IsValid)
            {
               
                repoUser.Update(model);
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Edit");
        }

        public IActionResult Delete(int id)
        {
            User User = repoUser.GetBy(x => x.Id == id);
            if (User != null) repoUser.Delete(User);
            return RedirectToAction("Index");


        }
    }
}
