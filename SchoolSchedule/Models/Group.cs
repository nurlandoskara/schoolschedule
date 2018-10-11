using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolSchedule.Models
{
    public class Group : BaseDbObject
    {
        public string ClassLetter { get; set; }
        public ClassYears ClassYear { get; set; }
        public int CreatedYear { get; set; }

        public string DisplayName => string.Concat(ClassYear, ClassLetter);
    }
}