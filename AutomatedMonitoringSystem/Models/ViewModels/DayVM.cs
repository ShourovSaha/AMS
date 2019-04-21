using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class DayVM
    {
        public string Day { get; set; }
        public DayVM(string day)
        {
            this.Day = day;
        }
    }
}