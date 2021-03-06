﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class NoticeVM
    {
        public long NoticeId { get; set; }
        public long PostedBy { get; set; }
        public int MaskingId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime PostedForTime { get; set; }
    }
}