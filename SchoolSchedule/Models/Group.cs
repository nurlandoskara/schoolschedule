namespace SchoolSchedule.Models
{
    public class Group : BaseDbObject
    {
        public string ClassLetter { get; set; }
        public int CreatedYear { get; set; }

        public string DisplayName => string.Concat(ClassYear, ClassLetter);
    }
}