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
    
    public partial class Payment
    {
        public long StudentId { get; set; }
        public Nullable<int> Year { get; set; }
        public string Month { get; set; }
        public string PaidStatus { get; set; }
        public Nullable<System.DateTime> SystemEntryDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
