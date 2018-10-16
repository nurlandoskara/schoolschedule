using SchoolSchedule.Helpers;
using SchoolSchedule.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolSchedule.ViewModels
{
    public class BaseViewModel<T> where T:BaseDbObject
    {
        public ClassYears? ClassYear { get; set; }
        public List<SelectListItem> ClassYearses => SelectListHelper.GetClassYears();
        public IEnumerable<T> List { get; set; }
    }
}