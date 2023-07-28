using DvdLibraryApp.Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdLibraryApp.Models
{
    public class DvdEF
    {
        [Key]
        public int DvdId { get; set; }
        public int DirectorId { get; set; }
        public int? RatingId { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Notes { get; set; }

        public virtual DirectorEF Director { get; set; }
        public virtual RatingEF Rating { get; set; }
    }
}