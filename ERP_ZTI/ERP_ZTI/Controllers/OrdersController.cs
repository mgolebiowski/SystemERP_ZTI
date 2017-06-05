using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_ZTI
{
    public class OrdersController : Controller
    {
        Models.ERP_DBEntities db = new Models.ERP_DBEntities();

        // GET Orders
        public ActionResult Index()
        {
            var notifications = new Models.NotificationModel();
            ViewBag.zeroA = notifications.i_zeroAmount;
            ViewBag.smallA = notifications.i_smallAmount;
            return View();
        }

        // GET: Orders/Orders
        public ActionResult Orders()
        {
            var orders = from o in db.Orders
                        select o;

            return View(orders.AsEnumerable());
        }

        // GET: newOrder
        public ActionResult newOrder()
        {

            ViewBag.nextOrderID = db.Orders.ToArray().LastOrDefault().OrderID + 1;
            ViewBag.products = db.IProducts.AsEnumerable(); 
            ViewBag.contractors = db.Contractors.AsEnumerable();
            return View();
        }

        // POST newOrder
        [HttpPost, ActionName("newOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult newOrderPost([Bind(Include = "OrderID, IProductID, ContractorID, Amount")] Models.Orders newOrder)
        {
            newOrder.OrderID = db.Orders.ToArray().LastOrDefault().OrderID + 1;
            db.Orders.Add(newOrder);
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        // GET: Contractors
        public ActionResult Contractors()
        {
            var contr = from c in db.Contractors
                         select c;

            return View(contr.AsEnumerable());
        }

        // GET: newContractor
        public ActionResult newContractor()
        {

            ViewBag.nextContractorID = db.Contractors.ToArray().LastOrDefault().ContractorID + 1;
            return View();
        }

        // POST newOrder
        [HttpPost, ActionName("newContractor")]
        [ValidateAntiForgeryToken]
        public ActionResult newContractorPost([Bind(Include = "ContractorID,Name,Adress, Phone, NIP")] Models.Contractors newContractor)
        {
            newContractor.ContractorID = db.Contractors.ToArray().LastOrDefault().ContractorID + 1;
            db.Contractors.Add(newContractor);
            db.SaveChanges();
            return RedirectToAction("Contractors");
        }

        // GET: End/id
        public ActionResult End(int? id)
        {
            var orderToRemove = db.Orders.Find(id);

            var productToUpdate = db.IProducts.Find(orderToRemove.IProductID);
            productToUpdate.Amount += orderToRemove.Amount;

            db.Orders.Remove(orderToRemove);
            db.SaveChanges();
            return RedirectToAction("Orders");
        }

        // GET: deleteContractor/id
        public ActionResult deleteContractor(int? id)
        {
            var contractorToRemove = db.Contractors.Find(id);
            db.Contractors.Remove(contractorToRemove);
            db.SaveChanges();
            return RedirectToAction("Contractors");
        }

        // GET: editContractor/id
        public ActionResult editContractor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var contractorToEdit = db.Contractors.Find(id);
            return View(contractorToEdit);
        }

        // POST editContractor
        [HttpPost, ActionName("editContractor")]
        [ValidateAntiForgeryToken]
        public ActionResult editContractorPost(int? id, string name, string adress, string phone, string NIP)
        {
            var contractor = db.Contractors.Find(id);
            contractor.Name = name;
            contractor.Adress = adress;
            contractor.Phone = phone;
            contractor.NIP = NIP;
            var result = TryUpdateModel(contractor);
            if (result) db.SaveChanges();

            return RedirectToAction("Contractors");
        }

    }
}