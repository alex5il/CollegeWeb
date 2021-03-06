﻿using System;
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

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("WebServiceTemplate") : View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Giga games game store.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "College of Management contact page.";

            return View();
        }
    }
}