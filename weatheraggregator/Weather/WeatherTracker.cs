using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Utils;
using Weather.WeatherObjects;
using System.Configuration;
using ClockGui.Logger;

namespace Weather
{
    public class WeatherTracker
    {
        private string city;
        private string country;
        private string state;
        private string units;

        //To be used when sending the request to the server.
        private string requestString;
        private RequestUnit requestUnits;

        private int maxFetchAttempts = WeatherSettingsProvider.WeatherFetchLimitOnFailure;

        /// <summary>
        /// Will use the default values from App.Config.
        /// </summary>
        public WeatherTracker()
        {
            this.city = ConfigurationManager.AppSettings["WEATHER_CITY"];
            this.country = ConfigurationManager.AppSettings["WEATHER_COUNTRY"];
            this.state = ConfigurationManager.AppSettings["WEATHER_STATE"];
            this.units = ConfigurationManager.AppSettings["WEATHER_UNITS"];

            this.requestUnits = RequestStringBuilder.StringToUnitEnum(this.units);
            this.requestString = RequestStringBuilder.BuildRequestString(this.requestUnits, this.country, this.city, this.state);

            Logger.Debug($"[{nameof(WeatherTracker)}] [{nameof(WeatherTracker)}] WeatherTracker Created: City: {city} Country: {country} State: {state}");
        }

        public WeatherTracker(string _city, string _country, string _state, string _units)
        {
            this.city = _city;
            this.country = _country;
            this.state = _state;
            this.units = _units;

            this.requestUnits = RequestStringBuilder.StringToUnitEnum(this.units);
            this.requestString = RequestStringBuilder.BuildRequestString(this.requestUnits, this.country, this.city, this.state);
        }

        public async Task<WeatherBase> GetWeatherAsync()
        {
            Logger.Debug($"[{nameof(WeatherTracker)}] [{nameof(GetWeatherAsync)}] Asynchronously getting weather.");

            WeatherBase weather = null;
            bool success = false;
            int i;

            for (i = 1; i <= maxFetchAttempts; i++)
            {
                await Task.Run(() => success = RequestSender.TryGetWeatherResults(requestString, out weather));
                if (success) { break; }

                //If it wasn't successful try again until limit reached.
                Logger.Debug($"[{nameof(WeatherTracker)}] [{nameof(GetWeatherAsync)}] Unable to retreive weather, trying again. On attempt {i} of {maxFetchAttempts}");
            }

            //Log if we got the weather or not and which attempt it was.
            if (success && weather.main != null)
            {
                Logger.Debug($"[{nameof(WeatherTracker)}] [{nameof(GetWeatherAsync)}] Weather retreived after {i} attempt(s). Weather Results = {weather.ToString()}");
                return weather;
            }
            else
            {
                Logger.Error($"[{nameof(WeatherTracker)}] [{nameof(GetWeatherAsync)}] Weather unable to be retreived max attempts [{maxFetchAttempts}] limit reached. Will try again at next poll loop.");
            }

            //Return null. Was not able to retreive the weather.
            return null;
        }

        public bool TryGetWeatherString(out string weatherString)
        {
            Logger.Debug($"[{nameof(WeatherTracker)}] [{nameof(TryGetWeatherString)}] Getting weather string.");

            weatherString = string.Empty;
            var success = RequestSender.TryGetWeatherResults(requestString, out WeatherBase weather);

            if (success)
            {
                weatherString = weather.ToString();
            }

            return success;
        }
    } 
}
