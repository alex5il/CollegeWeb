using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebFinalProject.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Total Cost"), Required, Range(0.00, 10000000.00)]
        public decimal Cost { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [DisplayName("Transaction Date"), Required]
        public DateTime CommentDate { get; set; }

        public int ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}