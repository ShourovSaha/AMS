using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.CommonClass
{
    public class ResponseResult
    {
        public string MessageCode { get; set; }
        public string SystemMessage { get; set; }
        public object Content { get; set; }
    }
}