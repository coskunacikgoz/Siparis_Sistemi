using MvcProject.Models;
using MvcProject.Models.Carts;
using MvcProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class CustomerController : Controller
    {

        [HttpGet]
        public ActionResult ListMyProducts(int page = 1)
        {
            List<Product> products;

            using (NorthwindEntities db = new NorthwindEntities())
            {
                products = db.Products.ToList();
            }

            int pageSize = 10;

            ListProductsViewModel model = new ListProductsViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageSize = pageSize,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(products.Count / (double)pageSize)
                //Math, matematik işlemlerinde kullanılan hazır bir class'tır.
                //Math.Ceiling ise ondalıklı sayıları yukarı yuvarlayan bir metoddur. 8.2'yi 9 yapar.
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult AddMyCart(int id)
        {
            MyCart myCart;

            if (Session["cart"] == null)
            {
                myCart = new MyCart();
            }
            else
            {
                myCart = Session["cart"] as MyCart;
            }

            Product product;
            using (NorthwindEntities db = new NorthwindEntities())
            {
                product = db.Products.Find(id);
            }

            CartItem cartItem = new CartItem();

            cartItem.ID = product.ProductID;
            cartItem.Name = product.ProductName;
            cartItem.Price = (decimal)product.UnitPrice;

            myCart.Add(cartItem);

            Session["cart"] = myCart;

            return RedirectToAction("ListMyProducts");
        }

        [HttpGet]
        public ActionResult ListMyCartItems()
        {
            if (Session["cart"] != null)
            {
                MyCart myCart = Session["cart"] as MyCart;

                ListCartItemsViewModel model = new ListCartItemsViewModel
                {
                    CartItems = myCart.CartItems,
                    TotalPrice = myCart.TotalPrice
                };

                return View(model);
            }

            return View();
        }

        [HttpGet]
        public ActionResult DeleteMyCart(int id)
        {
            if (Session["cart"] != null)
            {
                MyCart myCart = Session["cart"] as MyCart;
                myCart.Delete(id);
            }

            return RedirectToAction("ListMyCartItems");
        }

        [HttpPost]
        public ActionResult UpdateMyCart(params short[] amounts)
        {
            if (Session["cart"] != null)
            {
                MyCart myCart = Session["cart"] as MyCart;
                myCart.Update(amounts);
            }

            return RedirectToAction("ListMyCartItems");
        }
    }
}