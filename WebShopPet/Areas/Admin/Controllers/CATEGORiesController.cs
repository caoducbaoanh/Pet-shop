using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Models;

namespace WebShopPet.Areas.Admin.Controllers
{
    public class CATEGORiesController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: Admin/CATEGORies
        public ActionResult Index()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            return View(db.CATEGORIES.ToList());
        }

        // GET: Admin/CATEGORies/Details/5
        public ActionResult Details(int? id)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORy cATEGORy = db.CATEGORIES.Find(id);
            if (cATEGORy == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORy);
        }

        // GET: Admin/CATEGORies/Create
        public ActionResult Create()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            ViewBag.PARENT_ID = new SelectList(db.CATEGORIES, "ID", "NAME");
            return View();
        }

        // POST: Admin/CATEGORies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,PARENT_ID")] CATEGORy cATEGORy)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.CATEGORIES.Add(cATEGORy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cATEGORy);
        }

        // GET: Admin/CATEGORies/Edit/5
        public ActionResult Edit(int? id)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            ViewBag.PARENT_ID = new SelectList(db.CATEGORIES, "ID", "NAME");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORy cATEGORy = db.CATEGORIES.Find(id);
            if (cATEGORy == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORy);
        }

        // POST: Admin/CATEGORies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,PARENT_ID")] CATEGORy cATEGORy)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.Entry(cATEGORy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cATEGORy);
        }

        // GET: Admin/CATEGORies/Delete/5
        public ActionResult Delete(int? id)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORy cATEGORy = db.CATEGORIES.Find(id);
            if (cATEGORy == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORy);
        }

        // POST: Admin/CATEGORies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            CATEGORy cATEGORy = db.CATEGORIES.Find(id);
            try
            {
                db.CATEGORIES.Remove(cATEGORy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không xóa được bản ghi này!" + ex.Message;
                return View("Delete", cATEGORy);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
