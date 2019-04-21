using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedMonitoringSystem.Models.ViewModels
{
    public class UserLogInRequestVM
    {
        public long Phone { get; set; }
        public string Password { get; set; }
    }
}