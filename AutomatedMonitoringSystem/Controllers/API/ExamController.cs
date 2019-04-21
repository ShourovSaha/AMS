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
    [RoutePrefix("api/Exam")]
    public class ExamController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public ExamController()
        {
            _dbContext = new AMSDBEntities();

        }


        // POST: api/Exam/ExamInfoSubjectWise
        [HttpPost]
        [Route("ExamInfoSubjectWise")]
        public ResponseResult GetExamInfoSubjectWise(GetExamResultVM model)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var examResultList = _dbContext.GetExamInfoSubjectWise_SP(model.SubjectId, model.ClassId, model.SectionId,
                                                                    model.Shift, model.ExamTypeId, model.Year);

                if (examResultList != null)
                {
                    responseResult.Content = examResultList;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data found.";
                }
                else
                {
                    responseResult.Content = null;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Data not found.";
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


        // POST: api/Exam/StudentExamResult
        [HttpPost]
        [Route("StudentExamResult")]
        public ResponseResult GetStudentExamResult(GetStudemtExamResultVM model)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var examResultList = _dbContext.GetExamInfoForStudent_SP(model.ClassId, model.SectionId,
                                                                    model.Shift, model.ExamTypeId, model.Year, model.Roll);

                if (examResultList != null)
                {
                    responseResult.Content = examResultList;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data found.";
                }
                else
                {
                    responseResult.Content = null;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Data not found.";
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
