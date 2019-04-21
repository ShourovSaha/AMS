using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class UserLogInInfoVM
    {
        public long UId { get; set; }//Phone
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }//usertype

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}