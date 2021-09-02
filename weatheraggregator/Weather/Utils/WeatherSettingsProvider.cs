using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Weather.Utils
{
    public static class WeatherSettingsProvider
    {
        public static int WeatherFetchLimitOnFailure
        {
            get
            {
                bool success = int.TryParse(ConfigurationManager.AppSettings["WeatherFetchLimitOnFailure"], out int result);

                return success ? result : 1;
            }
        }

        public static string APIKey
        {
            get { return ConfigurationManager.AppSettings["APIKey"]; }
        }

        public static string APIHost
        {
            get { return ConfigurationManager.AppSettings["APIHost"]; }
        }
    }
}
