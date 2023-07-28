namespace DvdLibraryApp.Migrations
{
    using DvdLibraryApp.Models;
    using DvdLibraryApp.Models.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DvdLibraryApp.Models.EF.DvdLibraryEF>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        // the Seed method allows us to put data into a database as part of the migration
        // called after updating database
        protected override void Seed(DvdLibraryApp.Models.EF.DvdLibraryEF context)
        {


            // AddOrUpdate puts data into a table if it doesn't exist or updates it if it already exists
            context.Directors.AddOrUpdate(
                    g => g.DirectorName,
                    new DirectorEF { DirectorName = "George" },
                    new DirectorEF { DirectorName = "Tarantino"},
                    new DirectorEF { DirectorName = "Gerwig"},
                    new DirectorEF { DirectorName = "Berry"}
                );

            context.Ratings.AddOrUpdate(
                r => r.Rating,
                new RatingEF { Rating = "G" },
                new RatingEF { Rating = "PG" },
                new RatingEF { Rating = "PG-13" },
                new RatingEF { Rating = "R" }
            );

            context.SaveChanges();


            context.DVDs.AddOrUpdate(
                    m => m.Title, // first parameter, column used to identify the record
                    new DvdEF // second parameter, model objects we are populating the table with
                    {
                        Title = "Star Wars",
                        DirectorId = context.Directors.First(g => g.DirectorName == "George").DirectorId,
                        RatingId = context.Ratings.First(r => r.Rating == "PG").RatingId,
                        ReleaseYear = "1972",
                        Notes = "Decent"

                    }
                );

            context.DVDs.AddOrUpdate(
          m => m.Title, 
          new DvdEF
          {
              Title = "Star Wars 2",
              DirectorId = context.Directors.First(g => g.DirectorName == "George").DirectorId,
              RatingId = context.Ratings.First(r => r.Rating == "PG").RatingId,
              ReleaseYear = "1973",
              Notes = "Just okay"
          }

      );

        }
    }
}

