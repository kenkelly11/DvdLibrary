using DvdLibraryApp.Models;
using DvdLibraryApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibraryApp
{
    // these methods are called from the controller when Web.config settings are set to "EF"
    public class DvdRepositoryEF : IDvdRepository
    {
        public void Add(Dvd dvd) // take user input as a parameter
        {
            var repository = new DvdLibraryEF();
            var newDirector = repository.Directors.FirstOrDefault(d => d.DirectorName == dvd.Director);
            if (newDirector == null) // if the director isn't already in the DbSet, 
            {
                // add the new director to the Directors DbSet
                newDirector = repository.Directors.Add(new DirectorEF { DirectorName = dvd.Director });
                repository.SaveChanges();
            }

            var newRating = repository.Ratings.FirstOrDefault(d => d.Rating == dvd.Rating);
            if (newRating == null) 
            {
                newRating = repository.Ratings.Add(new RatingEF { Rating = dvd.Rating });
                repository.SaveChanges();
            }

            // add the new dvd to the DVDs DbSet
            var DVD = repository.DVDs.Add(
                new DvdEF
                {
                    DvdId = dvd.DvdId,
                    Director = newDirector,
                    Notes = dvd.Notes,
                    ReleaseYear = dvd.ReleaseYear,
                    Rating = newRating,
                    Title = dvd.Title
                });

            repository.SaveChanges();
        }

        public void Delete(int dvdId)
        {
            var repository = new DvdLibraryEF();
            var DVD = repository.DVDs.Find(dvdId); // finds the Dvd with the associated Id value in the DVDs DbSet

            if (DVD != null) 
            {
                Dvd deleted = new Dvd
                {
                    DvdId = DVD.DvdId,
                    Director = DVD.Director.ToString(),
                    Notes = DVD.Notes,
                    ReleaseYear = DVD.ReleaseYear,
                    Rating = DVD.Rating.Rating,
                    Title = DVD.Title
                };
                repository.DVDs.Remove(DVD); // removes all properties of the Dvd
                repository.SaveChanges();
            }
        }

        public void Edit(Dvd dvd)
        {
            var repository = new DvdLibraryEF();
            var newDirector = repository.Directors.FirstOrDefault(d => d.DirectorName == dvd.Director);

            if (newDirector == null) // if the director isn't already in the DbSet, 
            {
                // add them to the Directors DbSet
                newDirector = repository.Directors.Add(new DirectorEF { DirectorName = dvd.Director });
                repository.SaveChanges();
            }

            var newRating = repository.Ratings.FirstOrDefault(d => d.Rating == dvd.Rating);
            if (newRating == null)
            {
                newRating = repository.Ratings.Add(new RatingEF { Rating = dvd.Rating });
                repository.SaveChanges();
            }

            var theDvd = repository.DVDs.FirstOrDefault(d => d.DvdId == dvd.DvdId);

            // update the Dvd properties with the new properties
            theDvd.Director = newDirector;
            theDvd.Notes = dvd.Notes;
            theDvd.ReleaseYear = dvd.ReleaseYear;
            theDvd.Rating = newRating;
            theDvd.Title = dvd.Title;

            repository.SaveChanges();
        }

        public Dvd Get(int dvdId)
        {
            var repository = new DvdLibraryEF();
            var DVD = repository.DVDs.Find(dvdId);
            Dvd returnList = new Dvd();

            if (DVD != null)
            {
                // save the Dvd properties to the empty list properties
                returnList.DvdId = DVD.DvdId;
                returnList.Director = DVD.Director.DirectorName;
                returnList.Notes = DVD.Notes;
                returnList.ReleaseYear = DVD.ReleaseYear;
                returnList.Rating = DVD.Rating.Rating;
                returnList.Title = DVD.Title;
                return returnList;
            }
            else
            {
                return null;
            }
        }

        public List<Dvd> GetAll()
        {
            var repository = new DvdLibraryEF();
            List<Dvd> returnDictionary = new List<Dvd>();
            var dvdList = repository.DVDs.ToList(); // save all the Dvds in the repository to the list

            foreach (DvdEF t in dvdList)
            {
                returnDictionary.Add(
                new Dvd
                {
                    DvdId = t.DvdId,
                    Director = t.Director.DirectorName,
                    Notes = t.Notes,
                    ReleaseYear = t.ReleaseYear,
                    Rating = t.Rating.Rating,
                    Title = t.Title

                });
            }
            return returnDictionary;
        }

        public List<Dvd> GetDirector(string dvdDirector)
        {
            var repository = new DvdLibraryEF();
            List<Dvd> returnDictionary = new List<Dvd>();
            var dvdList = repository.DVDs.ToList(); // put all Dvds in the repository into the dvdList

            foreach (DvdEF t in dvdList) // loop through every dvd in the list
            {
                var DVD = t;
                if (DVD.Director.DirectorName == dvdDirector) // search for the user-written director name in the list
                {
                    returnDictionary.Add( // when a match is found, return all properties associated with the chosen director
                    new Dvd
                    {
                        DvdId = t.DvdId,
                        Director = t.Director.DirectorName,
                        Notes = t.Notes,
                        ReleaseYear = t.ReleaseYear,
                        Rating = t.Rating.Rating,
                        Title = t.Title
                    });
                };
            }
            return returnDictionary;
        }

        public List<Dvd> GetName(string dvdTitle)
        {
            var repository = new DvdLibraryEF();
            List<Dvd> returnDictionary = new List<Dvd>();
            var dvdList = repository.DVDs.ToList();

            foreach (DvdEF t in dvdList)
            {
                var DVD = t;
                if (DVD.Title == dvdTitle)
                {
                    returnDictionary.Add(
                    new Dvd
                    {
                        DvdId = t.DvdId,
                        Director = t.Director.DirectorName,
                        Notes = t.Notes,
                        ReleaseYear = t.ReleaseYear,
                        Rating = t.Rating.Rating,
                        Title = t.Title

                    });
                };
            }
            return returnDictionary;
        }

        public List<Dvd> GetRating(string dvdRating)
        {
            var repository = new DvdLibraryEF();
            List<Dvd> returnDictionary = new List<Dvd>();
            var dvdList = repository.DVDs.ToList();

            foreach (DvdEF t in dvdList)
            {
                var DVD = t;
                if (DVD.Rating.Rating == dvdRating)
                {
                    returnDictionary.Add(
                    new Dvd
                    {
                        DvdId = t.DvdId,
                        Director = t.Director.DirectorName,
                        Notes = t.Notes,
                        ReleaseYear = t.ReleaseYear,
                        Rating = t.Rating.Rating,
                        Title = t.Title
                    });
                };
            }
            return returnDictionary;
        }

        public List<Dvd> GetYear(string dvdYear)
        {
            var repository = new DvdLibraryEF();
            List<Dvd> returnDictionary = new List<Dvd>();
            var dvdList = repository.DVDs.ToList();

            foreach (DvdEF t in dvdList)
            {
                var DVD = t;
                if (DVD.ReleaseYear == dvdYear)
                {
                    returnDictionary.Add(
                    new Dvd
                    {
                        DvdId = t.DvdId,
                        Director = t.Director.DirectorName,
                        Notes = t.Notes,
                        ReleaseYear = t.ReleaseYear,
                        Rating = t.Rating.Rating,
                        Title = t.Title
                    });
                };
            }
            return returnDictionary;
        }
    }
}