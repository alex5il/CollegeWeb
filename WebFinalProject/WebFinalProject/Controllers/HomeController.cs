using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFinalProject.Models;
using PagedList;

namespace WebFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Index also returns ajax request for latest reviews
        public ActionResult Index(int? page)
        {
            ViewBag.page = (page ?? 1);

            if (Request.IsAjaxRequest())
            {
                return PartialView("WebServiceTemplate");
            }
            else
            {
                return View();
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}