using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.NetworkModels;
using AutomatedMonitoringSystem.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedMonitoringSystem.Controllers.Web
{
    public class AttendanceController : BaseController
    {
        private readonly AMSDBEntities _dbContext = null;
        private ApiRequest _apiRequest = null;

        public AttendanceController()
        {
            _dbContext = new AMSDBEntities();
            _apiRequest = new ApiRequest();
        }

        // GET: Attendance
        public ActionResult Index()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            return View();
        }


        public JsonResult StudentAttendanceClassSectionWise(StudentAttendanceByClassVM studentAttendanceByClassVM)
        {
            ResponseResult responseResult = new ResponseResult();
            List<GetAttedanceByClass_SP_Result> attendanceList = new List<GetAttedanceByClass_SP_Result>();

            try
            {
                var res = _apiRequest.HttpPostRequest(studentAttendanceByClassVM, "api/Attendance/StudentAttendanceByClassSection");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    attendanceList = JsonConvert.DeserializeObject<List<GetAttedanceByClass_SP_Result>>(responseResult.Content.ToString());
                }
                else
                {
                    TempData["msgAlert"] = "Y";
                    TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(attendanceList, JsonRequestBehavior.AllowGet);
        }



        public JsonResult StudentAttendanceClassSectionRollWise(StudentAttendanceByRollVM studentAttendanceByRollVM)
        {
            ResponseResult responseResult = new ResponseResult();
            List<GetAttedanceByClass_SP_Result> attendanceList = new List<GetAttedanceByClass_SP_Result>();
            try
            {
                var res = _apiRequest.HttpPostRequest(studentAttendanceByRollVM, "api/Attendance/StudentAttendanceByRoll");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    attendanceList = JsonConvert.DeserializeObject<List<GetAttedanceByClass_SP_Result>>(responseResult.Content.ToString());
                }
                else
                {
                    TempData["msgAlert"] = "Y";
                    TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(attendanceList, JsonRequestBehavior.AllowGet);
        }
    }
}