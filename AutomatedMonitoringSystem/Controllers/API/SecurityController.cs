using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.ViewModels;

namespace AutomatedMonitoringSystem.Controllers.API
{
    [RoutePrefix("api/Security")]
    public class SecurityController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public SecurityController()
        {
            _dbContext = new AMSDBEntities();
        }


        // Get: api/Security
        [HttpPost]
        [Route("UserLogin")]
        public ResponseResult GetUserLoginInfo(UserLogInRequestVM userLogInData)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                if (userLogInData == null && userLogInData.Password == null)
                {
                    responseResult.Content = null;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Inputs is null!";

                    return responseResult;
                }

                //var result = _dbContext.sp_GetUserLogInInfo(userLogInValue.Phone, userLogInValue.Password);
                var result = from u in _dbContext.Users
                             join ut in _dbContext.UserTypes
                             on u.UserTypeId equals ut.Id
                             where u.UId.Equals(userLogInData.Phone) &&
                             u.Password.Equals(userLogInData.Password) &&
                             u.Status.Equals("Y")
                             select new
                             {
                                 u.FirstName,
                                 u.LastName,
                                 u.UId,
                                 ut.Title
                             };

                if (result.Any())
                {
                    responseResult.Content = result.ToList()[0];
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data found.";
                }
                else
                {
                    responseResult.Content = result;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Data not found!";
                }
                
            }
            catch (Exception ex)
            {
                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.N.ToString();
                responseResult.SystemMessage = ex.Message;
            }
            return responseResult;
        }


        public enum MessageCode
        {
            Y, N
        }
    }
}
