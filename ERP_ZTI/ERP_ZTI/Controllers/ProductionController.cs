using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_ZTI.Controllers
{
    public class ProductionController : Controller
    {
        Models.ERP_DBEntities db = new Models.ERP_DBEntities();
        // GET: Production - kolejka
        public ActionResult Index()
        {
            var queue = from q in db.ProductsQueue
                        select q;

            return View(queue.AsEnumerable());
        }
        // GET: Close
        public ActionResult Close(int? id)
        {

            var prod = from p in db.ProductsQueue
                       where(p.QueueID == id)
                       select p;
            int prodAmount = prod.First().Amount.Value;

            var productToUpdate = db.Products.Find(prod.First().ProductID);
            int intAmount;
            int.TryParse(productToUpdate.Amount, out intAmount);
            intAmount += prodAmount;
            productToUpdate.Amount = intAmount.ToString();
            TryUpdateModel(productToUpdate, "", new string[] { "Name, Amount, PlaceX, PlaceY" });
            db.ProductsQueue.Remove(prod.First());
            db.SaveChanges();
            return View();
        }

    }
}