using Weather.Utils;

namespace Weather.WeatherObjects
{
    public class WeatherBase
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        override
        public string ToString()
        {
            return $"Temp=[{(int)this.main.temp}], " +
            $"Humidity=[{this.main.humidity}], " +
            $"Desc=[{this.weather[0].description}], " +
            $"Sunrise=[{TimeConvertor.UTCToLocalDateTime(this.sys.sunrise).ToShortTimeString()}], " +
            $"Sunset=[{TimeConvertor.UTCToLocalDateTime(this.sys.sunset).ToShortTimeString()}]";
            
        }

    }
}
