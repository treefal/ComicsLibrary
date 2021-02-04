using ComicsLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsLibrary.Services
{
    public static class Helpers
    {
        public static IEnumerable<string> HumanizeHours(
            IEnumerable<LibraryLocationHours> locationHours)
        {
            var hours = new List<string>();

            foreach (var time in locationHours)
            {
                var day = HumanizeDay(time.DayOfWeek);
                var openTime = HumanizeTime(time.OpenTime);
                var closeTime = HumanizeTime(time.CloseTime);

                var timeEntry = $"{day} {openTime} to {closeTime}";
                hours.Add(timeEntry);
            }

            return hours;
        }

        public static string HumanizeDay(int number)
        {
            // Our data corellates 1 to Sunday, so subtract 1 
            return Enum.GetName(typeof(DayOfWeek), number - 1);
        }

        public static string HumanizeTime(int time)
        {
            return TimeSpan.FromHours(time).ToString("hh':'mm");
        }
    }
}
