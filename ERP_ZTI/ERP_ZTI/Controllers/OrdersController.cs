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
            return View();
        }

        // GET: Orders/Orders
        public ActionResult Orders()
        {
            var orders = from o in db.Orders
                        select o;

            return View(orders.AsEnumerable());
        }

        // GET: Contractors
        public ActionResult Contractors()
        {
            var contr = from c in db.Contractors
                         select c;

            return View(contr.AsEnumerable());
        }
    }
}