using DvdLibraryApp.Models;
using DvdLibraryApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Configuration;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;
using AcceptVerbsAttribute = System.Web.Http.AcceptVerbsAttribute;

namespace DvdLibraryApp.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        // 
        private static IDvdRepository _dvdRepository = DvdControllerFactory.GetRepo();

        [EnableCors(origins: "*", headers: "*", methods: "get")]
        [Route("dvds/{select}/{search}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Search(string search, string select)
        {
            // runs the code block with the corresponding drop down menu selection 

            if (select == "Title") 
            {
                var getName = _dvdRepository.GetName(search); // call GetName method in associated repo

                if (getName == null || getName.Count == 0)
                {
                    return NotFound();
                }

                else
                {
                    return Ok(getName);
                }
            }

            if (select == "Director Name")
            {
                var getDirector = _dvdRepository.GetDirector(search);

                if (getDirector == null || getDirector.Count == 0)
                {
                    return NotFound();
                }

                else
                {
                    return Ok(getDirector);
                }
            }

            if (select == "Release Year")
            {
                var getYear = _dvdRepository.GetYear(search);

                if (getYear == null || getYear.Count == 0)
                {
                    return NotFound();
                }

                else
                {
                    return Ok(getYear);
                }
            }

            if (select == "Rating")
            {
                var getRating = _dvdRepository.GetRating(search);

                if (getRating == null || getRating.Count == 0)
                {
                    return NotFound();
                }

                else
                {
                    return Ok(getRating);
                }
            }

            else 
            { 
                return NotFound(); 
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "get")]
        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult DvdList()
        {
            return Ok(_dvdRepository.GetAll());
        }

        [EnableCors(origins: "*", headers: "*", methods: "get")]
        [Route("dvd/{dvdId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int dvdId)
        {
            Dvd movie = _dvdRepository.Get(dvdId);

            if (movie == null)
            {
                return NotFound();
            }

            else
            {
                return Ok(movie);
            }
        }

        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult AddDvd(AddDvdVM model)
        {
            Dvd movie = new Dvd();

            if (ModelState.IsValid)
            {
                // sets the new Dvd properties 
                movie.Title = model.Title;
                movie.Rating = model.Rating;
                movie.Director = model.Director;
                movie.ReleaseYear = model.ReleaseYear;

                _dvdRepository.Add(movie);

                return Ok(movie);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("dvd/{dvdId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int dvdId)
        {
            _dvdRepository.Delete(dvdId); // removes the dvdId from the repository
            return Ok(_dvdRepository.GetAll());
        }

        [Route("dvd/{dvdId}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Edit(EditDvdVM model)
        { 
            Dvd movie = _dvdRepository.Get(model.DvdId); // old properties are stored in movie variable

            if (ModelState.IsValid)
            {
                // save the updated model properties to the movie variable
                movie.DvdId = model.DvdId;
                movie.Title = model.Title;
                movie.Rating = model.Rating;
                movie.Director = model.Director;
                movie.ReleaseYear = model.ReleaseYear;

                _dvdRepository.Edit(movie);

                return Ok(movie);
            }

            else 
            { 
                return NotFound(); 
            }
        }
    }
}