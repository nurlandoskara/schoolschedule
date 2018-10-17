using SchoolSchedule.Helpers;
using SchoolSchedule.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolSchedule.ViewModels
{
    public class SubjectTeacherViewModel
    {
        public int? ClassYear { get; set; }
        public SelectList ClassYears => SelectListHelper.GetClassYears(ClassYear);
        public IEnumerable<SubjectTeacher> SubjectTeachers { get; set; }
    }
}