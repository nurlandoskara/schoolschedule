using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolSchedule.Helpers
{
    public static class DictionaryHelper
    {
        private static Dictionary<DayOfWeek, string> DayOfWeeks { get; set; } = new Dictionary<DayOfWeek, string>
        {
            {DayOfWeek.Monday, "Дүйсенбі"},
            {DayOfWeek.Tuesday, "Сейсенбі"},
            {DayOfWeek.Wednesday, "Сәрсенбі"},
            {DayOfWeek.Thursday, "Бейсенбі"},
            {DayOfWeek.Friday, "Жұма"},
            {DayOfWeek.Saturday, "Сенбі"},
            {DayOfWeek.Sunday, "Жексенбі"}
        };

        public static string GetDayOfWeekName(DayOfWeek day)
        {
            return DayOfWeeks.FirstOrDefault(x => x.Key == day).Value;
        }
    }
}