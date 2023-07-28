using DvdLibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibraryApp
{
    public interface IDvdRepository
    {
        Dvd Get(int dvdId);
        List<Dvd> GetAll();
        void Add(Dvd dvd);
        void Delete(int dvdId);
        void Edit(Dvd dvd);
        List<Dvd> GetName(string dvdTitle);
        List<Dvd> GetDirector(string dvdDirector);
        List<Dvd> GetRating(string dvdRating);
        List<Dvd> GetYear(string dvdYear);
        
    }
}
