using Artemis.BL.Repositories;
using Artemis.DAL.Entities; 
using Artemis.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Artemis.UI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Slide> repoSlide;
        IRepository<Product> repoProduct;

        public HomeController(IRepository<Slide> _repoSlide, IRepository<Product> _repoProduct)
        {
            repoSlide = _repoSlide;
           repoProduct = _repoProduct;
        }
        public IActionResult Index()
        {
            
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                ViewBag.Login = 1;
                IndexVM indexVM = new IndexVM()
                {
                    Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
                    Products=repoProduct.GetAll(x=>x.Status&& x.User.Id.ToString() != id).Include(x=> x.ProductPicture).Include(x=>x.Category).OrderBy(x=>Guid.NewGuid()).Take(9)
                };
                return View(indexVM);
            }
            else
            {
                ViewBag.Login = 0;
                IndexVM indexVM = new IndexVM()
                {
                    Slides = repoSlide.GetAll().OrderBy(x => x.DisplayIndex),
                    Products = repoProduct.GetAll(x => x.Status).Include(x => x.ProductPicture).Include(x => x.Category).OrderBy(x => Guid.NewGuid()).Take(9)
                };
                return View(indexVM);
            }
            
            
            
        }

        
    }
}
