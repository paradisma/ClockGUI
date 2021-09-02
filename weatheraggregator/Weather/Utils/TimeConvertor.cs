using System;

namespace Weather.Utils
{
    public class TimeConvertor
    {
        public static DateTime UTCToLocalDateTime(int utcInt)
        {
            DateTime startEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            startEpoch = startEpoch.AddSeconds(utcInt);

            return startEpoch.ToLocalTime();
        }
    }
}
