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
    public class PRODUCTsController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: Admin/PRODUCTs
        public ActionResult Index()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            var pRODUCTS = db.PRODUCTS.Include(p => p.BRAND).Include(p => p.CATEGORy);
            return View(pRODUCTS.ToList());
        }

        // GET: Admin/PRODUCTs/Details/5
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
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Admin/PRODUCTs/Create
        public ActionResult Create()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME");
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME");
            return View();
        }

        // POST: Admin/PRODUCTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CATEGORY_ID,BRAND_ID,NAME,IMPORT_PRICE,PRICE,DISCOUNT,COLOR,SIZE,DESCRIPTION,AVAILABLE_QUANTITY,QUANTITY_SOLD,PRIMARY_IMAGE")] PRODUCT pRODUCT)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            try
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                if (ModelState.IsValid)
                {
                    var f = Request.Files["IMAGEFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/Assets/User/img/" + fileName);
                        f.SaveAs(UploadPath);

                        pRODUCT.PRIMARY_IMAGE = "~/Assets/User/img/" + fileName;
                    }
                    db.PRODUCTS.Add(pRODUCT);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
                ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
                return View(pRODUCT);
            }
        }

        // GET: Admin/PRODUCTs/Edit/5
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
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
            ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CATEGORY_ID,BRAND_ID,NAME,IMPORT_PRICE,PRICE,DISCOUNT,COLOR,SIZE,DESCRIPTION,AVAILABLE_QUANTITY,QUANTITY_SOLD,PRIMARY_IMAGE")] PRODUCT pRODUCT)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["IMAGEFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);

                        string UploadPath = Server.MapPath("~/Assets/User/img/" + fileName);
                        f.SaveAs(UploadPath);
                        pRODUCT.PRIMARY_IMAGE = "~/Assets/User/img/" + fileName;
                    }
                    db.Entry(pRODUCT).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
                ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
                return View(pRODUCT);
            }
        }

        // GET: Admin/PRODUCTs/Delete/5
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
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Admin/PRODUCTs/Delete/5
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
            PRODUCT pRODUCT = db.PRODUCTS.Find(id);
            try
            {
                db.PRODUCTS.Remove(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không xóa được bản ghi này!" + ex.Message;
                return View("Delete", pRODUCT);
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
