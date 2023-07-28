using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DvdLibraryApp.Models
{
    public class EditDvdVM
    {
        [Required]
        public int DvdId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ReleaseYear { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Rating { get; set; }
        public string Notes { get; set; }
    }
}