using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Models;
using PagedList;

namespace WebShopPet.Areas.Admin.Controllers
{
    public class ORDERsController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: Admin/ORDERs
        public ActionResult Index(string sortOrder)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            ViewBag.SaptheoDate = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.SaptheoStatus = sortOrder == "Status" ? "status_desc" : "Gia";
            ViewBag.SaptheoAddress = sortOrder == "ADDRESS" ? "address_desc" : "ADDRESS";

            var oRDERS = db.ORDERS.Include(o => o.USER);

            switch (sortOrder)
            {
                case "date_desc":
                    oRDERS = oRDERS.OrderByDescending(s => s.DATE);
                    break;
                case "status_desc":
                    oRDERS = oRDERS.OrderByDescending(s => s.STATUS);
                    break;
                case "Status":
                    oRDERS = oRDERS.OrderBy(s => s.STATUS);
                    break;
                case "address_desc":
                    oRDERS = oRDERS.OrderByDescending(s => s.ADDRESS);
                    break;
                case "ADDRESS":
                    oRDERS = oRDERS.OrderBy(s => s.ADDRESS);
                    break;
                default:
                    oRDERS = oRDERS.OrderBy(s => s.DATE);
                    break;
            }
            return View(oRDERS.ToList());
        }

        // GET: Admin/ORDERs/Details/5
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
            ORDER oRDER = db.ORDERS.Find(id);
            List<ORDER_DETAILS> listOrder = new List<ORDER_DETAILS>();
            listOrder = db.ORDER_DETAILS.Where(s => s.ORDER_ID == id).Select(s =>s).ToList();
            ViewBag.ListOrder = listOrder;
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }

        // GET: Admin/ORDERs/Create
        public ActionResult Create()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME");
            return View();
        }

        // POST: Admin/ORDERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,USER_ID,TOTAL_AMOUNT,STATUS,ADDRESS,PHONE,DATE")] ORDER oRDER)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.ORDERS.Add(oRDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", oRDER.USER_ID);
            return View(oRDER);
        }

        // GET: Admin/ORDERs/Edit/5
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
            ORDER oRDER = db.ORDERS.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", oRDER.USER_ID);
            return View(oRDER);
        }

        // POST: Admin/ORDERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,USER_ID,TOTAL_AMOUNT,STATUS,ADDRESS,PHONE,DATE")] ORDER oRDER)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.Entry(oRDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", oRDER.USER_ID);
            return View(oRDER);
        }

        // GET: Admin/ORDERs/Delete/5
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
            ORDER oRDER = db.ORDERS.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }

        // POST: Admin/ORDERs/Delete/5
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
            ORDER oRDER = db.ORDERS.Find(id);
            db.ORDERS.Remove(oRDER);
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
