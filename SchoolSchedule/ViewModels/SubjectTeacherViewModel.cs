using SchoolSchedule.Helpers;
using SchoolSchedule.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolSchedule.ViewModels
{
    public class SubjectTeacherViewModel
    {
        public ClassYears? ClassYear { get; set; }
        public List<SelectListItem> ClassYearses => SelectListHelper.GetClassYears();
        public IEnumerable<SubjectTeacher> SubjectTeachers { get; set; }
    }
}