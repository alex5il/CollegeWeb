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
            // Filter parameters

            var reviews = from r in db.Reviews
                          select r;

            // Order by desc review date
            reviews = reviews.OrderByDescending(s => s.ReviewDate);

            int pageSize = 1; // Latest reviews
            int pageNumber = (page ?? 1); // DEFAULT 1

            var model = reviews.ToPagedList(pageNumber, pageSize);

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("LatestReviewsTemplate", model) : View(model);
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