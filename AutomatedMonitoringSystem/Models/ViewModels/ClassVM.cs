using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class ClassVM
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Shift { get; set; }
        public string ClassWithShift
        {
            get
            {
                return this.ClassName + "-" + this.Shift;
            }
        }
        

        public string GetClassName()
        {
            return this.ClassName + "-" + this.Shift;
        }
    }
}