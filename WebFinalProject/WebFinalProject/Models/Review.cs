using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFinalProject.Models
{
    public class Review
    {
        public int ID { get; set; }

        [DisplayName("Title"), Required, StringLength(30)]
        public string Title { get; set; }

        [DisplayName("Content"), Required, StringLength(1000)]
        public string Content { get; set; }

        [DisplayName("Score"), Required]
        [Range(0, 100)]
        public int Score { get; set; }

        [DisplayName("Review Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime ReviewDate { get; set; }

        [DisplayName("Text"), Required]
        public string Text { get; set; }

        public int ApplicationUserID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser User { get; set; }

        public int GameID { get; set; }

        [ForeignKey("GameID")]
        public virtual Game Game { get; set; }
    }
}
