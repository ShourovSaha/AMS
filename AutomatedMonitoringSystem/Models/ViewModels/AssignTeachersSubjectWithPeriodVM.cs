using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class AssignTeachersSubjectWithPeriodVM
    {
        public int PeriodId { get; set; }
        public int SubjectId { get; set; }
        public long UserPhone { get; set; }
    }
}