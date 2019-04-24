using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomatedMonitoringSystem.Models;
using AutomatedMonitoringSystem.Models.ViewModels;
using AutomatedMonitoringSystem.CommonClass;
using AutomatedMonitoringSystem.Models.NetworkModels;
using Newtonsoft.Json;

namespace AutomatedMonitoringSystem.Controllers.Web
{
    public class NoticesController : BaseController
    {
        //private readonly AMSDBEntities db = null;
        private ApiRequest _apiRequest = null;

        public NoticesController()
        {
            _apiRequest = new ApiRequest();
        }


        public ActionResult AddNotice()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNotice(int subjectId, int classId, int sectionId, string Title, string Message, DateTime PostedForTime)
        {           
            ResponseResult responseResult = new ResponseResult();
            NoticeVM noticeVM = new NoticeVM()
            {
                ClassId = classId,
                SectionId = sectionId,
                SubjectId = subjectId,
                Title = Title,
                Message = Message,
                PostedBy = (long)Session["LogInUserPhone"],
                PostedDate = GetLocalTime(),
                PostedForTime = PostedForTime, 
                UpdatedDate = GetLocalTime()
            };
            try
            {
                var res = _apiRequest.HttpPostRequest(noticeVM, "api/Notice/AddNotice");
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
            return View();
        }


        public ActionResult GetNiticeList()
        {
            ResponseResult responseResult = new ResponseResult();
            List<GetNoticeDetails_SP_Result> noticeList = new List<GetNoticeDetails_SP_Result>();
            try
            {
                var res = _apiRequest.HttpPostRequest(null, "api/Notice/NoticeList");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    noticeList = JsonConvert.DeserializeObject<List<GetNoticeDetails_SP_Result>>(responseResult.Content.ToString());

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;
                }
            }
            catch (Exception ex)
            {
                TempData["msgAlert"] = "N";
                TempData["msgAlertDetails"] = ex.Message.ToString();
            }
            return View(noticeList); 
        }


        //==================
        // GET: Notices
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Notices.ToListAsync());
        //}


        //// GET: Notices/Details/5
        //public async Task<ActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Notice notice = await db.Notices.FindAsync(id);
        //    if (notice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notice);
        //}

        //// GET: Notices/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Notices/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,PostedBy,MaskingId,Message,Title,PostedDate,UpdatedDate,PostedForTime")] Notice notice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Notices.Add(notice);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(notice);
        //}

        //// GET: Notices/Edit/5
        //public async Task<ActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Notice notice = await db.Notices.FindAsync(id);
        //    if (notice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notice);
        //}

        //// POST: Notices/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,PostedBy,MaskingId,Message,Title,PostedDate,UpdatedDate,PostedForTime")] Notice notice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(notice).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(notice);
        //}

        //// GET: Notices/Delete/5
        //public async Task<ActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Notice notice = await db.Notices.FindAsync(id);
        //    if (notice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(notice);
        //}

        //// POST: Notices/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(long id)
        //{
        //    Notice notice = await db.Notices.FindAsync(id);
        //    db.Notices.Remove(notice);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
