using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.NetworkModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedMonitoringSystem.Models.ViewModels;

namespace AutomatedMonitoringSystem.Controllers.Web
{
    public class UserController : BaseController
    {

        private readonly AMSDBEntities _dbContext = null;
        private ApiRequest _apiRequest = null;

        public UserController()
        {
            _dbContext = new AMSDBEntities();
            _apiRequest = new ApiRequest();
        }


        // GET: User
        public ActionResult Index(string Name)
        {
            ResponseResult responseResult = new ResponseResult();
            List<vm_GetAllStudentInfo> studentInfoList = new List<vm_GetAllStudentInfo>();
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/User/GetStudentsInfoListByName?name=" + Name);
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    studentInfoList = JsonConvert.DeserializeObject<List<vm_GetAllStudentInfo>>(responseResult.Content.ToString());

                    TempData["msgAlert"] = "Y";
                    TempData["msgAlertDetails"] = responseResult.SystemMessage;

                }
                //else
                //{
                //    TempData["msgAlert"] = "N";
                //    TempData["msgAlertDetails"] = responseResult.SystemMessage;
                //}

            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return View(studentInfoList);
        }


        public ActionResult GetTeachers()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            ViewData["Days"] = new SelectList(GetWeekDays(), "Day", "Day");
            return View();
        }


        public JsonResult PeriodAssignmentAjax(string subjectId, string periodId, string userPhone)
        {
            ResponseResult responseResult = new ResponseResult();
            AssignTeachersSubjectWithPeriodVM assignTeachersVM = new AssignTeachersSubjectWithPeriodVM()
            {
                PeriodId = Convert.ToInt32(periodId),
                SubjectId = Convert.ToInt32(subjectId),
                UserPhone = Convert.ToInt64(userPhone)
            };

            try
            {
                var res = _apiRequest.HttpPostRequest(assignTeachersVM, "api/Period/AssignTeacherSubjectWithPeriodOrtUpdate");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                //if (responseResult.MessageCode == "Y")
                //{
                //    return responseResult.MessageCode;
                //}
                //else(responseResult.MessageCode == "N")
                //{
                //}

            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(responseResult.MessageCode, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetTeachersBySubject(string subjectId)
        {
            ResponseResult responseResult = new ResponseResult();
            List<TeacherVM> teacherList = new List<TeacherVM>();
            List<object> teachers = new List<object>();

            int sId = Convert.ToInt32(subjectId);
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/User/TeachersBySubject?subjectId=" + sId);
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    teachers = JsonConvert.DeserializeObject<List<object>>(responseResult.Content.ToString()).ToList();

                    for (int i = 0; i < teachers.Count; i++)
                    {
                        var teacherObj = JsonConvert.DeserializeObject<List<TeacherVM>>(teachers[i].ToString());
                        TeacherVM teacherVM = new TeacherVM()
                        {
                            UId = teacherObj[0].UId,
                            FirstName = teacherObj[0].FirstName,
                            LastName = teacherObj[0].LastName,
                            Gender = teacherObj[0].Gender,
                            Email = teacherObj[0].Email
                        };
                        teacherList.Add(teacherVM);
                    }
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
            return Json(teacherList, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetTeachersAjax()//All teacher 
        {
            ResponseResult responseResult = new ResponseResult();
            List<TeacherVM> teachers = new List<TeacherVM>();

            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/User/Teachers");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    teachers = JsonConvert.DeserializeObject<List<TeacherVM>>(responseResult.Content.ToString());
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
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AssignTeacherWithSubject()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");

            return View();
        }


        [HttpPost]
        public ActionResult AssignTeacherWithSubject(int ClassId, int SectionId, int SubjectId, long UserPhone)
        {
            try
            {
                if (DoesUserExists(UserPhone) == true)
                {
                    if (IsUserTeacherOrNot(UserPhone) == true)
                    {
                        ResponseResult responseResult = new ResponseResult();
                        TeacherSubjectVM teacherSubjectVM = new TeacherSubjectVM()
                        {
                            ClassId = ClassId,
                            SectionId = SectionId,
                            SubjectId = SubjectId,
                            UserId = UserPhone
                        };

                        var res = _apiRequest.HttpPostRequest(teacherSubjectVM, "api/User/AssignTeacherWithSubject");
                        string apiResponse = res.ToString();
                        responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                        if (responseResult.MessageCode == "Y")
                        {
                            TempData["msgAlert"] = responseResult.MessageCode;
                            TempData["msgAlertDetails"] = responseResult.SystemMessage;
                        }
                        else
                        {
                            TempData["msgAlert"] = responseResult.MessageCode;
                            TempData["msgAlertDetails"] = responseResult.SystemMessage;
                        }
                    }
                    else
                    {
                        TempData["msgAlert"] = "N";
                        TempData["msgAlertDetails"] = "This user is not a teacher!";
                        return View();
                    }
                }
                else
                {
                    TempData["msgAlert"] = "N";
                    TempData["msgAlertDetails"] = "User does not exists!";
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }

            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            return View();
        }




        public bool DoesUserExists(long UserPhone)
        {
            ResponseResult responseResult = new ResponseResult();
            bool result = false;
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/User/DoesUserExist?userPhoneNumber=" + UserPhone);
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    result = true;
                    //result = JsonConvert.DeserializeObject<bool>(responseResult.Content.ToString());
                }
                else if (responseResult.MessageCode == "N")
                {
                    result = true;
                    //result = JsonConvert.DeserializeObject<bool>(responseResult.Content.ToString());
                }

            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return result;
        }


        public bool IsUserTeacherOrNot(long UserPhone)
        {
            ResponseResult responseResult = new ResponseResult();
            int result = 0;
            bool flag = false;
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/User/UserTypeByPhoneNumber?userPhoneNumber=" + UserPhone);
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    result = JsonConvert.DeserializeObject<int>(responseResult.Content.ToString());

                    if (result == 2)
                    {
                        flag = true;
                    }
                    else
                    {
                        //TempData["msgAlert"] = "N";
                        //TempData["msgAlertDetails"] = "This user is not a teacher!";
                        flag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return flag;
        }


        public ActionResult AssignUserWithStudent()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            return View(new List<vm_GetAllStudentInfo2>());
        }


        [HttpPost]
        public ActionResult AssignUserWithStudent1(int ClassId, int SectionId)
        {
            ResponseResult responseResult = new ResponseResult();
            List<vm_GetAllStudentInfo2> studentList = new List<vm_GetAllStudentInfo2>();
            SubjectByClassSectionVM subjectByClassSectionVM = new SubjectByClassSectionVM()
            {
                ClassId = ClassId,
                SectionId = SectionId
            };

            try
            {
                var res = _apiRequest.HttpPostRequest(subjectByClassSectionVM, "api/Student/StudentsInfoListByClassSection");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    studentList = JsonConvert.DeserializeObject<List<vm_GetAllStudentInfo2>>(responseResult.Content.ToString());

                    if (studentList.Count < 1)
                    {
                        TempData["msgAlert"] = "Y";
                        TempData["msgAlertDetails"] = "No Student Found!";
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");

            return View(studentList);
        }

        [HttpPost]
        public ActionResult AssignUserWithStudent(int ClassId, int SectionId)
        {
            ResponseResult responseResult = new ResponseResult();
            List<vm_GetAllStudentInfo2> studentList = new List<vm_GetAllStudentInfo2>();
            SubjectByClassSectionVM subjectByClassSectionVM = new SubjectByClassSectionVM()
            {
                ClassId = ClassId,
                SectionId = SectionId
            };

            try
            {
                var res = _apiRequest.HttpPostRequest(subjectByClassSectionVM, "api/Student/StudentsInfoListByClassSection");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    studentList = JsonConvert.DeserializeObject<List<vm_GetAllStudentInfo2>>(responseResult.Content.ToString());

                    if (studentList.Count < 1)
                    {
                        TempData["msgAlert"] = "Y";
                        TempData["msgAlertDetails"] = "No Student Found!";
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");

            return View(studentList);
        }

        //[HttpPost]
        //public ActionResult AssignUserWithStudent(long StudentId, string ClassName, string SectionName, int Roll)
        //{
        //    vm_GetAllStudentInfo studentVM = new vm_GetAllStudentInfo()
        //    {
        //        StudentId = StudentId,
        //        ClassName = ClassName,
        //        SectionName = SectionName,
        //        Roll = Roll
        //    };
        //    return View("FinalAssignUserWithStudent", studentVM);
        //}


        //public ActionResult FinalAssignUserWithStudent(long StudentId, long UserPhone)
        //{

        //}



        public JsonResult FinalAssignUserWithStudent(string UserPhone, string StudentId)
        {
            ResponseResult responseResult = new ResponseResult();
            AssignGuardianWithStudentVM assignGuardianWithStudentVM = new AssignGuardianWithStudentVM()
            {
                StudentId = Convert.ToInt64(StudentId),
                UserId = Convert.ToInt64(UserPhone)
            };
            try
            {
                var res = _apiRequest.HttpPostRequest(assignGuardianWithStudentVM, "api/User/AssignGuardianWithStudent");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    TempData["msgAlert"] = responseResult.MessageCode;
                    TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
                else
                {
                    TempData["msgAlert"] = responseResult.MessageCode;
                    TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(responseResult, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetGardiansPhoneNumber(string UserPhone)
        {
            ResponseResult responseResult = new ResponseResult();
            List<long> gardianphoneNumbers = new List<long>();
            try
            {
                long phoneNumber = Convert.ToInt64(UserPhone);
                var res = _apiRequest.HttpPostRequest(null, "api/User/GardiansPhoneNumber" + phoneNumber);
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    gardianphoneNumbers = JsonConvert.DeserializeObject<List<long>>(responseResult.Content.ToString());

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(gardianphoneNumbers, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetAllGardian()
        {
            ResponseResult responseResult = new ResponseResult();
            List<User> gardians = new List<User>();
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/User/AllGurdians");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    gardians = JsonConvert.DeserializeObject<List<User>>(responseResult.Content.ToString());

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return Json(gardians, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(string FirstName, string LastName, long Phone,
                                    DateTime Birthday, string Gender, string Email, string Password,
                                    string RePassword, int UserTypeId)
        {
            if (Password != RePassword)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = "Password is not matched";
                return RedirectToAction("Index");
            }

            ResponseResult responseResult = new ResponseResult();
            UserInfoVM userVMObj = new UserInfoVM()
            {
                UId = Phone,
                FirstName = FirstName,
                LastName = LastName,
                Birthday = Birthday,
                Email = Email,
                Gender = Gender,
                UserTypeId = UserTypeId,
                Status = "Y",
                Password = Password
            };
            try
            {
                var res = _apiRequest.HttpPostRequest(userVMObj, "api/User/AddUser");
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
            return RedirectToAction("AddUser");
        }



    }
}
