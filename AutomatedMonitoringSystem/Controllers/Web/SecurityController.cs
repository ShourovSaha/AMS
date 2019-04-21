using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using AutomatedMonitoringSystem.Models.ViewModels;
using AutomatedMonitoringSystem.Models.NetworkModels;
using AutomatedMonitoringSystem.CommonClass;
using Newtonsoft.Json;

namespace AutomatedMonitoringSystem.Controllers.Web
{
    public class SecurityController : Controller
    {

        private ApiRequest _apiRequestObj = null;

        public SecurityController()
        {
            _apiRequestObj = new ApiRequest();
        }

        // GET: Security
        public ActionResult LogIn()
        {
            return View();
        }


        // Post: Security
        [HttpPost]
        public ActionResult LogIn(long phone, string password)
        {
            try
            {
                UserLogInRequestVM userLogInRequestVMObj = new UserLogInRequestVM()
                {
                    Phone = phone,
                    Password = password
                };

                object res = _apiRequestObj.HttpPostRequest(userLogInRequestVMObj, "api/Security/UserLogin");
                string response = res.ToString();
                ResponseResult responseResultObj = JsonConvert.DeserializeObject<ResponseResult>(response);

                if (responseResultObj.MessageCode == "Y")
                {
                    UserLogInInfoVM userInfo = JsonConvert.DeserializeObject<UserLogInInfoVM>(responseResultObj.Content.ToString());
                    //SessionInitialize(userInfo);
                    SessionInitialize(userInfo);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msgAlert"] = "NotAuthorized";
                    TempData["msgAlertDetails"] = responseResultObj.SystemMessage.ToString();
                    //Session["LogInUserInfo"] = null;
                    return RedirectToAction("LogIn");
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["systemErrorMsg"] = ex.Message;
                TempData["msgAlertDetails"] = "Sorry, something wrong. Please wait and try after a few minutes.";
                return RedirectToAction("LogIn");
            }
        }

        private void SessionInitialize(UserLogInInfoVM userInfo)
        {
            Session["LogInUserFristName"] = userInfo.FirstName;
            Session["LogInUserPhone"] = userInfo.UId;
            Session["LogInUserType"] = userInfo.UserType;
        }

        //public void SessionInitialize(UserLogInInfoVM userLogInInfoVM)
        //{
        //    Session["UserInfo"] = new GlobalUserInfo()
        //    {
        //        FristName = userLogInInfoVM.FirstName,
        //        LastName = userLogInInfoVM.LastName,
        //        UserId = userLogInInfoVM.UId,
        //        UserType = userLogInInfoVM.UserType
        //    };
        //}

        public ActionResult LogOut()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("LogIn");
        }

        // GET: Security/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Security/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Security/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Security/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Security/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
