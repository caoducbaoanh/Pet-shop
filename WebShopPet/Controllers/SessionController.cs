using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Controllers;
using WebShopPet.Models;

namespace ShopPet.Controllers
{
    public class SessionController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();
        // GET: Session
        public ActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string EMAIL, string PASSWORD)
        {
            LoadCategories();
            if (ModelState.IsValid)
            {
                var user = db.USERS.Where(u => u.EMAIL.Equals(EMAIL) && u.PASSWORD.Equals(PASSWORD)).ToList();
                if(user.Count() > 0)
                {
                    if(user.FirstOrDefault().ROLE == 0)
                    {
                        Session["ID"] = user.FirstOrDefault().ID;
                        Session["Name"] = user.FirstOrDefault().NAME;
                        Session["Role"] = 0;
                        return RedirectToAction("Index", "Home");
                    }else if(user.FirstOrDefault().ROLE == 1)
                    {
                        Session["ID"] = user.FirstOrDefault().ID;
                        Session["Name"] = user.FirstOrDefault().NAME;
                        Session["Role"] = 1;
                        return Redirect("http://localhost:53553/Admin/Homes");
                    }
                }
                else
                {
                    ViewBag.error_login = "Đăng nhập không thành công";
                    return RedirectToAction("create");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Name"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}