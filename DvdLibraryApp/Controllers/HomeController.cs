using DvdLibraryApp.Models;
using DvdLibraryApp.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DvdLibraryApp.Controllers
{
    public class HomeController : Controller
    {
        // from API codealong
        // basically only here so that VS has something to run in-browser
        public ActionResult Index()
        {
            var repository = new DvdLibraryEF();

            var model = from movie in repository.DVDs
                        select new ListDvdVM
                        {
                            DvdId = movie.DvdId,
                            Title = movie.Title,
                            Director = movie.Director.ToString(),
                            Rating = movie.Rating.ToString()
                        };

            return View(model);
        }
    }
}