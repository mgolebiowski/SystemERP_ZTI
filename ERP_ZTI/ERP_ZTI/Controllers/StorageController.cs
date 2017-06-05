using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP_ZTI.Controllers
{
    public class StorageController : Controller
    {
        Models.ERP_DBEntities db = new Models.ERP_DBEntities();
        // GET: Storage
        public ActionResult Index()
        {
            var storage = from s in db.Products
                        select s;
            return View(storage);
        }

        // GET Change/ID
        public ActionResult Change(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var storage = db.Products.Find(id);
            return View(storage);
        }

        // POST Change/ID
        [HttpPost, ActionName("Change")]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePost(int? id, string placeX, int placeY)
        {
            var productToUpdate = db.Products.Find(id);
            productToUpdate.PlaceX = placeX;
            productToUpdate.PlaceY = placeY;
            TryUpdateModel(productToUpdate, "", new string[] { "Name, Amount, PlaceX, PlaceY" });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: IntStorage
        public ActionResult IntStorage()
        {
            var storage = from s in db.IProducts
                          select s;
            return View(storage);
        }

        // GET Edit/ID
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var storage = db.IProducts.Find(id);
            ViewBag.contrList = db.Contractors.AsEnumerable();
            return View(storage);
        }

        // POST Edit/ID
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, string name, int amount, int contractorID)
        {
            var productToUpdate = db.IProducts.Find(id);
            productToUpdate.Name = name;
            productToUpdate.Amount = amount;
            productToUpdate.ContractorID = contractorID;

            TryUpdateModel(productToUpdate);
            db.SaveChanges();
            return RedirectToAction("IntStorage");
        }
    }
}