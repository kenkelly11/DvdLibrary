using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdLibraryApp.Models.EF
{
    public class RatingEF
    {
        [Key]
        public int RatingId { get; set; }
        public string Rating { get; set; }
    }
}