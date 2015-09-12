using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFinalProject.Models
{
    public class Game
    {
        public int ID { get; set; }

        [DisplayName("Name"), Required, StringLength(30)]
        public string Title { get; set; }

        [DisplayName("Description"), Required, StringLength(500)]
        public string Content { get; set; }

        [DisplayName("TotalScore")]
        [Range(0, 100)]
        public int TotalScore { get; set; }

        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime RealeseDate { get; set; }

        [DisplayName("Cost"), Required]
        public decimal Cost { get; set; }

        public int ApplicationUserID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        // Many to many
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
