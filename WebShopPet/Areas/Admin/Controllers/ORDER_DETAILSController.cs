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
    public class ORDER_DETAILSController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: Admin/ORDER_DETAILS
        public ActionResult Index()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            var oRDER_DETAILS = db.ORDER_DETAILS.Include(o => o.ORDER).Include(o => o.PRODUCT);
            return View(oRDER_DETAILS.ToList());
        }

        // GET: Admin/ORDER_DETAILS/Details/5
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
            ORDER_DETAILS oRDER_DETAILS = db.ORDER_DETAILS.Find(id);
            if (oRDER_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_DETAILS);
        }

        // GET: Admin/ORDER_DETAILS/Create
        public ActionResult Create()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "ADDRESS");
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME");
            return View();
        }

        // POST: Admin/ORDER_DETAILS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ORDER_ID,QUANTITY,PRODUCT_PRICE,PRODUCT_ID")] ORDER_DETAILS oRDER_DETAILS)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.ORDER_DETAILS.Add(oRDER_DETAILS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "ADDRESS", oRDER_DETAILS.ORDER_ID);
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME", oRDER_DETAILS.PRODUCT_ID);
            return View(oRDER_DETAILS);
        }

        // GET: Admin/ORDER_DETAILS/Edit/5
        public ActionResult Edit(int? id)
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
            ORDER_DETAILS oRDER_DETAILS = db.ORDER_DETAILS.Find(id);
            if (oRDER_DETAILS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "ADDRESS", oRDER_DETAILS.ORDER_ID);
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME", oRDER_DETAILS.PRODUCT_ID);
            return View(oRDER_DETAILS);
        }

        // POST: Admin/ORDER_DETAILS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ORDER_ID,QUANTITY,PRODUCT_PRICE,PRODUCT_ID")] ORDER_DETAILS oRDER_DETAILS)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.Entry(oRDER_DETAILS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ORDER_ID = new SelectList(db.ORDERS, "ID", "ADDRESS", oRDER_DETAILS.ORDER_ID);
            ViewBag.PRODUCT_ID = new SelectList(db.PRODUCTS, "ID", "NAME", oRDER_DETAILS.PRODUCT_ID);
            return View(oRDER_DETAILS);
        }

        // GET: Admin/ORDER_DETAILS/Delete/5
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
            ORDER_DETAILS oRDER_DETAILS = db.ORDER_DETAILS.Find(id);
            if (oRDER_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(oRDER_DETAILS);
        }

        // POST: Admin/ORDER_DETAILS/Delete/5
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
            ORDER_DETAILS oRDER_DETAILS = db.ORDER_DETAILS.Find(id);
            db.ORDER_DETAILS.Remove(oRDER_DETAILS);
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
    }
}
