using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class AssignGuardianWithStudentVM
    {
        public long UserId { get; set; }
        public long StudentId { get; set; }
    }
}