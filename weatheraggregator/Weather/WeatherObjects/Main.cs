using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.WeatherObjects
{
    public class Main
    {
        public float temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }

        override
        public string ToString()
        {
            return $"Temp {temp}\n" +
                   $"Humidity {humidity}%\n" +
                   $"Today's Min {temp_min}\n" +
                   $"Today's Max {temp_max}";
        }
    }
}
