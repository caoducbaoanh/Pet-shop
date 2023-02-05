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
using WebShopPet.Controllers;

namespace WebShopPets.Controllers
{
    public class ProductController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: PRODUCTs
        public ActionResult Index(string sortOrder, string SearchString, string currentFilter, int? page, string CategoryID)
        {
            LoadCategories();
            var products = db.PRODUCTS.Select(p => p);
            if (CategoryID != null)
            {
                int? ID = int.Parse(CategoryID);
                products = db.PRODUCTS.Where(p =>p.CATEGORY_ID == ID);
            }
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SapTheoGia = sortOrder == "Gia" ? "gia_desc" : "Gia";
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            //var pRODUCTS = db.PRODUCTS.Include(p => p.BRAND).Include(p => p.CATEGORy).Include(p => p.USER);
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.NAME.Contains(SearchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.NAME);
                    break;
                case "Gia":
                    products = products.OrderBy(s => s.PRICE);
                    break;
                case "gia_desc":
                    products = products.OrderByDescending(s => s.PRICE);
                    break;
                default:
                    products = products.OrderBy(s => s.NAME);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber,pageSize));
        }

        //GET: PRODUCTs/Details/5
            public ActionResult Details(int? id)
            {
                LoadCategories();
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
        

        //    // GET: PRODUCTs/Create
        //    public ActionResult Create()
        //    {
        //        ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME");
        //        ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME");
        //        ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME");
        //        return View();
        //    }

        //    // POST: PRODUCTs/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create([Bind(Include = "ID,USER_ID,CATEGORY_ID,BRAND_ID,NAME,PRICE,DISCOUNT,COLOR,SIZE,DESCRIPTION,AVAILABLE_QUANTITY,QUANTITY_SOLD,PRIMARY_IMAGE")] PRODUCT pRODUCT)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.PRODUCTS.Add(pRODUCT);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
        //        ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
        //        ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", pRODUCT.USER_ID);
        //        return View(pRODUCT);
        //    }

        //    // GET: PRODUCTs/Edit/5
        //    public ActionResult Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        PRODUCT pRODUCT = db.PRODUCTS.Find(id);
        //        if (pRODUCT == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
        //        ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
        //        ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", pRODUCT.USER_ID);
        //        return View(pRODUCT);
        //    }

        //    // POST: PRODUCTs/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit([Bind(Include = "ID,USER_ID,CATEGORY_ID,BRAND_ID,NAME,PRICE,DISCOUNT,COLOR,SIZE,DESCRIPTION,AVAILABLE_QUANTITY,QUANTITY_SOLD,PRIMARY_IMAGE")] PRODUCT pRODUCT)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(pRODUCT).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.BRAND_ID = new SelectList(db.BRANDs, "ID", "NAME", pRODUCT.BRAND_ID);
        //        ViewBag.CATEGORY_ID = new SelectList(db.CATEGORIES, "ID", "NAME", pRODUCT.CATEGORY_ID);
        //        ViewBag.USER_ID = new SelectList(db.USERS, "ID", "NAME", pRODUCT.USER_ID);
        //        return View(pRODUCT);
        //    }

        //    // GET: PRODUCTs/Delete/5
        //    public ActionResult Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        PRODUCT pRODUCT = db.PRODUCTS.Find(id);
        //        if (pRODUCT == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(pRODUCT);
        //    }

        //    // POST: PRODUCTs/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        PRODUCT pRODUCT = db.PRODUCTS.Find(id);
        //        db.PRODUCTS.Remove(pRODUCT);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
        //}
    }
}

