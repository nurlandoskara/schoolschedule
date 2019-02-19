using System.ComponentModel;

namespace SchoolSchedule.Enums
{
    public class Enums
    {
        public enum WeekDay
        {
            [Description("Дүйсенбі")]
            Monday = 1,
            [Description("Сейсенбі")]
            Tuesday = 2,
            [Description("Сәрсенбі")]
            Wednesday = 3,
            [Description("Бейсенбі")]
            Thursday = 4,
            [Description("Жұма")]
            Friday = 5,
            [Description("Сенбі")]
            Saturday = 6
        }
    }
}