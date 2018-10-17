using SchoolSchedule.Helpers;
using SchoolSchedule.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolSchedule.ViewModels
{
    public class BaseViewModel<T> where T:BaseDbObject
    {
        public int? ClassYear { get; set; }
        public SelectList ClassYears => SelectListHelper.GetClassYears(ClassYear);
        public IEnumerable<T> List { get; set; }
    }
}