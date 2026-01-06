using Artemis.BL.Repositories;
using Artemis.DAL.Entities;
using Artemis.UI.Models;
using Artemis.UI.Tools;
using Artemis.UI.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Artemis.UI.Controllers
{
    public class UserController : Controller
    {
        IRepository<User> repoUser;
        IRepository<Product> repoProduct;
        IRepository<Brand> repoBrand;
        IRepository<Category> repoCategory;
        public UserController(IRepository<User> _repoUser, IRepository<Product> _repoProduct, IRepository<Brand> _repoBrand,
        IRepository<Category> _repoCategory)
        {
            repoUser = _repoUser;
            repoProduct = _repoProduct;
            repoBrand = _repoBrand;
            repoCategory = _repoCategory;
        }
        public IActionResult Index()
        {
            if(User.HasClaim(x=> x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
            }
            else
            {
                ViewBag.Login = 0;
            }
            return View();
        }

        [Route("/user/login")]
        public IActionResult Login() 
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
            }
            else
            {
                ViewBag.Login = 0;
            }
            return View();
        }

        [Route("/user/login"),HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            string md5Password = GeneralTools.GetMD5(password);
            User user = repoUser.GetBy(x => x.UserName == username && x.Password == md5Password && x.Status);
            if (user != null)
            {
               List<Claim> claims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                   new Claim("adsoyad", user.NameSurname),
                   new Claim(ClaimTypes.Role, "User")

               };
                ClaimsIdentity identity = new ClaimsIdentity(claims,"UserCookieSchema");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties() {IsPersistent=true });

                ViewBag.Login = 1;
                return Redirect("/");
            }
            else TempData["bilgi"] = "Kullanıcı adı veya Parola Bilgisi Hatalı";
            return RedirectToAction("login");
            
        }

        public async Task<IActionResult> LogOut(User us)
        {
            User user= new User
            {
                UserName = us.UserName,
                Password = us.Password,
                NameSurname = us.NameSurname,
                Id = us.Id
            };


            await HttpContext.SignOutAsync();
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Append("MyCard", JsonConvert.SerializeObject(us), options);

            return Redirect("/");
        }


        

        [Route("/user/profile")]
        public IActionResult Profile()
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                string id = User.Claims.First(x=> x.Type == ClaimTypes.NameIdentifier).Value;
                UserVM userVM = new UserVM
                {
                    Users = repoUser.GetBy(x => x.Id.ToString() == id),
                    Products= repoProduct.GetAll(x=> x.UserID.ToString()==id).Include(x=>x.Category).Include(x=>x.Brand)
                };
                ViewBag.Login = 1;
                return View(userVM);
            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }
        }

        [Route("/user/newproduct")]
        public IActionResult New()
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                string id = User.Claims.First(x=> x.Type== ClaimTypes.NameIdentifier).Value;
                UserVM userVM = new UserVM
                {
                    Product = new Product(),
                    Brands= repoBrand.GetAll().OrderBy(x=>x.Name),
                    Categories=repoCategory.GetAll(x=> x.ParentID != null).OrderBy(x=>x.Name),
                    Users= repoUser.GetBy(x =>x.Id.ToString()==id)
                };
                return View(userVM);
            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }
        }

        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Insert(UserVM model)
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                string id = User.Claims.First(x=>x.Type== ClaimTypes.NameIdentifier).Value;
                model.Product.User=repoUser.GetBy(x=>x.Id.ToString()==id);
                model.Product.Status=false;
                if (ModelState.IsValid)
                {
                    repoProduct.Add(model.Product);
                    return Redirect("profile");
                }
                else return RedirectToAction("New");
            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }
        }

        [Route("/user/editproduct/{productid}")]
        public IActionResult Edit(int productid)
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                Product product = repoProduct.GetBy(x=>x.ID==productid);
                string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
               UserVM userVM = new UserVM
               {
                   Product = product,
                   Brands=repoBrand.GetAll().OrderBy(x=>x.Name),
                   Categories= repoCategory.GetAll(x=> x.ParentID != null).OrderBy(x => x.Name),
                   Users = repoUser.GetBy(x=>x.Id.ToString()== id ),
               };
                if (product != null) return View(userVM);
                else return RedirectToAction("profile");

                
            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }
        }


        [HttpPost,ValidateAntiForgeryToken, Route("/user/editproduct/{productid}")]
        public IActionResult Edit(UserVM model)
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                if (ModelState.IsValid)
                {
                    string id = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                    model.Product.User = repoUser.GetBy(x => x.Id.ToString() == id);
                    repoProduct.Update(model.Product);
                    return RedirectToAction("profile");
                }
                else return RedirectToAction("edit");
            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }
        }

        public IActionResult Delete(int productid)
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                Product product = repoProduct.GetBy(x => x.ID == productid);
                if (product != null) repoProduct.Delete(product);
                return RedirectToAction("profile");
            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }
        }



        [Route("/user/signin") ]
        public IActionResult Signİn()
        {
            User user = new User();
            return View(user);

        }
        [Route("/user/signin"),HttpPost, ValidateAntiForgeryToken]
        public  IActionResult Signİn(User us)
        {
            
            
                us.Password = GeneralTools.GetMD5(us.Password);
            us.Status = true;

            if (ModelState.IsValid)
            {
                repoUser.Add(us);
                repoUser.Update(us);
                return RedirectToAction("login");
            }
                

            
            return Redirect("/login");
        }

    }
}