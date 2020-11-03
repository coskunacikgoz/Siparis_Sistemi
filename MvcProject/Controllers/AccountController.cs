using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class AccountController : Controller
    {

        //public ActionResult Index()
        //{
        //      Guid Northwind Customer tablosundaki id olarak geçer, ardışık harf ve sayılardan oluşur ve eşsiz bir değer oluşturur.
        //    string value = Guid.NewGuid().ToString();
        //    return View();
        //}

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            NorthwindEntities db = new NorthwindEntities();
            Employee loginEmployee = db.Employees.FirstOrDefault(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName);

            if (loginEmployee != null)
            {
                Session["login"] = loginEmployee;
                return RedirectToAction("ListMyProducts", "Admin");
            }

            ModelState.AddModelError("","Kullanici adi veya sifre hatalidir");

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (Session["login"] != null)
            {
                Session.Remove("login");
            }

            return View("Login");
        }
    }
}