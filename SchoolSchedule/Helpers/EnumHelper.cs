using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolSchedule.Helpers
{
    public class EnumHelper
    {
        public static List<SelectListItem> GetClassYears()
        {
            return Enum.GetValues(typeof(ClassYears)).Cast<ClassYears>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int) v).ToString()
            }).ToList();
        }
    }
}