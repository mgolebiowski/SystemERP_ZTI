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
            
            //ToDo
            // W momencie wyslania do magazynu nalezy dodac odpowiedni amount do produktu w magazynie

            return View();
        }
    }
}