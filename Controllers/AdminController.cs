using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FptBook.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["UserName"] == Session["UserName"] && Session["UserNameAdmin"] != null)
            {
                return View();
            }
            //return View("Error");
            return RedirectToAction("Error");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}