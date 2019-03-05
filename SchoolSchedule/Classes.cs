using System.Collections.Generic;

namespace SchoolSchedule
{
    public class Classes
    {
        public static Dictionary<int, string> LessonTimes()
        {
            var lessonTimes = new Dictionary<int, string>
            {
                {0, ""},
                {1, "8:00-8:40"},
                {2, "8:45-9:25"},
                {3, "9:40-10:20"},
                {4, "10:25-11:05"},
                {5, "11:20-12:00"},
                {6, "12:05-12:45"},
                {7, "12:50-13:30"},
                {8, "14:00-14:40"},
                {9, "14:45-15:25"},
                {10, "15:40-16:20"},
                {11, "16:25-17:05"},
                {12, "17:20-18:00"},
                {13, "18:05-18:45"},
                {14, "18:50-19:30"}
            };
            return lessonTimes;
        }
    }
}