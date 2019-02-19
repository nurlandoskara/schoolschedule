using System;
using System.Collections.Generic;

namespace SchoolSchedule.Models.DTO
{
    public class DayDto
    {
        public Enums.Enums.WeekDay WeekDay { get; set; }
        public List<LessonDto> Lessons { get; set; }
    }
}