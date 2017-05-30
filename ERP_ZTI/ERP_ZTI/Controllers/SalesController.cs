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
            var notifications = new Models.NotificationModel();
            ViewBag.zeroA = notifications.zeroAmount;
            ViewBag.smallA = notifications.smallAmount;
            var sales = from s in db.Sales
                            select s;

            return View(sales.AsEnumerable());
        }
        // GET: Sales/Sales
        public ActionResult Sales()
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
        // GET: CreateCustomer
        public ActionResult CreateCustomer()
        {
            ViewBag.nextCustomerID = db.Customers.ToArray().LastOrDefault().CustomerID + 1;
            return View();
        }
        // POST CreateCustomer
        [HttpPost, ActionName("CreateCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomerPost([Bind(Include = "CustomerID,Name,Adress,Phone,NIP")] Models.Customers customerEntry)
        {
            customerEntry.CustomerID = db.Customers.ToArray().LastOrDefault().CustomerID + 1;
            db.Customers.Add(customerEntry);
            db.SaveChanges();
            return RedirectToAction("Customers");
        }
        // GET: EditCustomer
        public ActionResult EditCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var customer = db.Customers.Find(id);
            return View(customer);
        }
        // POST CreateCustomer
        [HttpPost, ActionName("EditCustomer")]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomerPost(int? id, string name, string adress, string phone, string NIP)
        {
            //ToDo
            var customer = db.Customers.Find(id);
            customer.Name = name;
            customer.Adress = adress;
            customer.Phone = phone;
            customer.NIP = NIP;
            var result = TryUpdateModel(customer);
            if(result) db.SaveChanges();
            
            return RedirectToAction("Customers");
        }

        // GET: Create
        public ActionResult Create()
        {
            ViewBag.nextQueueId = db.ProductsQueue.ToArray().LastOrDefault().QueueID + 1;
            ViewBag.products = db.Products.ToList();
            return View();
        }
        // POST Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost([Bind(Include = "QueueID,ProductID,Amount")] Models.ProductsQueue productEntry)
        {
            productEntry.QueueID = db.ProductsQueue.ToArray().LastOrDefault().QueueID + 1;
            db.ProductsQueue.Add(productEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: SalesSend
        public ActionResult SalesSend(int? id)
        {

            var prod = from s in db.Sales
                       where (s.SalesId == id)
                       select s;
            var sellAmount = prod.First().Amount;

            var productToUpdate = db.Products.Find(prod.First().ProductID);

            int availableAmount;
            int.TryParse(productToUpdate.Amount, out availableAmount);

            if(availableAmount >= sellAmount)
            {
                productToUpdate.Amount = (availableAmount - sellAmount).ToString();
                TryUpdateModel(productToUpdate, "", new string[] { "Name, Amount, PlaceX, PlaceY" });
                db.Sales.Remove(prod.First());
                db.SaveChanges();

                return View();
            }
            else
            {
                ViewBag.error = "There is not enough products in storage.";
                return View();
            }

            
        }
        //GET SalesCreate
        public ActionResult SalesCreate()
        {           
            if (db.Sales.ToArray().Count() == 0)
            {
                ViewBag.nextSalesId = 0;
            }
            else
            {
                ViewBag.nextSalesId = db.Sales.ToArray().LastOrDefault().SalesId + 1;
            }
            ViewBag.customers = db.Customers.ToList();
            ViewBag.products = db.Products.ToList();
            return View();
        }
        // POST Create
        [HttpPost, ActionName("SalesCreate")]
        [ValidateAntiForgeryToken]
        public ActionResult SalesCreatePost([Bind(Include = "SalesId,CustomerID,ProductID,Amount")] Models.Sales productEntry)
        {
            if(db.Sales.ToArray().Count() == 0)
            {
                productEntry.SalesId = 0;
            }
            else
            {
                productEntry.SalesId = db.Sales.ToArray().LastOrDefault().SalesId + 1;
            }
            db.Sales.Add(productEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}