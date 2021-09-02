using System;
using Weather.WeatherObjects;
using RestSharp;
using Newtonsoft.Json;
using ClockGui.Logger;

namespace Weather.Utils
{
    public static class RequestSender
    {
        /// <summary>
        /// Gets the current weather from the API using the requestString and assigning results to WeatherBase object.
        /// </summary>
        /// <param name="weather">Weather object results will be assigned to, will be null if unsuccessful.</param>
        /// <returns>If API request was able to get the current weather.</returns>
        public static bool TryGetWeatherResults(string requestString, out WeatherBase weather)
        {
            Logger.Debug($"[{nameof(RequestSender)}] [{nameof(TryGetWeatherResults)}] Attempting to get weather using requestString={requestString}");

            weather = null;

            var client = new RestClient(requestString);
            var request = new RestRequest(Method.GET);

            request.AddHeader("x-rapidapi-host", APIConfigurator.APIHost);
            request.AddHeader("x-rapidapi-key", APIConfigurator.APIKey);

            IRestResponse response = client.Execute(request);

            if(response.ResponseStatus == ResponseStatus.Completed)
            {
                if(response.Content != string.Empty)
                {
                    try
                    {
                        weather = JsonConvert.DeserializeObject<WeatherBase>(response.Content);
                    }
                    catch (Exception)
                    {
                        Logger.Error($"[{nameof(RequestSender)}] [{nameof(TryGetWeatherResults)}] Exception thrown when attempting to deserialize weather response.");
                        return false;
                    }
                }
                else
                {
                    Logger.Error($"[{nameof(RequestSender)}] [{nameof(TryGetWeatherResults)}] Weather response content returned was empty.");
                    return false;
                }
            }
            else if(response.ResponseStatus == ResponseStatus.Error)
            {
                Logger.Warn($"[{nameof(RequestSender)}] [{nameof(TryGetWeatherResults)}] There was an error attempting to get a response from the server: {response.ErrorMessage}");
                return false;
            }

            Logger.Debug($"[{nameof(RequestSender)}] [{nameof(TryGetWeatherResults)}] Successfully retreived weather.");
            return true;
        }
    }
}
