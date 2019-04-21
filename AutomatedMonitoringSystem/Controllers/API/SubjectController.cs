using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutomatedMonitoringSystem.Controllers.API
{
    [RoutePrefix("api/Subject")]
    public class SubjectController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public SubjectController()
        {
            _dbContext = new AMSDBEntities();
        }

        // POST: api/Subject/SubjectList
        [HttpPost]
        [Route("SubjectList")]
        public ResponseResult GetSubjectListByClassSection(SubjectByClassSectionVM subjectByClassSectionVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var subjectList = _dbContext.Subjects
                                            .Where(a => a.ClassId == subjectByClassSectionVM.ClassId && 
                                                        a.SectionId == subjectByClassSectionVM.SectionId)
                                            .Select(a => new
                                            {
                                                a.SubjectId,
                                                a.SubjectName
                                            })
                                            .Distinct();

                if (subjectList != null)
                {
                    responseResult.Content = subjectList;
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
