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
    
    public partial class vm_GetAllClassInfoAlongWithSubjectsWithTeacher
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int MaskingSubjectId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Shift { get; set; }
        public long TeacherId { get; set; }
    }
}
