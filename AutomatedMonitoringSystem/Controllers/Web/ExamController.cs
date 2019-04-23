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
    public class ExamController : BaseController
    {
        private readonly AMSDBEntities _dbContext = null;
        private ApiRequest _apiRequest = null;

        public ExamController()
        {
            _dbContext = new AMSDBEntities();
            _apiRequest = new ApiRequest();
        }

        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetExamResultByClassSectionShift()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            ViewData["YearList"] = new SelectList(YearList(), "Year", "Year");
            return View(new List<GetExamInfoSubjectWise_SP_Result>());
        }

        [HttpPost]
        public ActionResult GetExamResultByClassSectionShift(int subjectId, int classId, int sectionId, /*string shift,*/ int examTypeId, int year)
        {
            ResponseResult responseResult = new ResponseResult();
            List<GetExamInfoSubjectWise_SP_Result> resultList = new List<GetExamInfoSubjectWise_SP_Result>();
            GetExamResultVM data = new GetExamResultVM()
            {
                SubjectId = subjectId,
                ClassId = classId,
                SectionId = sectionId,
                ExamTypeId = examTypeId,
                //Shift = shift,
                Year = year
            };
            try
            {
                var res = _apiRequest.HttpPostRequest(data, "api/Exam/ExamInfoSubjectWise");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    resultList = JsonConvert.DeserializeObject<List<GetExamInfoSubjectWise_SP_Result>>(responseResult.Content.ToString());
                }
                //else if (responseResult.MessageCode == "N")
                //{
                //    //teachers = JsonConvert.DeserializeObject<bool>(responseResult.Content.ToString());
                //}

            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            ViewData["YearList"] = new SelectList(YearList(), "Year", "Year");
            return View(resultList);
        }



        public ActionResult GetStudentExamResult()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            ViewData["YearList"] = new SelectList(YearList(), "Year", "Year");
            return View(new List<GetExamInfoForStudent_SP_Result>());
        }


        [HttpPost]
        public ActionResult GetStudentExamResult(int roll, int classId, int sectionId, int examTypeId, int year)
        {
            ResponseResult responseResult = new ResponseResult();
            List<GetExamInfoForStudent_SP_Result> resultList = new List<GetExamInfoForStudent_SP_Result>();
            GetStudemtExamResultVM data = new GetStudemtExamResultVM()
            {
                Roll = roll,
                ClassId = classId,
                SectionId = sectionId,
                ExamTypeId = examTypeId,
                Year = year
            };
            try
            {
                var res = _apiRequest.HttpPostRequest(data, "api/Exam/StudentExamResult");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    resultList = JsonConvert.DeserializeObject<List<GetExamInfoForStudent_SP_Result>>(responseResult.Content.ToString());
                }
                //else if (responseResult.MessageCode == "N")
                //{
                //    //teachers = JsonConvert.DeserializeObject<bool>(responseResult.Content.ToString());
                //}

            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            ViewData["YearList"] = new SelectList(YearList(), "Year", "Year");
            return View(resultList);
        }
    }
}