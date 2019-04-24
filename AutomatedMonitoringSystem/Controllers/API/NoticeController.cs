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
    [RoutePrefix("api/Notice")]
    public class NoticeController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public NoticeController()
        {
            _dbContext = new AMSDBEntities();
        }

        // POST: api/Notice/AddNotice
        [HttpPost]
        [Route("AddNotice")]
        public ResponseResult AddNotice(NoticeVM noticeVM)//Add Or Update Notice 
        {
            ResponseResult responseResult = new ResponseResult();
            //System.Data.Entity.Core.Objects.ObjectParameter MSG_Code =
            //    new System.Data.Entity.Core.Objects.ObjectParameter("MSG_Code", typeof(string));
            //System.Data.Entity.Core.Objects.ObjectParameter MSG =
            //    new System.Data.Entity.Core.Objects.ObjectParameter("MSG", typeof(string));
            try
            {
                int subjectMuskingId = GetSubjectMuskingId(noticeVM.SubjectId);
                Notice noticeObj = new Notice()
                {
                    MaskingId = subjectMuskingId,
                    Message = noticeVM.Message,
                    PostedBy = noticeVM.PostedBy,
                    PostedDate = noticeVM.PostedDate,
                    PostedForTime = noticeVM.PostedForTime,
                    Title = noticeVM.Title,
                    UpdatedDate = noticeVM.UpdatedDate
                };
                _dbContext.Notices.Add(noticeObj);
                _dbContext.SaveChanges();

                responseResult.MessageCode = MessageCode.Y.ToString();
                responseResult.SystemMessage = "Posted Successfully.";
                responseResult.Content = null;
            }
            catch (Exception ex)
            {
                responseResult.MessageCode = MessageCode.N.ToString();
                responseResult.SystemMessage = ex.Message;
                responseResult.Content = null;
                //throw ex;
            }
            return responseResult;
        }

        // POST: api/Notice/NoticeList
        [HttpPost]
        [Route("NoticeList")]
        public ResponseResult GetNoticeList()
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var noticeList = _dbContext.GetNoticeDetails_SP();

                if (noticeList != null)
                {
                    responseResult.Content = noticeList;
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

        public int GetSubjectMuskingId(int subjectId)
        {
            return _dbContext.Subjects.Where(a => a.SubjectId == subjectId)
                                .Select(a => a.MaskingSubjectId)
                                .FirstOrDefault();
        }


        // GET: api/Notice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Notice/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Notice
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Notice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Notice/5
        public void Delete(int id)
        {
        }
    }
}
