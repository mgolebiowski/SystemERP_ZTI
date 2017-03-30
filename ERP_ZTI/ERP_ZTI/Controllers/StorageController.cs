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
            /*
             * Dodac dwie tabele (produkty nasze Products oraz polprodukty 
             * IProducts)
             * 
             *
             */
            return View();
        }
    }
}