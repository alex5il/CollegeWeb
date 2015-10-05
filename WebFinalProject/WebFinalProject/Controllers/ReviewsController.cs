using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebFinalProject.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using LinqKit;

namespace WebFinalProject.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET : Reviews - paged
        public ActionResult Index(string searchString, string emailFilter, int? scoreAbove, int? page, int? myReviews)
        {
            var reviews = from r in db.Reviews
                          select r;

            if (Request.IsAjaxRequest())
            {
                var predicate = PredicateBuilder.True<Review>();

                if (!String.IsNullOrEmpty(searchString)) predicate = predicate.And(review => review.Game.Title.Contains(searchString));

                if (!String.IsNullOrEmpty(emailFilter)) predicate = predicate.And(review => review.User.UserName.Contains(emailFilter));

                // int is always not null
                if (scoreAbove != 0) predicate = predicate.And(game => game.Score >= scoreAbove);

                if (myReviews == 1) predicate = predicate.And(review => review.User.UserName == User.Identity.Name);

                reviews = reviews.AsExpandable().Where(predicate);
            }

            // Order by desc release date
            reviews = reviews.OrderByDescending(g => g.ReviewDate);

            int pageSize = 3;
            int pageNumber = (page ?? 1); // DEFAULT 1

            var model = reviews.ToPagedList(pageNumber, pageSize);

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("ReviewsTemplate", model) : View(model);
        }

        public ActionResult LatestReviews()
        {
            var reviews = (from r in db.Reviews
                           orderby r.ReviewDate descending
                           select r).Take(5);

            var model = reviews.ToList();

            return (ActionResult)PartialView("_LatestReviews", model);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        [Authorize(Roles = "User")]
        public ActionResult Create(int gameId)
        {
            ViewBag.GameID = gameId;

            ViewBag.GameTitle = db.Games.Find(gameId).Title;

            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create([Bind(Include = "Id,Title,Content,Score,ReviewDate,UserId,GameId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);

                // Recalculate game's average score and add it
                var gameReviews = (from r in db.Reviews
                                   where r.GameId == review.GameId
                                   select r.Score).ToList();

                gameReviews.Add(review.Score);

                db.Games.Find(review.GameId).AverageScore = (int)gameReviews.Average();

                review.ReviewDate = DateTime.Now;
                review.UserId = User.Identity.GetUserId();

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameID = review.GameId;
            ViewBag.GameTitle = db.Games.Find(review.GameId).Title;

            return View(review);
        }

        // GET: Reviews/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = db.Reviews.Find(id);

            if (User.Identity.GetUserId() != review.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (review == null)
            {
                return HttpNotFound();
            }

            ViewBag.GameID = review.GameId;
            ViewBag.GameTitle = db.Games.Find(review.GameId).Title;

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,Score,ReviewDate,UserId,GameId")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;

                // Recalculate game's average score and add it
                var gameReviews = (from r in db.Reviews
                                   where r.GameId == review.GameId && r.Id != review.Id
                                   select r.Score).ToList();

                gameReviews.Add(review.Score);

                db.Games.Find(review.GameId).AverageScore = (int)gameReviews.Average();

                review.UserId = User.Identity.GetUserId();
                review.ReviewDate = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameID = review.GameId;
            ViewBag.GameTitle = db.Games.Find(review.GameId).Title;

            return View(review);
        }

        // GET: Reviews/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);

            // Recalculate game's average score and add it
            var gameReviews = (from r in db.Reviews
                               where r.GameId == review.GameId && r.Id != review.Id
                               select r.Score).ToList();

            if (gameReviews.Count() > 0)
            {
                db.Games.Find(review.GameId).AverageScore = (int)gameReviews.Average();
            }
            else
            {
                db.Games.Find(review.GameId).AverageScore = 0;
            }

            db.Reviews.Remove(review);

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
