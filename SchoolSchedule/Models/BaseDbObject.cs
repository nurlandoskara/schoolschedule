using System.ComponentModel.DataAnnotations;

namespace SchoolSchedule.Models
{
    public class BaseDbObject
    {
        [Required]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int ClassYear { get; set; }
    }
}