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
                var periodList = _dbContext.GetRoutineByClassSectionShift_SP(weeklyRoutinebyClassSectionShiftDayVM.ClassId,
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
        public ResponseResult AssignTeacherSubjectWithPeriodOrtUpdate(AssignTeachersSubjectWithPeriodVM model)
        {
            ResponseResult responseResult = new ResponseResult();
            //System.Data.Entity.Core.Objects.ObjectParameter MSG_Code =
            //   new System.Data.Entity.Core.Objects.ObjectParameter("MSG_Code", typeof(string));
            //System.Data.Entity.Core.Objects.ObjectParameter MSG =
            //    new System.Data.Entity.Core.Objects.ObjectParameter("MSG", typeof(string));

            try
            {
                int smId = GetSubjectMuskingIdForPeriodAssignment(model.SubjectId,
                                                                   model.UserPhone);

                if (IsPeriodAlreadyAssigned(smId, model.PeriodId) == false)
                {
                    Period periodObj = new Period()
                    {
                        PeriodId = model.PeriodId,
                        MaskingSubjectId = smId
                    };

                    _dbContext.Periods.Add(periodObj);
                    _dbContext.SaveChanges();
                    //_dbContext.AddOrUpdateRoutineAssignment_SP(periodObj.MaskingSubjectId,
                    //                                           periodObj.PeriodId,
                    //                                            MSG_Code, MSG);

                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Assigned sussfully!";
                    responseResult.Content = null;
                }
                else
                {
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Already Assigned!";
                    responseResult.Content = null;
                }               
            }
            catch (Exception ex)
            {
                responseResult.MessageCode = MessageCode.N.ToString(); ;
                responseResult.SystemMessage = ex.Message;
                responseResult.Content = null;
            }
            return responseResult;
        }


        public bool IsPeriodAlreadyAssigned(int subjectMuskingId, int periodId)
        {
            try
            {
                int pId =_dbContext.Periods.Where(a => a.MaskingSubjectId == subjectMuskingId &&
                                                a.PeriodId == periodId)
                                                .Select(a => a.PeriodId)
                                                .SingleOrDefault();
                if (pId > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        } 



        public int GetSubjectMuskingIdForPeriodAssignment(int subjectId, long teacherId)
        {
            int msId = -1;
            try
            {
                List<int> subjectMuskingList = GetSubjectMuskingIds(subjectId);
                msId = _dbContext.TeacherSubjects
                                    .Where(a => subjectMuskingList.Any(b => b == a.MaskingSubjectId) &&
                                                a.TeacherId == teacherId)
                                    .Select(a => a.MaskingSubjectId)
                                    .SingleOrDefault();
                return msId > 0 ? msId : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            //return msId;
        }


        public List<int> GetSubjectMuskingIds(int subjectId)
        {
            ResponseResult responseResult = new ResponseResult();
            List<int> subjectMuskingIds = new List<int>();
            //List<SubjectMuskingIdVM> subjectMuskingIdList = new List<SubjectMuskingIdVM>();

            try
            {
                subjectMuskingIds = _dbContext.Subjects
                                    .Where(a => a.SubjectId == subjectId)
                                    .Select(a => a.MaskingSubjectId)
                                    .Distinct()
                                    .ToList();

                //foreach (var i in subjectMuskingIds)
                //{
                //    SubjectMuskingIdVM data = new SubjectMuskingIdVM()
                //    {
                //        SubjectMuskingId = i
                //    };
                //    subjectMuskingIdList.Add(data);
                //}

            }
            catch (Exception ex)
            {
                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.Y.ToString();
                responseResult.SystemMessage = ex.Message;
            }
            return subjectMuskingIds;
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

    public class SubjectMuskingIdVM
    {
        public int SubjectMuskingId { get; set; }
    }
}
