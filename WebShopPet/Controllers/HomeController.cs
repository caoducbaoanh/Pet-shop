using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Controllers;
using WebShopPet.Models;

namespace WebShopPets.Controllers
{
    public class HomeController : BaseController
    {
        ShopPetDB db = new ShopPetDB();
        public ActionResult Index()
        {
            //if (Session["ID"] != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            
            var listP = db.PRODUCTS.Where(x => x.CATEGORy.ID == 3);
            ViewBag.ListThu = listP;
            var listP2 = db.PRODUCTS.Where(x => x.CATEGORy.ID == 4);
            ViewBag.ListThu2 = listP2;
            LoadCategories();
            var session = Session["Order"];
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,NAME,SEX,EMAIL,PASSWORD,ROLE,STATUS")] USER user)
        {
            LoadCategories();
            if (ModelState.IsValid)
            {
                db.USERS.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Login()
        {
            LoadCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            LoadCategories();
            if (ModelState.IsValid)
            {
                var user = db.USERS.Where(u => u.EMAIL.Equals(email) && u.PASSWORD.Equals(password)).ToList();
                if (user.Count() > 0)
                {
                    Session["NAME"] = user.FirstOrDefault().NAME;
                    Session["EMAIL"] = user.FirstOrDefault().EMAIL;
                    Session["ID"] = user.FirstOrDefault().ID;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công~";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}