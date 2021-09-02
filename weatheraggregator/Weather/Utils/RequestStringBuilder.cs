using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClockGui.Logger;

namespace Weather.Utils
{
    public static class RequestStringBuilder
    {
        public static string BuildRequestString(RequestUnit unit, string country, string city, string state = null)
        {
            Logger.Debug($"[{nameof(RequestStringBuilder)}] [{nameof(BuildRequestString)}] Building request string with params unit={unit.ToString()}, country={country}, city={city}, state={state}");

            string units = UnitEnumToString(unit);
            string location = (state == null) ? city : $"{city},{state}";
            string apiHost = WeatherSettingsProvider.APIHost;
            string apiKey = WeatherSettingsProvider.APIKey;
            return $"{apiHost}units={units}&q={location}%2C%20{country}&appid={apiKey}";
        }

        public static string BuildRequestStringByGeo()
        {
            return null;
        }


        #region Public Helpers.


        public static string UnitEnumToString(RequestUnit unit)
        {
            string enumAsStr = string.Empty;

            switch (unit)
            {
                case RequestUnit.IMPERIAL:
                    enumAsStr = "imperial";
                    break;

                case RequestUnit.METRIC:
                    enumAsStr = "metric";
                    break;
            }

            return enumAsStr;
        }

        public static RequestUnit StringToUnitEnum(string unit)
        {
            unit = unit.ToLower();
            RequestUnit strAsEnum = RequestUnit.UNKNOWN;

            switch (unit)
            {
                case "imperial":
                    strAsEnum = RequestUnit.IMPERIAL;
                    break;

                case "metric":
                    strAsEnum = RequestUnit.METRIC;
                    break;
            }

            return strAsEnum;
        }

        #endregion
    }
}
