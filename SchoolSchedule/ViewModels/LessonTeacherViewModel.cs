using SchoolSchedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolSchedule.ViewModels
{
    public class LessonTeacherViewModel
    {
        public int? TeacherId { get; set; }
        [Display(Name = "Select teacher")]
        public SelectList Teachers { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}