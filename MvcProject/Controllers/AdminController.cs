using MvcProject.Attributes;
using MvcProject.Models;
using MvcProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    [EmployeeAuthorize]
    public class AdminController : Controller
    {
        NorthwindEntities _db = new NorthwindEntities();

        [HttpGet]
        public ActionResult ListMyProducts()
        {
            ListProductsViewModel model = new ListProductsViewModel
            {
                Products = _db.Products.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult AddMyProduct()
        {
            ProductManiViewModel model = new ProductManiViewModel
            {
                Product = new Product(),
                Categories = _db.Categories.ToList(),
                Suppliers = _db.Suppliers.ToList(),
                ButtonName = "Add Product"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddMyProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                ProductManiViewModel model = new ProductManiViewModel
                {
                    Product = new Product(),
                    Categories = _db.Categories.ToList(),
                    Suppliers = _db.Suppliers.ToList(),
                    ButtonName = "Add Product"
                };

                return View(model);
            }

            _db.Products.Add(product);
            _db.SaveChanges();

            TempData["message"] = "Veri girildi";

            return RedirectToAction("ListMyProducts");
            //return View("ListMyProducts");
        }

        [HttpGet]
        public ActionResult UpdateMyProduct(int id)
        {
            ProductManiViewModel model = new ProductManiViewModel
            {
                Product = _db.Products.Find(id),
                Categories = _db.Categories.ToList(),
                Suppliers = _db.Suppliers.ToList(),
                ButtonName = "Update Product"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateMyProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                ProductManiViewModel model = new ProductManiViewModel
                {
                    Product = _db.Products.Find(product.ProductID),
                    Categories = _db.Categories.ToList(),
                    Suppliers = _db.Suppliers.ToList(),
                    ButtonName = "Update Product"
                };

                return View(model);
            }

            _db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();

            TempData["message"] = "Veri guncellendi";

            return RedirectToAction("ListMyProducts");
        }

        [HttpGet]
        public ActionResult DeleteMyProduct(int id)
        {
            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();

            TempData["message"] = "Veri silindi";

            return RedirectToAction("ListMyProducts");
        }
    }
}