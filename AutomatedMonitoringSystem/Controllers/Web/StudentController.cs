using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models.ViewModels.StudentVM;
using AutomatedMonitoringSystem.Models.ViewModels;

namespace AutomatedMonitoringSystem.Controllers.Web
{
    public class StudentController : BaseController
    {
        private readonly AMSDBEntities _dbContext = null;
        private ApiRequest _apiRequest = null;

        public StudentController()
        {
            _dbContext = new AMSDBEntities();
            _apiRequest = new ApiRequest();
        }



        //get: student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddStudent()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(string Name, DateTime Birthday, string FatherName, string MotherName,
                    string PresentAddress, string PermanentAddress, string Contact1, string Contact2,
                    int Roll, int Year, string Shift, long ClassId, int SectionId, string Residential)
        {
            ResponseResult responseResult = new ResponseResult();
            StudentVM studentVM = new StudentVM()
            {
                StudentId = 0,
                Name = Name,
                Birthday = Birthday,
                FatherName = FatherName,
                MotherName = MotherName,
                PermanentAddress = PermanentAddress,
                PresentAddress = PresentAddress,
                Contact1 = Contact1,
                Contact2 = Contact2,
                //=======
                Roll = Roll,
                Year = Year,
                Shift = Shift,
                ClassId = ClassId,
                SectionId = SectionId,
                Residential = Residential
            };

            try
            {
                var res = _apiRequest.HttpPostRequest(studentVM, "api/Student/AddStudent");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    TempData["msgAlert"] = "Y";
                    TempData["msgAlertDetails"] = responseResult.SystemMessage;
                    
                }
                else
                {
                    TempData["msgAlert"] = "N";
                    TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }

            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }

            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            return RedirectToAction("AddStudent");
        }



        public JsonResult GetStudentsByClassSection(string ClassId, string SectionId)
        {
            ResponseResult responseResult = new ResponseResult();
            List<GetStudentInfo_SP_Result> studentList = new List<GetStudentInfo_SP_Result>();
            SubjectByClassSectionVM sObj = new SubjectByClassSectionVM()
            {
                ClassId = Convert.ToInt32(ClassId),
                SectionId = Convert.ToInt32(SectionId)
            };

            try
            {
                var res = _apiRequest.HttpPostRequest(sObj, "api/Student/StudentsInfoListByClassSection");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    studentList = JsonConvert.DeserializeObject<List<GetStudentInfo_SP_Result>>(responseResult.Content.ToString());
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
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }

    }
}

