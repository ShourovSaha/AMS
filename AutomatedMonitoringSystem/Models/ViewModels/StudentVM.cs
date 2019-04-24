using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class StudentVM
    {
        public long StudentId { get; set; }
        public string Name { get; set; }
        public System.DateTime Birthday { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        //========
        public int Roll { get; set; }
        public int Year { get; set; }
        //public string Shift { get; set; }
        public long ClassId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string Residential { get; set; }
    }
}