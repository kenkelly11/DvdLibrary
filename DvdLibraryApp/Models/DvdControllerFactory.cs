using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DvdLibraryApp;

namespace DvdLibraryApp.Models
{
    public class DvdControllerFactory
    {
        public static IDvdRepository GetRepo()
        {
            // instantiates the appropriate implementation of IDvdRepository
            switch (Settings.GetRepositoryType())
            {
                // depending on the value of Mode in Web.config, the repository from which we draw data changes
                case "Mock": // value of Mode
                    return new DvdRepositoryMock(); // implementation
                case "EF": // value of Mode
                    return new DvdRepositoryEF(); // implementation
                default:
                    throw new Exception("Not a valid Repository type");
            }
        }
    }
}