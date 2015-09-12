using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFinalProject.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [DisplayName("Genre Name"), Required, StringLength(30)]
        public string Name { get; set; }

        [DisplayName("Description"), Required, StringLength(500)]
        public string Description { get; set; }
    }
}