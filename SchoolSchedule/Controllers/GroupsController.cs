﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolSchedule.Models;

namespace SchoolSchedule.Controllers
{
    public class GroupsController : BaseController<Group>
    {
        public GroupsController()
        {
            Context = new ModelContainer();
        }
    }
}