﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFinalProject.Models
{
    public class Game
    {
        public int Id { get; set; }

        [DisplayName("Game Title"), Required, StringLength(30)]
        public string Title { get; set; }

        [DisplayName("Description"), Required, StringLength(500)]
        public string Description { get; set; }

        [DisplayName("Total Score")]
        [Range(0, 100)]
        public int TotalScore { get; set; }

        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RealeseDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        [DisplayName("Cost"), Required, Range(0.00, 1000.00)]
        public decimal Cost { get; set; }

        public int ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
