using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFinalProject.Models
{
    public class Cart
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static List<Purchase> GetCart(Controller controller)
        {
            if (controller.Session["Cart"] == null)
            {
                controller.Session.Add("Cart", new List<Purchase>());
            }

            return controller.Session["Cart"] as List<Purchase>;
        }

        public static bool AddToCart(Controller controller, int GameId, int? quantity)
        {
            Game game = db.Games.Find(GameId);

            if (game == null)
            {
                return false;
            }

            List<Purchase> purchases = GetCart(controller);
            Purchase purchase = purchases.Find(p => p.GameId == GameId);

            if (purchase == null)
            {
                purchase = new Purchase { GameId = GameId, Quantity = (quantity ?? 1), Game = game };
                purchases.Add(purchase);
            }
            else
            {
                purchase.Quantity += quantity ?? 1;
            }

            return true;
        }

        public static bool RemoveFromCart(Controller controller, int GameId, int? quantity)
        {
            Game game = db.Games.Find(GameId);

            if (game == null)
            {
                return false;
            }

            List<Purchase> purchases = GetCart(controller);
            Purchase purchase = purchases.Find(p => p.GameId == GameId);

            if (purchase != null)
            {
                purchase.Quantity -= quantity ?? 1;

                if (purchase.Quantity <= 0)
                {
                    purchases.Remove(purchase);
                }
            }

            return true;
        }

        public static void EmptyCart(Controller controller)
        {
            List<Purchase> purchases = GetCart(controller);
            purchases.Clear();
        }

        public static void CheckOut(Controller controller, string UserId)
        {
            List<Purchase> purchases = GetCart(controller);
            decimal Cost = purchases.Sum(p => p.Quantity * p.Game.Cost);

            Transaction transaction = new Transaction { TransactionDate = DateTime.Now, Cost = Cost, UserId = UserId };

            db.Transactions.Add(transaction);
            db.SaveChanges();

            foreach (Purchase p in purchases)
            {
                p.TransactionId = transaction.Id;
                db.Purchases.Add(p);
            }
            db.SaveChanges();

            EmptyCart(controller);
        }
    }
}