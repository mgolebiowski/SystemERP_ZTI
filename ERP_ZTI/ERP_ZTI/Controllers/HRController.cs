using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_ZTI.Models;

namespace ERP_ZTI.Controllers
{
    public class HRController : Controller
    {
        // GET: HR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Manage()
        {
            var context = new ApplicationDbContext();

            var allUsers = context.Users.ToList();

            return View(allUsers);

        }
    }
}