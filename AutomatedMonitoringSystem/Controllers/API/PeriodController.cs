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
    [RoutePrefix("api/Period")]
    public class PeriodController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public PeriodController()
        {
            _dbContext = new AMSDBEntities();
        }

        // POST: api/Period/RoutineByClassSectionShift
        [HttpPost]
        [Route("RoutineByClassSectionShift")]
        public ResponseResult GetRoutineByClassSectionShift(WeeklyRoutinebyClassSectionShiftDayVM weeklyRoutinebyClassSectionShiftDayVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var periodList = _dbContext.GetPeriodsByClassSectionShift_SP(weeklyRoutinebyClassSectionShiftDayVM.ClassId,
                                                        weeklyRoutinebyClassSectionShiftDayVM.SectionId,
                                                        weeklyRoutinebyClassSectionShiftDayVM.Shift);
                if (periodList != null)
                {
                    responseResult.Content = periodList;
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


        // POST: api/Period/PeriodsByWeekDay
        [HttpPost]
        [Route("PeriodsByWeekDay")]
        public ResponseResult GetPeriodsByWeekDay(string day)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var periods = _dbContext.RelationPeriodDayTimes.Where(a => a.Day == day)
                                                                .Select(a => new
                                                                {
                                                                    a.PeriodId,
                                                                    a.StartTime,
                                                                    a.EndTime
                                                                });
                if (periods != null)
                {
                    responseResult.Content = periods;
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



        // POST: api/Period/AssignTeacherSubjectWithPeriodOrtUpdate
        [HttpPost]
        [Route("AssignTeacherSubjectWithPeriodOrtUpdate")]
        public ResponseResult AssignTeacherSubjectWithPeriodOrtUpdate(AssignTeachersSubjectWithPeriodVM assignTeachersSubjectWithPeriodVM)
        {
            ResponseResult responseResult = new ResponseResult();
            System.Data.Entity.Core.Objects.ObjectParameter MSG_Code =
               new System.Data.Entity.Core.Objects.ObjectParameter("MSG_Code", typeof(string));
            System.Data.Entity.Core.Objects.ObjectParameter MSG =
                new System.Data.Entity.Core.Objects.ObjectParameter("MSG", typeof(string));

            try
            {
                Period periodObj = new Period()
                {
                    PeriodId = assignTeachersSubjectWithPeriodVM.PeriodId,
                    MaskingSubjectId = GetSubjectMaskingIdBySubjactid(assignTeachersSubjectWithPeriodVM.SubjectId)
                };

                _dbContext.AddOrUpdateRoutineAssignment_SP(periodObj.MaskingSubjectId, 
                                                           periodObj.PeriodId,
                                                            MSG_Code, MSG);
                responseResult.MessageCode = MSG_Code.Value.ToString();
                responseResult.SystemMessage = MSG.Value.ToString();
                responseResult.Content = null;
            }
            catch (Exception)
            {
                responseResult.MessageCode = MSG_Code.ToString();
                responseResult.SystemMessage = MSG.Value.ToString();
                responseResult.Content = null;
            }
            return responseResult;
        }


        public int GetSubjectMaskingIdBySubjactid(int subjectId)
        {
            return _dbContext.Subjects
                            .Where(a => a.SubjectId == subjectId)
                            .Select(a => a.MaskingSubjectId)
                            .SingleOrDefault();
        }


        public bool IsPeriodSubjectExist(int periodId, int subjectMuskingId)
        {
            int count;
            try
            {
                count = _dbContext.Periods
                                   .Where(a => a.PeriodId == periodId && a.MaskingSubjectId == subjectMuskingId)
                                   .Count();

                if (count > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

    }
}
