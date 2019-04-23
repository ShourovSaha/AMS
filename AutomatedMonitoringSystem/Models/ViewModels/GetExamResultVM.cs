using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class GetExamResultVM
    {
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        //public string Shift { get; set; }
        public int ExamTypeId { get; set; }
        public int Year { get; set; }
    }

    public class GetStudemtExamResultVM
    {
        public int Roll { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        //public string Shift { get; set; }
        public int ExamTypeId { get; set; }
        public int Year { get; set; }
    }
}