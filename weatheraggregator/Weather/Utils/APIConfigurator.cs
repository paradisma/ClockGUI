using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Weather.Utils
{
    public static class APIConfigurator
    {
        public static string APIKey = ConfigurationManager.AppSettings["APIKey"];
        public static string APIHost = ConfigurationManager.AppSettings["APIHost"];
    }
}
