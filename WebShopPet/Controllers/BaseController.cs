using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Models;

namespace WebShopPet.Controllers
{
    public class BaseController : Controller
    {
        private ShopPetDB db = new ShopPetDB();
        // GET: Base
        public void LoadCategories()
        {

            ViewBag.Categories = db.CATEGORIES;
        }
    }
}