using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSchedule.Models
{
    public class SubjectTeacher : BaseDbObject
    {
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}