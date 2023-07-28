using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DvdLibraryApp.Models.EF
{
    // DbContext is a class provided by EF to establish connection to the database
    public class DvdLibraryEF : DbContext
    {
        // links model properties to the database through a connection string in Web.config
        public DvdLibraryEF()
            : base("DvdLibrary")
        {
        }

        // the three tables in the SQL database
        public DbSet<DvdEF> DVDs { get; set; }
        public DbSet<RatingEF> Ratings { get; set; }
        public DbSet<DirectorEF> Directors { get; set; }
    }
}