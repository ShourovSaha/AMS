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
    
    public partial class GetExamInfoForStudent_SP_Result
    {
        public Nullable<System.DateTime> Date { get; set; }
        public decimal ObtainMark { get; set; }
        public decimal MarksOutOf { get; set; }
        public long ExamTypeId { get; set; }
        public int MaskingId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public long TeacherId { get; set; }
        public long StudentId { get; set; }
        public string Name { get; set; }
        public int Roll { get; set; }
        public string Contact1 { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string Shift { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public string TeacherEmail { get; set; }
    }
}
