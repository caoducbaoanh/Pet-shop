using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebShopPet.Models;

namespace WebShopPet.Controllers
{
    public class ORDERsController : BaseController
    {
        private ShopPetDB db = new ShopPetDB();

        // GET: Orders
        public ActionResult Index()
        {
            LoadCategories();
            var order = Session["Order"];
            var list = new List<ORDER_DETAILS>();

            if (Session["ID"] != null)
            {
                int UserID = int.Parse(Session["ID"].ToString());
                var ListOrders = db.ORDERS.Where(x => x.USER.ID == UserID);
                List<ORDER_DETAILS> ListOrderItems = new List<ORDER_DETAILS>();
                foreach (var item in ListOrders)
                {
                    ListOrderItems.AddRange(item.ORDER_DETAILS);
                }
                ViewBag.OrderItems = ListOrderItems;
            }
            if (order != null)
            {
                list = (List<ORDER_DETAILS>)order;
            }
            return View(list);
        }

        // GET: Orders/Create
        public ActionResult Create(int ProductID, int Quantity)
        {
            var order = Session["Order"];
            int? quan = 0;
            int number_order = 0;
            PRODUCT product = (PRODUCT)db.PRODUCTS.Single(x => x.ID == ProductID);
            var list = (List<ORDER_DETAILS>)order;
            if (order != null)
            {
                if (list.Exists(x => x.PRODUCT.ID == ProductID))
                {
                    foreach (var item in list)
                    {
                        if (item.PRODUCT.ID == product.ID)
                        {
                            item.QUANTITY = item.QUANTITY + Quantity;
                            quan = item.QUANTITY;
                        }
                    }
                    Session["Order"] = list;
                }
                else
                {
                    var item = new ORDER_DETAILS();
                    item.PRODUCT = product;
                    item.PRODUCT = product;
                    item.QUANTITY = Quantity;
                    list.Add(item);
                    Session["Order"] = list;
                }

            }
            else
            {
                var item = new ORDER_DETAILS();
                item.PRODUCT = product;
                item.QUANTITY = Quantity;
                list = new List<ORDER_DETAILS>();
                list.Add(item);
                Session["Order"] = list;
            }
             return Json(new { Message = "Đặt hàng thành công!", Quantity = quan, orders = list.Count}, JsonRequestBehavior.AllowGet);

        }

        // GET: ORDERs/Delete/5
        public ActionResult Delete(int? id)
        {
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

        public ActionResult DeleteCart(int ProductID)
        {
            var order = Session["Order"];
    
            if (order != null)
            {
                var list = (List<ORDER_DETAILS>)order;
                if (list.Exists(x => x.PRODUCT.ID == ProductID))
                {
                    foreach (var item in list)
                    {
                        if (item.PRODUCT.ID == ProductID)
                        {
                            list.Remove(item);
                            break;
                        }
                    }
                    Session["Order"] = list;
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateOrder(int []quantity)
        {
            var order = Session["Order"];
            if (order != null)
            {
                var list = (List<ORDER_DETAILS>)order;
                for(int i = 0; i < list.Count; i++)
                {
                    list[i].QUANTITY = quantity[i];
                }
                Session["Order"] = list;
            }
            return RedirectToAction("Index");

        }


        public ActionResult PayOrder(string ADDRESS, string PHONE)
        {
            ORDER cart = new ORDER();
            cart.USER_ID = int.Parse(Session["ID"].ToString());
            cart.STATUS = 0;
            cart.TOTAL_AMOUNT = 0;
            cart.ADDRESS = ADDRESS;
            cart.PHONE = PHONE;
            cart.DATE = DateTime.UtcNow.Date;
            var order = Session["Order"];
            int? totalamount = 0;
            db.ORDERS.Add(cart);
            if (order != null)
            {
                var list = (List<ORDER_DETAILS>)order;
                foreach(var item in list)
                {
                    ORDER_DETAILS order_details = new ORDER_DETAILS();
                    order_details.ORDER_ID = cart.ID;
                    order_details.PRODUCT_ID = item.PRODUCT.ID;
                    order_details.PRODUCT_PRICE = item.PRODUCT.PRICE;
                    order_details.QUANTITY = item.QUANTITY;
                    PRODUCT product = (PRODUCT)db.PRODUCTS.Single(x => x.ID == item.PRODUCT.ID);
                    product.AVAILABLE_QUANTITY -=  item.QUANTITY;
                    product.QUANTITY_SOLD += item.QUANTITY;
                    db.ORDER_DETAILS.Add(order_details);
                    totalamount += item.QUANTITY * item.PRODUCT.PRICE;
                    db.SaveChanges();
                }
            }
            cart.TOTAL_AMOUNT = (int)totalamount;
            db.SaveChanges();
            Session["Order"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult  Checkout()
        {
            LoadCategories();
            if (Session["ID"] == null){
                return RedirectToAction("Create", "Session");
            }
            else
            {
                var order = Session["Order"];
                var list = new List<ORDER_DETAILS>();
                if (order != null)
                {
                    list = (List<ORDER_DETAILS>)order;
                }
                return View(list);
            }
        }

        public ActionResult LoadTempQuantity(int ID)
        {
            int? quantity = 0;
            var order = Session["Order"];
            var list = new List<ORDER_DETAILS>();
            if (order != null)
            {
                list = (List<ORDER_DETAILS>)order;
            }
            foreach (var item in list)
            {
                if(item.PRODUCT.ID == ID)
                {
                    quantity = item.QUANTITY;
                    break;
                }
            }
            return Json(new {Quantity = quantity}, JsonRequestBehavior.AllowGet);
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
