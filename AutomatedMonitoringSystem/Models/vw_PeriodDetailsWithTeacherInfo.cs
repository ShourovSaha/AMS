//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutomatedMonitoringSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_PeriodDetailsWithTeacherInfo
    {
        public int PeriodId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Day { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string Shift { get; set; }
        public int SubjectId { get; set; }
        public int MaskingSubjectId { get; set; }
        public long UId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
