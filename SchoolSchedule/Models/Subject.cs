using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SchoolSchedule.Models
{
    public class Subject : BaseDbObject
    {
        public string NameKz { get; set; }
        public string NameEn { get; set; }
        public string NameRu { get; set; }

        [Description("Subject Name")]
        public string DisplayName => string.Concat(NameKz, ' ', ClassYear);
    }
}