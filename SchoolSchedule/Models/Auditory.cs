namespace SchoolSchedule.Models
{
    public class Auditory: BaseDbObject
    {
        public string Name { get; set; }
        public string DisplayName => Name;
    }
}