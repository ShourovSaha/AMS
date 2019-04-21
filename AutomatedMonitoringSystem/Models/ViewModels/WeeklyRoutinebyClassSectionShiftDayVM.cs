using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class WeeklyRoutinebyClassSectionShiftDayVM
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string Shift { get; set; }
        //public string Day { get; set; }
    }
}