using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Utils
{
    public static class TimeSpanHumanizeExtensions
    {
        public static string Humanize(this TimeSpan timeSpan, int precision)
        {
            var timeParts = GetTimeParts(timeSpan).Where(x => x != null);
            timeParts = timeParts.Take(precision);
            return string.Join(string.Empty, timeParts);
        }

        private static IEnumerable<string> GetTimeParts(TimeSpan timespan)
        {
            var weeks = timespan.Days / 7;
            var daysInWeek = timespan.Days % 7;
            var hours = timespan.Hours;
            var minutes = timespan.Minutes;
            var seconds = timespan.Seconds;

            var outputWeeks = weeks > 0;
            var outputDays = outputWeeks || daysInWeek > 0;
            var outputHours = outputDays || hours > 0;
            var outputMinutes = outputHours || minutes > 0;
            var outputSeconds = outputMinutes || seconds > 0;

            if (outputWeeks)
                yield return GetTimePart("w", weeks);
            if (outputDays)
                yield return GetTimePart("d", daysInWeek);
            if (outputHours)
                yield return GetTimePart("h", hours);
            if (outputMinutes)
                yield return GetTimePart("m", minutes);
            if (outputSeconds)
                yield return GetTimePart("s", seconds);
            else
                yield return "0s";
        }

        private static string GetTimePart(string timeUnit, int unit)
        {
            return unit != 0 ? $"{unit}{timeUnit}" : null;
        }
    }
}
