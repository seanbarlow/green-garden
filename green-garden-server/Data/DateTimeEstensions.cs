using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace green_garden_server.Data
{
    public static class DateTimeEstensions
    {
        public static DateTime ConvertTimeFromUtc(this DateTime dateTime, string timeZoneId = "Central Standard Time")
        {
            var cstZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, cstZone);
        }
    }
}
