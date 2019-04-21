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
    [RoutePrefix("api/Section")]
    public class SectionController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public SectionController()
        {
            _dbContext = new AMSDBEntities();
        }

        // POST: api/Section/SectionList
        [HttpPost]
        [Route("SectionList")]
        public ResponseResult GetSectionList()
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var sectionList = _dbContext.Sections.Select(a => new
                {
                    a.SectionId,
                    a.SectionName
                });

                if (sectionList != null)
                {
                    responseResult.Content = sectionList;
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
