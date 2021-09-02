using System.Configuration;

namespace ClockGUI.Utils
{
    public static class ConfigurationProvider
    {
        /// <summary>
        /// Returns the time in milliseconds that the clock's date and time will be updated.
        /// </summary>
        public static int TimeUpdateInterval
        {
            get
            {
                bool success = int.TryParse(ConfigurationManager.AppSettings["TimeUpdateInterval"], out int interval);

                if (success)
                {
                    return interval;
                }
                return 1000;
            }
        }

        public static int WeatherUpdateInterval
        {
            get
            {
                bool success = int.TryParse(ConfigurationManager.AppSettings["WeatherUpdateInterval"], out int interval);

                if (success)
                {
                    return interval;
                }
                return 600000;
            }
        }

        
    }
}
