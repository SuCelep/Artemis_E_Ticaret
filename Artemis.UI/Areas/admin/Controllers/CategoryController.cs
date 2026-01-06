using Artemis.BL.Repositories;
using Artemis.DAL.Entities;
using Artemis.UI.Areas.admin.ViewModelA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artemis.UI.Areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class CategoryController : Controller
    {
            IRepository<Category> repoCategory;
            public CategoryController(IRepository<Category> _repoCategory)
            {
                repoCategory = _repoCategory;
            }

            public IActionResult Index()
            {
                return View(repoCategory.GetAll().Include(x=>x.ParentCategory).OrderByDescending(x =>x.ID));
            }

            public IActionResult New()
            {
            CategoryVM categoryVM = new CategoryVM()
            {
                Category= new Category(),
                Categories = repoCategory.GetAll(x=> x.ParentID == null).OrderBy(x=> x.Name)
            };
                return View(categoryVM);
            }

            [HttpPost, ValidateAntiForgeryToken]
            public async Task<IActionResult> Insert(CategoryVM model)
            {
                if (ModelState.IsValid)
                {
                    repoCategory.Add(model.Category);
                    return RedirectToAction("Index");
                }
                else return RedirectToAction("New");
            }

            public IActionResult Edit(int id)
            {
                Category category = repoCategory.GetBy(x => x.ID == id);
                CategoryVM categoryVM = new CategoryVM
                {
                    Category = category,
                    Categories = repoCategory.GetAll().OrderBy(x=>x.Name)
                };
                if (category != null) return View(categoryVM);
                else return RedirectToAction("Index");


            }

            [HttpPost, ValidateAntiForgeryToken]
            public async Task<IActionResult> Update(CategoryVM model)
            {
                if (ModelState.IsValid)
                {
                    repoCategory.Update(model.Category);
                    return RedirectToAction("Index");
                }
                else return RedirectToAction("Edit");
            }

            public IActionResult Delete(int id)
            {
                Category Category = repoCategory.GetBy(x => x.ID == id);
            if (Category.ParentID == null)
            {
                
                return RedirectToAction("Index"); // uyarı mesejı göster
            }
            else 
            {
                if (Category != null) repoCategory.Delete(Category);
                return RedirectToAction("Index");
            }
              


            }
        }
}
