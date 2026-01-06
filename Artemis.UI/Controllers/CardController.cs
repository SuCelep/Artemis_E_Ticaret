using Artemis.BL.Repositories;
using Artemis.DAL.Entities;
using Artemis.UI.Models;
using Artemis.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Artemis.UI.Controllers
{
    public class CardController : Controller
    {
        IRepository<Product> repoProduct;
        IRepository<Order> repoOrder;
        IRepository<OrderDetails> repoOrderDetails;
        public CardController(IRepository<Product> _repoProduct, IRepository<Order> _repoOrder,
        IRepository<OrderDetails> _repoOrderDetails)
        {
                repoProduct = _repoProduct;
            repoOrder = _repoOrder;
            repoOrderDetails = _repoOrderDetails;
        }


        [Route("/sepet/sepeteekle"),HttpPost]
        public string AddCard(int productid,int quantity)
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                Product product = repoProduct.GetAll(x=>x.ID==productid).Include(x=>x.ProductPicture).FirstOrDefault();
                bool varMi= false;
                if (product != null)
                {
                    string text = string.Empty;
                    if (product.ProductPicture.Count != 0)
                    {
                        text = product.ProductPicture.FirstOrDefault().Picture;
                    }
                    else
                    {
                        text = "/wwwroot/img/gorsel-hazirlaniyor.jpg";
                    }

                    Card card = new Card()
                    {
                        ProductId = productid,
                        ProductName = product.Name,
                        ProductPicture= text,
                        ProductPrice = product.Price,
                        Quantity = quantity
                    };
                    List<Card>cards = new List<Card>();
                    if (Request.Cookies["MyCard"] != null)
                    {
                        cards = JsonConvert.DeserializeObject<List<Card>>(Request.Cookies["MyCard"]);
                        foreach (Card c in cards)
                        {
                            if (c.ProductId==productid)
                            {
                                varMi = true;
                                if (c.ProductId == productid) c.Quantity += quantity;
                                break;
                            }
                        }
                        
                    }
                    if (!varMi) cards.Add(card);
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddHours(3);
                    Response.Cookies.Append("MyCard",JsonConvert.SerializeObject(cards), options);
                    return product.Name;
                }
                return "~ Ürün Bulunamadı";
            }
            else
            {
                ViewBag.Login = 0;
                return "~ Oturum Kapalı";
            }
            
        }


        [Route("/sepet/sepetsayisi")]
        public int CardCount()
        {
            int geri = 0;
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                if (Request.Cookies["MyCard"] != null)
                {
                    List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(Request.Cookies["MyCard"]);
                    geri=cards.Sum(x=> x.Quantity);

                }
            }
            else
            {
                ViewBag.Login = 0;
            }

            return geri;
        }

        [Route("/sepet/sepettensil"), HttpPost]
        public string RemoveCard(int productid)
        {
            string rtn = "";
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                if (Request.Cookies["MyCard"] != null)
                {
                    List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(Request.Cookies["MyCard"]);
                    bool varMi = false;
                    foreach (Card c in cards)
                    {
                        if (c.ProductId == productid)
                        {
                            varMi = true;
                            cards.Remove(c);
                            break;
                        }
                        
                    }
                    if (varMi== true)
                    {
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddHours(3);
                        Response.Cookies.Append("MyCard", JsonConvert.SerializeObject(cards), options);
                        rtn = "OK";
                    }

                }
            }
            else
            {
                ViewBag.Login = 0;
            }

            return rtn;
        }


        [Route("/sepet")]
        public IActionResult Index ()
        {
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                if (Request.Cookies["MyCard"]   != null)
                {
                    List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(Request.Cookies["MyCard"]);
                    if (cards.Count <= 0)
                    {

                        return Redirect("/");
                    }else return View(cards);
                }
                else return Redirect("/");

            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }

            
        }


        [Route("/sepet/aliverisitamamla")]
        public IActionResult CheckOut()
        {
            ViewBag.ShippingFee = 1000;
            if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "User"))
            {
                ViewBag.Login = 1;
                if (Request.Cookies["MyCard"] != null)
                {
                    List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(Request.Cookies["MyCard"]);
                   CheckOutVM checkOutVM = new CheckOutVM()
                   {
                       Order= new Order(),
                       Cards = cards
                   };
                    return View(checkOutVM);
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                ViewBag.Login = 0;
                return Redirect("/");
            }
            
        }


        [Route("/sepet/aliverisitamamla"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(CheckOutVM model)
        {
            model.Order.RecDate = DateTime.Now;
           string orderNumber= model.Order.OrderNumber= DateTime.Now.Microsecond.ToString()+ DateTime.Now.Minute.ToString()+ DateTime.Now.Second.ToString() + DateTime.Now.Hour.ToString()+DateTime.Now.Microsecond.ToString()+DateTime.Now.Microsecond.ToString();
            if (orderNumber.Length > 20) orderNumber = orderNumber.Substring(0,20);
            model.Order.OrderNumber = orderNumber;
            model.Order.OrderStatus = EOrderStatus.Hazirlaniyor;
            model.Order.PaymentOption=EPaymentOption.KrediKarti;
            repoOrder.Add(model.Order);
            List<Card> cards = JsonConvert.DeserializeObject<List<Card>>(Request.Cookies["MyCard"]);
            foreach (Card c in cards)
            {
                OrderDetails od = new OrderDetails
                {
                    OrderID= model.Order.ID,
                    ProductID= c.ProductId,
                    ProductName= c.ProductName,
                    ProductPicture=c.ProductPicture,
                    ProductPrice=c.ProductPrice,
                    Quantity=c.Quantity,

                };
                repoOrderDetails.Add(od);
                
            }

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Append("MyCard", JsonConvert.SerializeObject(cards), options);

            return Redirect("/");
        }
    }
}
