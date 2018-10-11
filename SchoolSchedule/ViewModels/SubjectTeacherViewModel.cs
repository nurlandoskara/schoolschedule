using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolSchedule.Helpers;
using SchoolSchedule.Models;

namespace SchoolSchedule.ViewModels
{
    public class SubjectTeacherViewModel
    {
        public ClassYears ClassYear { get; set; }
        public List<SelectListItem> ClassYearses => EnumHelper.GetClassYears();
        public IEnumerable<SubjectTeacher> SubjectTeachers { get; set; }
    }
}