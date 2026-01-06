using Artemis.BL.Repositories;
using Artemis.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Artemis.UI.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        public ProductController(IRepository<Product> _repoProduct)
        {
                repoProduct = _repoProduct;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/product/details/{name}/{id}")]
        public IActionResult Details(string name, int id)
        {
            Product product = repoProduct.GetAll(x=>x.ID==id).Include(x=>x.Category).Include(x=>x.ProductPicture).Include(x=>x.User).Include(x=>x.Brand).FirstOrDefault();
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
            }
            else
            {
                ViewBag.Login = 0;
            }
            return View(product);
        }
    }
}
