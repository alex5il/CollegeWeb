using System;
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

        [DisplayName("Average Score")]
        [Range(0, 100)]
        public int AverageScore { get; set; }

        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        [DisplayName("Cost"), Required, Range(0.00, 1000.00)]
        public decimal Cost { get; set; }

        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
