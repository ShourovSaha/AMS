﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class TeacherVM
    {
        public long UId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
