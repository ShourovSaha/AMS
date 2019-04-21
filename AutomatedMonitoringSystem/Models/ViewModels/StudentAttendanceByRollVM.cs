using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class StudentAttendanceByRollVM : StudentAttendanceByClassVM
    {
        public int Roll { get; set; }
    }
}