using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_ZTI.Controllers
{
    public class SalesController : Controller
    {
        Models.ERP_DBEntities db = new Models.ERP_DBEntities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = from s in db.Sales
                            select s;

            return View(sales.AsEnumerable());
        }

        // GET: Customers
        public ActionResult Customers()
        {
            var customers = from c in db.Customers
                            select c;

            return View(customers.AsEnumerable());
        }
        // GET: Create
        public ActionResult Create()
        {
            ViewBag.nextQueueId = db.ProductsQueue.Count();
            ViewBag.products = db.Products.ToList();
            return View();
        }
        // POST Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost([Bind(Include = "QueueID,ProductID,Amount")] Models.ProductsQueue productEntry)
        {
            productEntry.QueueID= db.ProductsQueue.Count();
            db.ProductsQueue.Add(productEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}