using System;
using System.Collections.Generic;
using System.Linq;
using Q42.HueApi;

namespace ClockGui.Logger.Utils
{
    public static class LogStringFormatter
    {
        public static string CollectionToString(IEnumerable<Object> collection)
        {
            string returnStr = "[";
            Object lastItem = collection.Last();

            foreach(Object item in collection)
            {
                returnStr += item.ToString();
                returnStr += item.Equals(lastItem) ? "]" : ", ";
            }
            return returnStr;
        }

        public static string CollectionToString(IEnumerable<Light> collection)
        {
            string returnStr = "[";
            var lastItem = collection.Last();

            foreach (var light in collection)
            {
                returnStr += $"Name={light.Name} Id={light.Id}";
                returnStr += light.Equals(lastItem) ? "]" : ", ";
            }
            return returnStr;
        }
    }
}
