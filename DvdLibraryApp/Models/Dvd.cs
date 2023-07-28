using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DvdLibraryApp.Models
{
    public class Dvd
    {
        [Key]
        public int DvdId { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Notes { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
    }
}