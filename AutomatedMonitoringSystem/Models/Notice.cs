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
    
    public partial class Notice
    {
        public long Id { get; set; }
        public long PostedBy { get; set; }
        public int MaskingId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> PostedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> PostedForTime { get; set; }
    }
}