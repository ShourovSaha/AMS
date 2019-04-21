using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class UserInfoVM
    {
        public long StudentId { get; set; }
        //-------------
        public long UId { get; set; }//User Phone number
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }
        public string Status { get; set; }
    }
}