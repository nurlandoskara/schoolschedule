using SchoolSchedule.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolSchedule.Helpers
{
    public static class SelectListHelper
    {
        public static SelectList GetClassYears(int? selectedValue = null)
        {
            var classYears = new List<int> {1,2,3,4,5,6,7,8,9,10,11};
            return new SelectList(classYears, selectedValue);
            /*
            return classYears.Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int) v).ToString()
            }).ToList();
            */
        }

        public static SelectList GetSelectableList<T>(this IEnumerable<T> list, int? selectedValue = null)
        {
            return new SelectList(list, "Id", "DisplayName", selectedValue);
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