using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolSchedule.Models
{
    public class Lesson : BaseDbObject
    {
        public DayOfWeek DayOfWeek { get; set; }
        [ForeignKey("SubjectGroup")]
        public int SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }

        [ForeignKey("SubjectTeacher")]
        public int SubjectTeacherId { get; set; }
        public SubjectTeacher SubjectTeacher { get; set; }

        public int Order { get; set; }
    }
}