using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebFinalProject.Models
{
    public class Purchase
    {
        [Column(Order = 0), Key, ForeignKey("Transaction")]
        public int TransactionId { get; set; }

        [Column(Order = 1), Key, ForeignKey("Game")]
        public int GameId { get; set; }

        [DisplayName("Quantity"), Required, Range(0, 999)]
        public int Quantity { get; set; }

        public virtual Transaction Transaction { get; set; }

        public virtual Game Game { get; set; }
    }
}