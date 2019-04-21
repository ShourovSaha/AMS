using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models.ViewModels;
using AutomatedMonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutomatedMonitoringSystem.Models.NetworkModels;
using System.Data.Entity.SqlServer;

namespace AutomatedMonitoringSystem.Controllers.API
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly AMSDBEntities _dbContext = null;

        public UserController()
        {
            _dbContext = new AMSDBEntities();

        }



        // POST: api/User/AddUser
        [HttpPost]
        [Route("AddUser")]
        public ResponseResult AddUser(UserInfoVM userInfoVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                User user = new User()
                {
                    UId = userInfoVM.UId,
                    FirstName = userInfoVM.FirstName,
                    LastName = userInfoVM.LastName,
                    Birthday = userInfoVM.Birthday,
                    Gender = userInfoVM.Gender,
                    Email = userInfoVM.Email,
                    Password = userInfoVM.Password,
                    UserTypeId = userInfoVM.UserTypeId,
                    Status = userInfoVM.Status
                };

                _dbContext.Users.Add(user);
                //AssignGuardianWithStudent(userInfoVM.StudentId, userInfoVM.UId);
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


        // POST: api/User/AssignTeacherWithSubject
        [HttpPost]
        [Route("AssignTeacherWithSubject")]
        public ResponseResult AssignTeacherWithSubject(TeacherSubjectVM teacherSubjectVM)
        {
            int muskingId; 

            ResponseResult responseResult = new ResponseResult();
            //System.Data.Entity.Core.Objects.ObjectParameter MSG_Code =
            //   new System.Data.Entity.Core.Objects.ObjectParameter("MSG_Code", typeof(string));
            //System.Data.Entity.Core.Objects.ObjectParameter MSG =
            //    new System.Data.Entity.Core.Objects.ObjectParameter("MSG", typeof(string));

            try
            {

                if(IsTeacherAlreadyAssignedToSubject(teacherSubjectVM) == true)
                {
                    responseResult.MessageCode = "Y";
                    responseResult.SystemMessage = "Value Already Assigned!";
                    responseResult.Content = null;
                    return responseResult;
                }

                if(IsSubjectmuskingIdAlredyExists(teacherSubjectVM.SubjectId) == true)
                {
                    muskingId = AddSubjectMuskingId(teacherSubjectVM.SubjectId);//Adding new muskingId to existing subject 
                    TeacherSubject teacherSubjectObj = new TeacherSubject()
                    {
                        MaskingSubjectId = muskingId,
                        TeacherId = teacherSubjectVM.UserId
                    };
                    _dbContext.TeacherSubjects.Add(teacherSubjectObj);
                }
                else
                {
                    TeacherSubject teacherSubjectObj = new TeacherSubject()
                    {
                        MaskingSubjectId = GetSubjectMaskingIdBySubjactid(teacherSubjectVM.SubjectId),
                        TeacherId = teacherSubjectVM.UserId
                    };
                    _dbContext.TeacherSubjects.Add(teacherSubjectObj);
                }
                _dbContext.SaveChanges();
                responseResult.MessageCode = "Y";
                responseResult.SystemMessage = "Data seved succesfully";
                responseResult.Content = null;
            }
            catch (Exception ex)
            {
                responseResult.MessageCode = "N";
                responseResult.SystemMessage = ex.Message;
                responseResult.Content = null;
            }
            return responseResult;
        }


        private int AddSubjectMuskingId(int subjectid)
        {
            int newMuskingId = _dbContext.Subjects.Select(a => a.MaskingSubjectId).Max() + 1; 
            try
            {
                Subject subject = _dbContext.Subjects.Where(a => a.SubjectId == subjectid)
                                                 .SingleOrDefault();

                Subject subjectObj = new Subject()
                {
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.SubjectName,
                    ClassId = subject.ClassId,
                    SectionId = subject.SectionId,
                    MaskingSubjectId = newMuskingId
                };

                _dbContext.Subjects.Add(subjectObj);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
            return newMuskingId;
        }


        private bool IsSubjectmuskingIdAlredyExists(int subjectid)
        {
            List<int> subjectMuskingIds = GetSubjectMuskingIds(subjectid);
            List<TeacherSubject> data = new List<TeacherSubject>();
            for (int i = 0; i < subjectMuskingIds.Count; i++)
            {
                int mId = subjectMuskingIds[i];
                data = _dbContext.TeacherSubjects.Where(s => s.MaskingSubjectId == mId)
                                                .ToList();
                if (data.Count > 0)
                {
                    return true;
                }
            }
            if (data.Count() > 0)
            {
                return true;
            }
            return false;
        }


        private bool IsTeacherAlreadyAssignedToSubject(TeacherSubjectVM model)
        {
            List<int> subjectMuskingIds = GetSubjectMuskingIds(model.SubjectId);
            List<TeacherSubject> data = new List<TeacherSubject>(); 
            for (int i = 0; i < subjectMuskingIds.Count; i++)
            {
                int mId = subjectMuskingIds[i];
                data = _dbContext.TeacherSubjects.Where(s => s.TeacherId == model.UserId &&
                                                s.MaskingSubjectId == mId)
                                                .ToList();   
                if(data.Count > 0)
                {
                    return true;
                }
            }
            if (data.Count() > 0)
            {
                return true;
            }
            return false;
        }


        // POST: api/User/UserTypeByPhoneNumber
        [HttpPost]
        [Route("UserTypeByPhoneNumber")]
        public ResponseResult GetUserTypeByPhoneNumber(long userPhoneNumber)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                int userType = _dbContext.Users.Where(a => a.UId == userPhoneNumber)
                                                    .Select(x => x.UserTypeId)
                                                    .SingleOrDefault();

                if (userType != 0)
                {
                    responseResult.Content = userType;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data found.";
                }
                else
                {
                    responseResult.Content = userType;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Data not found.";
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



        // POST: api/User/AllGurdians
        [HttpPost]
        [Route("AllGurdians")]
        public ResponseResult GetAllGurdians()
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var gurdians = _dbContext.Users.ToList();

                if (gurdians != null)
                {
                    responseResult.Content = gurdians;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data found.";
                }
                else
                {
                    responseResult.Content = gurdians;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Data not found.";
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



        // POST: api/User/DoesUserExist
        [HttpPost]
        [Route("DoesUserExist")]
        public ResponseResult DoesUserExist(long userPhoneNumber)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var phoneNumber = _dbContext.Users.Where(a => a.UId == userPhoneNumber)
                                                    .Select(x => x.UId)
                                                    .SingleOrDefault();

                if (phoneNumber != 0)
                {
                    responseResult.Content = true;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "User Found.";
                }
                else
                {
                    responseResult.Content = false;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "User not found.";
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


        public int GetSubjectMaskingIdBySubjactid(int subjectId)
        {
            return _dbContext.Subjects
                            .Where(a => a.SubjectId == subjectId)
                            .Select(a => a.MaskingSubjectId)
                            .SingleOrDefault();
        }



        // Post: api/User/AssignTeachersSubjectWithPeriod
        [HttpPost]
        [Route("AssignTeachersSubjectWithPeriod")]
        public ResponseResult AssignTeachersSubjectWithPeriod(AssignGuardianWithStudentVM assignGuardianWithStudentVM)
        {
            ResponseResult responseResult = new ResponseResult();
            RelationStudentGuardian relationStudentGuardianObj = new RelationStudentGuardian()
            {
                StudentId = assignGuardianWithStudentVM.StudentId,
                UserId = assignGuardianWithStudentVM.UserId
            };

            try
            {
                if (IsStudentGardianRelationExists1(assignGuardianWithStudentVM) != true)
                {
                    _dbContext.RelationStudentGuardians.Add(relationStudentGuardianObj);
                    _dbContext.SaveChanges();

                    responseResult.Content = null;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data Saved Succesfully.";
                }
                else
                {
                    responseResult.Content = null;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Relation Already Exists.";
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



        // Post: api/User/AssignGuardianWithStudent
        [HttpPost]
        [Route("AssignGuardianWithStudent")]
        public ResponseResult AssignGuardianWithStudent(AssignGuardianWithStudentVM assignGuardianWithStudentVM)
        {
            ResponseResult responseResult = new ResponseResult();
            RelationStudentGuardian relationStudentGuardianObj = new RelationStudentGuardian()
            {
                StudentId = assignGuardianWithStudentVM.StudentId,
                UserId = assignGuardianWithStudentVM.UserId
            };

            try
            {
                if (IsStudentGardianRelationExists1(assignGuardianWithStudentVM) != true)
                {
                    _dbContext.RelationStudentGuardians.Add(relationStudentGuardianObj);
                    _dbContext.SaveChanges();

                    responseResult.Content = null;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Data Saved Succesfully.";
                }
                else
                {
                    responseResult.Content = null;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Relation Already Exists.";
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



        public bool IsStudentGardianRelationExists1(AssignGuardianWithStudentVM assignGuardianWithStudentVM)
        {
            try
            {
                var result = _dbContext.RelationStudentGuardians
                                       .Where(a => a.StudentId == assignGuardianWithStudentVM.StudentId &&
                                                           a.UserId == assignGuardianWithStudentVM.UserId)
                                       .SingleOrDefault();

                if (result != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Post: api/User/IsStudentGardianRelationExists
        [HttpPost]
        [Route("IsStudentGardianRelationExists")]
        public ResponseResult IsStudentGardianRelationExists(AssignGuardianWithStudentVM assignGuardianWithStudentVM)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                var result = _dbContext.RelationStudentGuardians
                                        .Where(a => a.StudentId == assignGuardianWithStudentVM.StudentId &&
                                                            a.UserId == assignGuardianWithStudentVM.UserId)
                                        .SingleOrDefault();

                if (result != null)
                {
                    responseResult.Content = true;
                    responseResult.MessageCode = MessageCode.Y.ToString();
                    responseResult.SystemMessage = "Relation Found.";
                }
                else
                {
                    responseResult.Content = false;
                    responseResult.MessageCode = MessageCode.N.ToString();
                    responseResult.SystemMessage = "Relation not found.";
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

        // Post: api/User/GardiansPhoneNumber
        [HttpPost]
        [Route("GardiansPhoneNumber")]
        public ResponseResult GetGardiansPhoneNumber(long phoneNumber)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                responseResult.Content = _dbContext.Users
                                                    .Where(x => SqlFunctions.StringConvert((double)x.UId)
                                                    .TrimStart().StartsWith(phoneNumber.ToString()));


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

        // Post: api/User/GetStudentsInfoListByName
        [HttpPost]
        [Route("GetStudentsInfoListByName")]
        public ResponseResult GetStudentsInfoListByName(string name)
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                responseResult.Content = _dbContext.GetStudentsInfoListByName_SP(name);
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


        // Post: api/User/TeachersBySubject
        [HttpPost]
        [Route("TeachersBySubject")]
        public ResponseResult GetAllTeachersBySubject(int subjectId)
        {
            ResponseResult responseResult = new ResponseResult();
            List<object> teachers = new List<object>();
            try
            {
                List<int> subjectMuskingIds = GetSubjectMuskingIds(subjectId);
                for (int i = 0; i < subjectMuskingIds.Count; i++)     
                {
                    var data = _dbContext.GetTeacherInfoByMuskingId(subjectMuskingIds[i]);
                    teachers.Add(data);
                }
                responseResult.Content = teachers;

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


        public List<int> GetSubjectMuskingIds(int subjectId)
        {
            ResponseResult responseResult = new ResponseResult();
            //int result = 0;
            List<int> subjectMuskingIds = new List<int>();
            try
            {
                subjectMuskingIds = _dbContext.Subjects
                                    .Where(a => a.SubjectId == subjectId)
                                    .Select(a => a.MaskingSubjectId)
                                    .ToList();
            }
            catch (Exception ex)
            {
                responseResult.Content = null;
                responseResult.MessageCode = MessageCode.Y.ToString();
                responseResult.SystemMessage = ex.Message;
            }
            return subjectMuskingIds;
        }



        // Post: api/User/Teachers
        [HttpPost]
        [Route("Teachers")]
        public ResponseResult GetAllTeachers()
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                responseResult.Content = _dbContext.Users.Where(a => a.UserTypeId == 2)
                                                          .Select(a => new
                                                          {
                                                              a.UId,
                                                              a.FirstName,
                                                              a.LastName,
                                                              a.Gender,
                                                              a.Email
                                                          });
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

        public enum MessageCode
        {
            Y, N
        }
    }
}
