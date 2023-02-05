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
    public class USERsController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: Admin/USERs
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SapTheoSex = sortOrder == "Sex" ? "sex_desc" : "Sex";
            ViewBag.SapTheoRole = sortOrder == "Role" ? "role_desc" : "Role";
            ViewBag.SapTheoStatus = sortOrder == "Status" ? "status_desc" : "Status";

            var users = db.USERS.Select(p => p);

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(p => p.NAME.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.NAME);
                    break;
                case "sex_desc":
                    users = users.OrderByDescending(s => s.SEX);
                    break;
                case "Sex":
                    users = users.OrderBy(s => s.SEX);
                    break;
                case "role_desc":
                    users = users.OrderByDescending(s => s.ROLE);
                    break;
                case "Role":
                    users = users.OrderBy(s => s.ROLE);
                    break;
                case "status_desc":
                    users = users.OrderByDescending(s => s.STATUS);
                    break;
                case "Status":
                    users = users.OrderBy(s => s.STATUS);
                    break;
                default:
                    users = users.OrderBy(s => s.NAME);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/USERs/Details/5
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
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: Admin/USERs/Create
        public ActionResult Create()
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            return View();
        }

        // POST: Admin/USERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,SEX,BIRTHDAY,EMAIL,PASSWORD,ROLE,STATUS,AVATAR")] USER uSER)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.USERS.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uSER);
        }

        // GET: Admin/USERs/Edit/5
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
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: Admin/USERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,SEX,BIRTHDAY,EMAIL,PASSWORD,ROLE,STATUS,AVATAR")] USER uSER)
        {
            int? role = 0;
            if (Session["Role"] != null) role = int.Parse(Session["Role"].ToString());
            if (Session["ID"] == null & role != 1)
            {
                return Redirect("http://localhost:53553/Session/Create");
            }
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uSER);
        }

        // GET: Admin/USERs/Delete/5
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
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: Admin/USERs/Delete/5
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
            USER uSER = db.USERS.Find(id);
            try
            {
                db.USERS.Remove(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không xóa được bản ghi này!" + ex.Message;
                return View("Delete", uSER);
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
