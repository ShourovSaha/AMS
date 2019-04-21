using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class PeriodVM
    {
        public int PeriodId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string PeriodTime
        {
            get
            {
                return this.StartTime + " - " + this.EndTime;
            }
        }
    }
}