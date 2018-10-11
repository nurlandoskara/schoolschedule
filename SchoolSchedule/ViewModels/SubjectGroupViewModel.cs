using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolSchedule.Models;

namespace SchoolSchedule.ViewModels
{
    public class SubjectGroupViewModel
    {
        public int GroupId { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public IEnumerable<SubjectGroup> SubjectGroups { get; set; }
    }
}