using SchoolSchedule.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolSchedule.ViewModels
{
    public class SubjectGroupViewModel
    {
        public int? GroupId { get; set; }
        public SelectList Groups { get; set; }
        public IEnumerable<SubjectGroup> SubjectGroups { get; set; }
    }
}