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

    }
}