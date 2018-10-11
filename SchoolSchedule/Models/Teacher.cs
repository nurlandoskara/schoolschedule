using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolSchedule.Models
{
    public class Teacher : BaseDbObject
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string PatronymicName { get; set; }

        [Description("Teacher Name")]
        public string DisplayName => string.Concat(Firstname, ' ', Surname, ' ', PatronymicName);
    }
}