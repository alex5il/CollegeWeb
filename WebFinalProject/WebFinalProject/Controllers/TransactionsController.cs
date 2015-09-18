using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebFinalProject.Models;
using Microsoft.AspNet.Identity;

namespace WebFinalProject.Controllers
{
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.User);
            return View(transactions.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult MyTransactions()
        {
            string UserId = User.Identity.GetUserId();

            var transactions = db.Transactions.Include(t => t.User).Where(t => t.UserId == UserId);
            return View("Index", transactions.ToList());
        }

        [Authorize(Roles = "User")]
        public ActionResult ShoppingCart()
        {
            return View(Cart.GetCart(this));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult AddToCart(int GameId, int? amount)
        {
            if (!Cart.AddToCart(this, GameId, amount))
            {
                return HttpNotFound();
            }

            return View("ShoppingCart", Cart.GetCart(this));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult RemoveFromCart(int GameId, int? amount)
        {
            if (!Cart.RemoveFromCart(this, GameId, amount))
            {
                return HttpNotFound();
            }

            return PartialView("CartTemplate", Cart.GetCart(this));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult EmptyCart()
        {
            Cart.EmptyCart(this);

            return PartialView("CartTemplate", Cart.GetCart(this));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult CheckOut()
        {
            Cart.CheckOut(this, User.Identity.GetUserId());

            return RedirectToAction("Index", "Home");
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<Purchase> purchases = db.Purchases.Include(p => p.Game.Genre).Where(p => p.TransactionId == id);

            if (purchases.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(purchases);
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
