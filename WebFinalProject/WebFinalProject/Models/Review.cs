using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFinalProject.Models
{
    public class Review
    {
        public int Id { get; set; }

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

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}
