using SchoolSchedule.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolSchedule.ViewModels
{
    public class LessonViewModel
    {
        public int? GroupId { get; set; }
        [Display(Name = "Select group")]
        public SelectList Groups { get; set; }
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}