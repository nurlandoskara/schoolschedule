using System;
using System.Collections.Generic;

namespace SchoolSchedule.Models.DTO
{
    public class DayDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<LessonDto> Lessons { get; set; }
    }
}