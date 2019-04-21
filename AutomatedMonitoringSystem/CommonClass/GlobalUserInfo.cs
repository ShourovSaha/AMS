using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.CommonClass
{
    public class GlobalUserInfo
    {
        public long UserId { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }

        public string GetFullName()
        {
            return FristName + " " + LastName;
        }
    }
}