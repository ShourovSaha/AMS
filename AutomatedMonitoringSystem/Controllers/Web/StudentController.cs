﻿using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.NetworkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models.ViewModels;
using System.IO;

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
                    int Roll, int Year,/* string Shift,*/ long ClassId, int SectionId, string Residential,
                    HttpPostedFileBase file)
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
                ClassId = ClassId,
                SectionId = SectionId,
                Residential = Residential
            };

            ImageVM imageVM = new ImageVM()
            {
                Extention = Path.GetExtension(file.FileName),
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                Location = "~/Content/Images/StudentImage/" + file.FileName
            };

            SaveStudentVM saveStudent = new SaveStudentVM()
            {
                StudentVM = studentVM,
                ImageVM = imageVM
            };

            try
            {
                if (file != null)
                {//keep image file in a folder
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Images/StudentImage/")
                                                          + file.FileName);
                }

                var res = _apiRequest.HttpPostRequest(saveStudent, "api/Student/AddStudent");
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



        //private void SaveImage(HttpPostedFileBase file, long imageId)
        //{
        //    try
        //    {
        //        string fileNameWithOutExtention = Path.GetFileNameWithoutExtension(file.FileName);
        //        string fileExtention = Path.GetExtension(file.FileName);
        //        string imgPath = "~/Content/Images/StudentImage/" + file.FileName;

        //        Image imageObj = new Image()
        //        {
        //            Id = imageId,//Student Id
        //            Location = imgPath,
        //            Extention = fileExtention,
        //            Name = fileNameWithOutExtention
        //        };

        //        _dbContext.Images.Add(imageObj);
        //        _dbContext.SaveChanges();

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msgAlert"] = "N";
        //        TempData["msgAlertDetails"] = ex.Message.ToString();
        //    }
        //}




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

