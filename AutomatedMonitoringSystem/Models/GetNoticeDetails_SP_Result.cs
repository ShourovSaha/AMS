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
    
    public partial class GetNoticeDetails_SP_Result
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public long PostedBy { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public Nullable<System.DateTime> PostedForTime { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ClassName { get; set; }
        public string Shift { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public int MaskingSubjectId { get; set; }
        public long TeacherId { get; set; }
        public long UId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
