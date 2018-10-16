using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolSchedule.Models;

namespace SchoolSchedule.Helpers
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetClassYears()
        {
            return Enum.GetValues(typeof(ClassYears)).Cast<ClassYears>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int) v).ToString()
            }).ToList();
        }

        public static SelectList GetSelectableGroups(this IEnumerable<Group> groups, int? selectedValue = null)
        {
            return new SelectList(groups, "Id", "DisplayName", selectedValue);
        }

        public static SelectList GetSelectableSubjectTeachers(this IEnumerable<SubjectTeacher> subjectTeachers, int? selectedValue = null)
        {
            return new SelectList(subjectTeachers, "Id", "Teacher.DisplayName", selectedValue);
        }

        public static SelectList GetSelectableSubjectGroups(this IEnumerable<SubjectGroup> subjectGroups, int? selectedValue = null)
        {
            return new SelectList(subjectGroups, "Id", "Subject.DisplayName", selectedValue);
        }
    }
}