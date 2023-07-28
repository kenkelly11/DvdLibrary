using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibraryApp.Models
{
    public class ListDvdVM
    {
        public int DvdId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Rating { get; set; }
    }
}