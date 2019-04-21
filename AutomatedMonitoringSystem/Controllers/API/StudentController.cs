using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.ViewModels;
using AutomatedMonitoringSystem.Models.ViewModels.StudentVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AutomatedMonitoringSystem.Controllers.API
{
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {

        private readonly AMSDBEntities _dbContext = null;

        public StudentController()
        {
            _dbContext = new AMSDBEntities();
        }

        // POST: api/Student/AddStudent
        [HttpPost]
        [Route("AddStudent")]
        public ResponseResult AddStudent(StudentVM studentVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                long sId = GenerateStudentId();
                StudentBasic studentBasic = new StudentBasic()
                {
                    StudentId = sId,
                    Name = studentVM.Name,
                    Birthday = studentVM.Birthday,
                    FatherName = studentVM.FatherName,
                    MotherName = studentVM.MotherName,
                    PresentAddress = studentVM.PresentAddress,
                    PermanentAddress = studentVM.PermanentAddress,
                    Contact1 = studentVM.Contact1,
                    Contact2 = studentVM.Contact2
                };

                StudentInfo studentInfo = new StudentInfo()
                {
                    StudentId = sId,
                    Roll = studentVM.Roll,
                    ClassId = studentVM.ClassId,
                    SectionId = studentVM.SectionId,
                    Shift = studentVM.Shift,
                    Year = studentVM.Year,
                    Residential = studentVM.Residential
                };

                _dbContext.StudentBasics.Add(studentBasic);
                _dbContext.StudentInfoes.Add(studentInfo);
                _dbContext.SaveChanges();

                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.Y.ToString();
                responseResult.SystemMessage = "Data Saved Succesfully.";
            }
            catch (Exception ex)
            {
                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.N.ToString();
                responseResult.SystemMessage = ex.Message;
            }
            return responseResult;
        }


        // Post: api/Student/StudentsInfoListByClassSection
        [HttpPost]
        [Route("StudentsInfoListByClassSection")]
        public ResponseResult GetStudentsInfoListByClassSection(SubjectByClassSectionVM subjectByClassSectionVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                responseResult.Content = _dbContext.GetStudentInfo_SP(subjectByClassSectionVM.ClassId, subjectByClassSectionVM.SectionId); 
                responseResult.MessageCode = MessageCode.Y.ToString();
                responseResult.SystemMessage = "Data Found!";
            }
            catch (Exception ex)
            {
                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.Y.ToString();
                responseResult.SystemMessage = ex.Message;
            }
            return responseResult;
        }

        //Generateing StudentId
        private long GenerateStudentId()
        {
            string id = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() 
                            + DateTime.Now.Day.ToString() + (GetTotalNumberOfStudent() + 1);
            return Convert.ToInt64(id);
        }

        public long GetTotalNumberOfStudent()
        {
            return _dbContext.StudentBasics.Select(a => a.StudentId).Count();
        }
        // POST: api/Student/StudentAttendanceByClass
        //[HttpPost]
        //[Route("StudentAttendanceByClass")]
        //public ResponseResult GetStudentAttendanceByClass(StudentAttendanceByClassVM studentAttendanceByClassVM)
        //{
        //    ResponseResult responseResult = new ResponseResult();
        //    try
        //    {                
        //        var stuendentList = _dbContext.GetAttedanceByClass_SP(studentAttendanceByClassVM.Date,
        //                                                studentAttendanceByClassVM.ClassName,
        //                                                studentAttendanceByClassVM.SectionName,
        //                                                studentAttendanceByClassVM.Shift);
        //        if (stuendentList != null)
        //        {
        //            responseResult.Content = stuendentList;
        //            responseResult.MessageCode = MessageCode.Y.ToString();
        //            responseResult.SystemMessage = "Data found!";
        //        }
        //        else
        //        {
        //            responseResult.Content = null;
        //            responseResult.MessageCode = MessageCode.N.ToString();
        //            responseResult.SystemMessage = "Data not found!";
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        responseResult.Content = null;
        //        responseResult.MessageCode = MessageCode.N.ToString();
        //        responseResult.SystemMessage = ex.Message;
        //    }
        //    return responseResult;
        //}


        // POST: api/Student/StudentAttendanceByClass
        //[HttpPost]
        //[Route("StudentAttendanceByRoll")]
        //public ResponseResult GetStudentAttendanceByStudentRoll(StudentAttendanceByRollVM studentAttendanceByRollVM)
        //{
        //    ResponseResult responseResult = new ResponseResult();
        //    try
        //    {
        //        var stuendenrAttendanceList = _dbContext.GetAttedanceByStudentRoll_SP(
        //                                                studentAttendanceByRollVM.Roll,
        //                                                studentAttendanceByRollVM.Date,
        //                                                studentAttendanceByRollVM.ClassName,
        //                                                studentAttendanceByRollVM.SectionName,
        //                                                studentAttendanceByRollVM.Shift);
        //        if (stuendenrAttendanceList != null)
        //        {
        //            responseResult.Content = stuendenrAttendanceList;
        //            responseResult.MessageCode = MessageCode.Y.ToString();
        //            responseResult.SystemMessage = "Data found!";
        //        }
        //        else
        //        {
        //            responseResult.Content = null;
        //            responseResult.MessageCode = MessageCode.N.ToString();
        //            responseResult.SystemMessage = "Data not found!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        responseResult.Content = null;
        //        responseResult.MessageCode = MessageCode.N.ToString();
        //        responseResult.SystemMessage = ex.Message;
        //    }
        //    return responseResult;
        //}


        // POST: api/Student/AddOrUpdateStudent
        //[Route("AddOrUpdateStudent")]
        [Route("UpdateStudent")]
        public ResponseResult UpdateStudent(StudentVM studentVM)
        {
            ResponseResult responseResult = new ResponseResult();
            //try
            //{
            //    StudentInfo studentInfoObj = new StudentInfo();

            //    _dbContext.StudentBasics.Add(studentBasic);
            //    _dbContext.StudentInfoes.Add(studentInfo);
            //    _dbContext.SaveChanges();

            //    responseResult.Content = null;
            //    responseResult.MessageCode = MessageCode.Y.ToString();
            //    responseResult.SystemMessage = "Data Saved Succesfully.";
            //}
            //catch (Exception ex)
            //{
            //    responseResult.Content = null;
            //    responseResult.MessageCode = MessageCode.N.ToString();
            //    responseResult.SystemMessage = ex.Message;
            //}
            return responseResult;
        }
        //=============
        //public ResponseResult AddOrUpdateStudent(StudentVM studentVM, StudentBasic studentBasic, StudentInfo studentInfo )
        //{
        //    ResponseResult responseResult = new ResponseResult();
        //    try
        //    {
        //        if (studentVM == null)
        //        {
        //            responseResult.Content = null;
        //            responseResult.MessageCode = MessageCode.N.ToString();
        //            responseResult.SystemMessage = "Inputs is null!";
        //            return responseResult;
        //        }

        //        _dbContext.StudentBasics.Add(studentVM.SectionId, studentVM.Name, studentVM.Birthday, studentVM.FatherName,
        //                                  studentVM.MotherName, studentVM.PresentAddress, studentVM.PermanentAddress,
        //                                    studentVM.Contact1, studentVM.Contact2);



        //        //_dbContext.sp_AddStudentInfo(studentVM.SectionId, studentVM.Name, studentVM.Birthday, studentVM.FatherName,
        //        //                            studentVM.MotherName, studentVM.PresentAddress, studentVM.PermanentAddress,
        //        //                            studentVM.Contact1, studentVM.Contact2,
        //        //                            studentVM.Roll, studentVM.Year, studentVM.Shift, studentVM.ClassId, 
        //        //                            studentVM.SectionId, studentVM.Residential);

        //        responseResult.Content = null;
        //        responseResult.MessageCode = MessageCode.Y.ToString();
        //        responseResult.SystemMessage = "Data Saved Succesfully.";
        //    }
        //    catch(Exception ex)
        //    {
        //        responseResult.Content = null;
        //        responseResult.MessageCode = MessageCode.N.ToString();
        //        responseResult.SystemMessage = ex.Message;
        //    }
        //    return responseResult;
        //}




        // GET: api/Student



        public enum MessageCode
        {
            Y, N
        }
    }
}
