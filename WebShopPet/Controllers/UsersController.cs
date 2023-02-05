using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Models;

namespace WebShopPet.Controllers
{
    public class UsersController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();     

        // GET: Users
        public ActionResult Index()
        {
            LoadCategories();
            return View(db.USERS.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            LoadCategories();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,EMAIL,PASSWORD, BIRTHDAY")] USER user)
        {
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = db.USERS.Any(x => x.EMAIL == user.EMAIL);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");
                    return View(user);
                }
                db.USERS.Add(user);
                db.SaveChanges();
                Session["ID"] = user.ID;
                Session["Name"] = user.NAME;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            LoadCategories();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,SEX,EMAIL,PASSWORD, AVATAR")] USER uSER)
        {
            LoadCategories();
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["IMAGEFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/Assets/User/img/" + FileName);
                        f.SaveAs(UploadPath);
                        uSER.AVATAR = "~/Assets/User/img/" + FileName;
                    }
                    db.Entry(uSER).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Details", uSER);
            }
            catch (Exception e)
            {
                ViewBag.Error(e.Message);
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USER uSER = db.USERS.Find(id);
            db.USERS.Remove(uSER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult IsUserExists(string EMAIL)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!db.USERS.Any(x => x.EMAIL == EMAIL), JsonRequestBehavior.AllowGet);
        }
    }
}
