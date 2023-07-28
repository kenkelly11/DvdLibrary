using DvdLibraryApp.Models;
using DvdLibraryApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DvdLibraryApp
{
    // controller calls these methods and uses this data when web.config setting is set to "Mock"
    public class DvdRepositoryMock : IDvdRepository
    {
        // create a Rating's list, add sample data
        private List<Rating> _ratings = new List<Rating>
            {new Rating
            {RatingId = 1, RatingType = "PG-13" },
                new Rating
                {
                    RatingId = 2, RatingType = "G"
                },
                new Rating
                {
                    RatingId = 3, RatingType = "R"
                }
            };

        // create a Director's list, add sample data
        private List<Director> _directors = new List<Director>
            {
                 new Director
            {DirectorId = 1, DirectorName = "Reitman" },
                new Director
                {
                    DirectorId = 2, DirectorName = "Stanton"
                }
    ,            new Director{
        DirectorId = 3, DirectorName = "Avildsen"
                },
                new Director
                {
                    DirectorId = 4, DirectorName="Dayton"
                },
                new Director
                {
                    DirectorId=5, DirectorName="Allers"
                }
            };

        // create a Dvd's list, add sample data
        private List<Dvd> _dvds = new List<Dvd>
    {
        new Dvd
            { DvdId=1, Title="Ghostbusters", Director = "Reitman" , ReleaseYear="1984", Notes="Not bad", Rating = "PG-13"},
        new Dvd
            { DvdId=2, Title="Finding Nemo", Director = "Stanton", ReleaseYear="2003", Notes="Fishy fun!", Rating = "G"},
        new Dvd
            { DvdId=3, Title="Rocky", Director = "Avildsen", ReleaseYear="1976", Notes="Empowering", Rating = "PG-13" },
        new Dvd
        {
            DvdId=4, Title="Little Miss Sunshine", Director="Dayton", ReleaseYear="2006", Notes="Road trip", Rating="PG-13"
        },
        new Dvd
        {
            DvdId=5, Title="Lion King", Director="Allers", ReleaseYear="1994", Notes="Emotional", Rating="G"
        }
    };

        public void Add(Dvd dvd)
        {
            dvd.DvdId = _dvds.Max(m => m.DvdId) + 1; // sets the DvdId to the next highest unassigned number
            _dvds.Add(dvd); // adds the new Dvd to the list
        }

        public void Edit(Dvd dvd)
        {
            var found = _dvds.FirstOrDefault(m => m.DvdId == dvd.DvdId); // find Dvd with the associated Id

            if (found != null)
            {
                found = dvd; // save Dvd properties
            }
        }

        public void Delete(int dvdId)
        {
            _dvds.RemoveAll(m => m.DvdId == dvdId);
        }

        public List<Dvd> GetAll()
        {
            return _dvds; // return all Dvds
        }

        public Dvd Get(int dvdId)
        {
            return _dvds.FirstOrDefault(m => m.DvdId == dvdId); // return specific Dvd
        }


        // Search methods


        public List<Dvd> GetName(string dvdTitle)
        {
            List<Dvd> list = new List<Dvd>();
            var selection = _dvds.FirstOrDefault(m => m.Title == dvdTitle); // search Dvd list for the title, return associated Dvd

            if (selection == null) 
            {
                return null;
            }

            else
            {
                list.Add(selection); // add selected Dvd to the list
                return list;
            }
        }

        public List<Dvd> GetDirector(string dvdDirector)
        {
            List<Dvd> list = new List<Dvd>();
            var selection = _dvds.FirstOrDefault(m => m.Director == dvdDirector);

            if (selection == null)
            {
                return null;
            }

            else
            {
                list.Add(selection);
                return list;
            }
        }

        public List<Dvd> GetRating(string dvdRating)
        {
            List<Dvd> list = new List<Dvd>();
            var selection = _dvds.FirstOrDefault(m => m.Rating == dvdRating);

            if (selection == null)
            {
                return null;
            }

            else
            {
                list.Add(selection);
                return list;
            }
        }

        public List<Dvd> GetYear(string dvdYear)
        {
            List<Dvd> list = new List<Dvd>();
            var selection = _dvds.FirstOrDefault(m => m.ReleaseYear == dvdYear);

            if (selection == null)
            {
                return null;
            }

            else
            {
                list.Add(selection);
                return list;
            }
        }
    }
}