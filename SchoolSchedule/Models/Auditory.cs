namespace SchoolSchedule.Models
{
    public class Auditory: BaseDbObject
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string DisplayName => $"{Number}, {Name}";
    }
}