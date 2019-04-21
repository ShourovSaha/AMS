using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutomatedMonitoringSystem.Controllers.API
{
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public ClassController()
        {
            _dbContext = new AMSDBEntities();
        }

        // POST: api/Class/ClassList
        [HttpPost]
        [Route("ClassList")]
        public ResponseResult GetClassList()
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var classList = _dbContext.Classes.ToList();
                //var classList = _dbContext.Classes.Select(a => new
                //{
                //    a.ClassId,
                //    a.ClassName,
                //    a.Shift
                //});

                if (classList != null)
                {
                    responseResult.Content = classList;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data found!";
                }
                else
                {
                    responseResult.Content = null;
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
    }
}
