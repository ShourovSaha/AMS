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
    [RoutePrefix("api/Attendance")]
    public class AttendanceController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public AttendanceController()
        {
            _dbContext = new AMSDBEntities();
        }



        // POST: api/Attendance/StudentAttendanceByClassSection
        [HttpPost]
        [Route("StudentAttendanceByClassSection")]
        public ResponseResult GetStudentAttendanceByClassSection(StudentAttendanceByClassVM studentAttendanceByClassVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var stuendentList = _dbContext.GetAttedanceByClass_SP(studentAttendanceByClassVM.Date,
                                                        studentAttendanceByClassVM.ClassId,
                                                        studentAttendanceByClassVM.SectionId,
                                                        studentAttendanceByClassVM.Shift);
                if (stuendentList != null)
                {
                    responseResult.Content = stuendentList;
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


        // POST: api/Attendance/StudentAttendanceByRoll
        [HttpPost]
        [Route("StudentAttendanceByRoll")]
        public ResponseResult GetStudentAttendanceByStudentRoll(StudentAttendanceByRollVM studentAttendanceByRollVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var stuendenrAttendanceList = _dbContext.GetAttedanceByStudentRoll_SP(
                                                        studentAttendanceByRollVM.Roll,
                                                        studentAttendanceByRollVM.Date,
                                                        studentAttendanceByRollVM.ClassId,
                                                        studentAttendanceByRollVM.SectionId,
                                                        studentAttendanceByRollVM.Shift);
                if (stuendenrAttendanceList != null)
                {
                    responseResult.Content = stuendenrAttendanceList;
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

    public enum MessageCode
    {
        Y, N
    }
}
