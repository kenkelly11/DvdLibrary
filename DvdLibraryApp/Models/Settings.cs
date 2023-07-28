using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DvdLibraryApp.Models
{
    public class Settings
    {
        private static string _repositoryType;
        public static string GetRepositoryType()
        {
            // _repositoryType will always be null or empty until we instantiate it with something, 
            // so it will always point to the value of "Mode" in AppSettings 
            if (string.IsNullOrEmpty(_repositoryType))
            {
                _repositoryType = ConfigurationManager.AppSettings["Mode"].ToString();
            }

            return _repositoryType;
        }
    }
}