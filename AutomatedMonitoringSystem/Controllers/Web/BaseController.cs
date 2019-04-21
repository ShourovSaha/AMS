using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.NetworkModels;
using AutomatedMonitoringSystem.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace AutomatedMonitoringSystem.Controllers.Web
{
    public class BaseController : Controller
    {
        private readonly AMSDBEntities _dbContext = null;
        private ApiRequest _apiRequest = null;

        public BaseController()
        {
            _dbContext = new AMSDBEntities();
            _apiRequest = new ApiRequest();
        }


        //public IEnumerable<object> GetSubjectListByClassSectionForDropDown(int classId, int sectionId)
        //{
        //    var subjectist = GetSubjectListByClassSection(classId, sectionId).Select(a => new
        //    {
        //        a.SubjectId,
        //        a.SubjectName
        //    });

        //    return subjectist;

        //}

        public JsonResult GetSubjectListByClassSection(int ClassId, int SectionId)
        {
            ResponseResult responseResult = new ResponseResult();
            List<SubjectVM> subjectList = new List<SubjectVM>();

            SubjectByClassSectionVM subjectVM = new SubjectByClassSectionVM()
            {
                ClassId = ClassId,
                SectionId = SectionId
            };

            try
            {
                var res = _apiRequest.HttpPostRequest(subjectVM, "api/Subject/SubjectList");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    subjectList = JsonConvert.DeserializeObject<List<SubjectVM>>(responseResult.Content.ToString());

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(subjectList, JsonRequestBehavior.AllowGet);
        }

        //public List<SubjectVM> GetSubjectListByClassSection(int classId, int sectionId)
        //{
        //    ResponseResult responseResult = new ResponseResult();
        //    List<SubjectVM> subjectList = new List<SubjectVM>();

        //    SubjectByClassSectionVM subjectVM = new SubjectByClassSectionVM()
        //    {
        //        ClassId = classId,
        //        SectionId = sectionId
        //    };

        //    try
        //    {
        //        var res = _apiRequest.HttpPostRequest(subjectVM, "api/Subject/SubjectList");
        //        string apiResponse = res.ToString();
        //        responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

        //        if (responseResult.MessageCode == "Y")
        //        {
        //            subjectList = JsonConvert.DeserializeObject<List<SubjectVM>>(responseResult.Content.ToString());

        //            //TempData["msgAlert"] = "Y";
        //            //TempData["msgAlertDetails"] = responseResult.SystemMessage;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msgAlert"] = "N";
        //        TempData["msgAlertDetails"] = ex.Message.ToString();
        //    }
        //    return subjectList;
        //}


        public IEnumerable<object> GetClassListForDropDown()
        {
            var classlist = GetClassList().Select(a => new
            {
                a.ClassId,
                a.ClassWithShift
            });
            return classlist;
        }


        public List<ClassVM> GetClassList()
        {
            ResponseResult responseResult = new ResponseResult();
            List<ClassVM> classList = new List<ClassVM>();
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/Class/ClassList");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    classList = JsonConvert.DeserializeObject<List<ClassVM>>(responseResult.Content.ToString());

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return classList;
        }



        public JsonResult GetPeriodsByWeekDay(string Day)
        {
            ResponseResult responseResult = new ResponseResult();
            List<PeriodVM> periods = new List<PeriodVM>();
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/Period/PeriodsByWeekDay?Day=" + Day);
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    periods = JsonConvert.DeserializeObject<List<PeriodVM>>(responseResult.Content.ToString());

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(periods, JsonRequestBehavior.AllowGet);
        }


        public IEnumerable<object> GetSectionListForDropDown()
        {
            var sectionlist = GetSectionList().Select(a => new
            {
                a.SectionId,
                a.SectionName
            });
            return sectionlist;
        }


        public List<SectionVM> GetSectionList()
        {
            ResponseResult responseResult = new ResponseResult();
            List<SectionVM> sectionList = new List<SectionVM>();
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/Section/SectionList");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    sectionList = JsonConvert.DeserializeObject<List<SectionVM>>(responseResult.Content.ToString());

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return sectionList;
        }


        public DateTime GetLocalTime()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, BdZone);
            return localDateTime;
        }


        public List<DayVM> GetWeekDays()
        {
            return new List<DayVM>()
            {
                new DayVM("Saturday"),
                new DayVM("Sunday"),
                new DayVM("Monday"),
                new DayVM("Tuesday"),
                new DayVM("Wednesday"),
                new DayVM("Thrusday"),
                new DayVM("Friday")
            };
        }



        public List<YearVM> YearList()
        {
            List<YearVM> yearList = new List<YearVM>();
            for (int i = GetLocalTime().Year; i >= 2000; i-- )
            {
                YearVM year = new YearVM()
                {
                    Year = i
                };
                yearList.Add(year);
            }
            return yearList;
        }

        public enum UserType
        {
            Admin,
            Teacher,
            Guardian
        }
        public enum MessageCode
        {
            Y, N
        }
    }
}
