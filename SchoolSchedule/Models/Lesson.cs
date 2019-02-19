using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolSchedule.Helpers;

namespace SchoolSchedule.Models
{
    public class Lesson : BaseDbObject
    {
        public Enums.Enums.WeekDay WeekDay { get; set; }
        public string Day => EnumHelper.Description(WeekDay);

        [Required]
        [ForeignKey("SubjectGroup")]
        public int SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }

        [Required]
        [ForeignKey("SubjectTeacher")]
        public int SubjectTeacherId { get; set; }
        public SubjectTeacher SubjectTeacher { get; set; }

        public int Order { get; set; }
        
        [ForeignKey("Auditory")]
        public int? AuditoryId { get; set; }
        public Auditory Auditory { get; set; }
    }
}