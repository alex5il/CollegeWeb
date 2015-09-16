﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebFinalProject.Models;
using PagedList;

namespace WebFinalProject.Controllers
{
    public class CatalogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Catalog
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            // Filter parameters
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSort = sortOrder;

            var games = from g in db.Games
                        select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Title.Contains(searchString)
                                    || g.Description.Contains(searchString));
            }

            // Order by desc release date
            games = games.OrderByDescending(g => g.ReleaseDate);

            int pageSize = 3;
            int pageNumber = (page ?? 1); // DEFAULT 1

            var model = games.ToPagedList(pageNumber, pageSize);

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("CatalogTemplate", model) : View(model);
        }

        public ActionResult TopRatedGames()
        {
            var games = (from g in db.Games
                         orderby g.AverageScore
                         select g).Take(5);

            var model = games.ToList();

            return (ActionResult)PartialView("_TopGameList", model);
        }

        // GET: Catalog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            // Get all the reviews scores to that specific game
            var reviews = from r in db.Reviews
                          where r.GameId == id
                          select r.Score;

            var ceilings = new[] { 60, 80, 100 };
            IEnumerable<IGrouping<int, int>> groupings = reviews.GroupBy(item => ceilings.FirstOrDefault(ceiling => ceiling >= item));

            if (groupings.SingleOrDefault(x => x.Key == 60) != null)
            {
                ViewBag.Bad = groupings.SingleOrDefault(x => x.Key == 60).Count();
            }

            if (groupings.SingleOrDefault(x => x.Key == 80) != null)
            {
                ViewBag.Good = groupings.SingleOrDefault(x => x.Key == 80).Count();
            }

            if (groupings.SingleOrDefault(x => x.Key == 100) != null)
            {
                ViewBag.Great = groupings.SingleOrDefault(x => x.Key == 100).Count();
            }

            return View(game);
        }

        // GET: Catalog/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return View();
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Title,Description,AverageScore,ReleaseDate,Cost,GenreId")] Game game,
                                   HttpPostedFileBase titleImg, HttpPostedFileBase thumbnailImg, HttpPostedFileBase video)
        {
            if (ModelState.IsValid)
            {
                game.AverageScore = 0;
                db.Games.Add(game);
                db.SaveChanges();

                string fileName;
                string path;

                if (titleImg != null && titleImg.ContentLength > 0)
                {
                    fileName = game.Id + "title.png";
                    path = Server.MapPath("~/Content/Media/");
                    titleImg.SaveAs(Path.Combine(path, fileName));
                }

                if (thumbnailImg != null && thumbnailImg.ContentLength > 0)
                {
                    fileName = game.Id + "title.png";
                    path = Server.MapPath("~/Content/Media/");
                    thumbnailImg.SaveAs(Path.Combine(path, fileName));
                }

                if (video != null && video.ContentLength > 0)
                {
                    fileName = game.Id + ".mp4";
                    path = Server.MapPath("~/Uploads");
                    video.SaveAs(Path.Combine(path, fileName));
                }

                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", game.GenreId);
            return View(game);
        }

        // GET: Catalog/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", game.GenreId);
            return View(game);
        }

        // POST: Catalog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,AverageScore,ReleaseDate,Cost,GenreId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", game.GenreId);
            return View(game);
        }

        // GET: Catalog/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Catalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
