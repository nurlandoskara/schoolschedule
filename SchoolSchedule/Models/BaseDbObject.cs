using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolSchedule.Models
{
    public class BaseDbObject
    {
        [Required]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}