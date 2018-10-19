using System.ComponentModel;

namespace SchoolSchedule.Models
{
    public class Subject : BaseDbObject
    {
        public string NameKz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }

        [Description("Subject Name")]
        public string DisplayName => string.Concat(NameKz, ", ", ClassYear, " класс");
    }
}