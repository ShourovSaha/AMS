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
    public class PeriodController : BaseController
    {
        private readonly AMSDBEntities _dbContext = null;
        private ApiRequest _apiRequest = null;

        public PeriodController()
        {
            _dbContext = new AMSDBEntities();
            _apiRequest = new ApiRequest();
        }


        public ActionResult GetRoutine()
        {
            ViewData["ClassList"] = new SelectList(GetClassListForDropDown(), "ClassId", "ClassWithShift");
            ViewData["SectionList"] = new SelectList(GetSectionListForDropDown(), "SectionId", "SectionName");
            return View(new Dictionary<string, List<GetPeriodsByClassSectionShift_SP_Result>>());
        }

        [HttpPost]
        public ActionResult GetRoutine(string classId, string sectionid, string shift)
        {
            ResponseResult responseResult = new ResponseResult();
            List<GetPeriodsByClassSectionShift_SP_Result> routineList = new List<GetPeriodsByClassSectionShift_SP_Result>();

            Dictionary<string, List<GetPeriodsByClassSectionShift_SP_Result>> pairs =
                new Dictionary<string, List<GetPeriodsByClassSectionShift_SP_Result>>();

            WeeklyRoutinebyClassSectionShiftDayVM vm = new WeeklyRoutinebyClassSectionShiftDayVM()
            {
                ClassId = Convert.ToInt32(classId),
                SectionId = Convert.ToInt32(sectionid),
                Shift = shift
            };
            try
            {
                var res = _apiRequest.HttpPostRequest(vm, "api/Period/RoutineByClassSectionShift");
                string apiResponse = res.ToString();
                responseResult = JsonConvert.DeserializeObject<ResponseResult>(apiResponse);

                if (responseResult.MessageCode == "Y")
                {
                    routineList = JsonConvert.DeserializeObject<List<GetPeriodsByClassSectionShift_SP_Result>>(responseResult.Content.ToString());
                    pairs = FormattingClassRoutine(routineList);

                    //TempData["msgAlert"] = "Y";
                    //TempData["msgAlertDetails"] = responseResult.SystemMessage;

                }
                else if (responseResult.MessageCode == "N")
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
            return View(pairs);
        }


        //Formatting routine 
        public Dictionary<string, List<GetPeriodsByClassSectionShift_SP_Result>> FormattingClassRoutine(List<GetPeriodsByClassSectionShift_SP_Result> dataList)
        {
            Dictionary<string, List<GetPeriodsByClassSectionShift_SP_Result>> pairs =
                new Dictionary<string, List<GetPeriodsByClassSectionShift_SP_Result>>();

            //List<RoutineVM> rList = new List<RoutineVM>();
            //Dictionary<string, List<RoutineVM>> dList = new Dictionary<string, List<RoutineVM>>();


            var sat_Obj = dataList.Where(a => a.Day == "Saturday").ToList();
            if (sat_Obj.Count > 0)
            {
                var returnData = FormateProcess(sat_Obj);
                pairs.Add("Saturday", returnData);
            }
            else
            {
                var returnData = FormateProcess(new List<GetPeriodsByClassSectionShift_SP_Result>());
                pairs.Add("Saturday", returnData);
            }

            var sun_Obj = dataList.Where(a => a.Day == "Sunday").ToList();
            if (sun_Obj.Count > 0)
            {
                var returnData = FormateProcess(sun_Obj);
                pairs.Add("Sunday", returnData);
            }
            else
            {
                var returnData = FormateProcess(new List<GetPeriodsByClassSectionShift_SP_Result>());
                pairs.Add("Sunday", returnData); 
            }

            var mon_Obj = dataList.Where(a => a.Day == "Monday").ToList();
            if (mon_Obj.Count > 0)
            {
                var returnData = FormateProcess(sun_Obj);
                pairs.Add("Monday", returnData);
            }
            else
            {
                var returnData = FormateProcess(new List<GetPeriodsByClassSectionShift_SP_Result>());
                pairs.Add("Monday", returnData);
            }

            var tue_Obj = dataList.Where(a => a.Day == "Tuesday").ToList();
            if (tue_Obj.Count > 0)
            {
                var returnData = FormateProcess(tue_Obj);
                pairs.Add("Tuesday", returnData);
            }
            else
            {
                var returnData = FormateProcess(new List<GetPeriodsByClassSectionShift_SP_Result>());
                pairs.Add("Tuesday", returnData);
            }

            var wed_Obj = dataList.Where(a => a.Day == "Wednesday").ToList();
            if (wed_Obj.Count > 0)
            {
                var returnData = FormateProcess(wed_Obj);
                pairs.Add("Wednesday", returnData);
            }
            else
            {
                var returnData = FormateProcess(new List<GetPeriodsByClassSectionShift_SP_Result>());
                pairs.Add("Wednesday", returnData);
            }

            var thu_Obj = dataList.Where(a => a.Day == "Thrusday").ToList();
            if (thu_Obj.Count > 0)
            {
                var returnData = FormateProcess(thu_Obj);
                pairs.Add("Thrusday", returnData);
            }
            else
            {
                var returnData = FormateProcess(new List<GetPeriodsByClassSectionShift_SP_Result>());
                pairs.Add("Thrusday", returnData);
            }

            var fri_Obj = dataList.Where(a => a.Day == "Friday").ToList();
            if (fri_Obj.Count > 0)
            {
                var returnData = FormateProcess(fri_Obj);
                pairs.Add("Friday", returnData);
            }
            else
            {
                var returnData = FormateProcess(new List<GetPeriodsByClassSectionShift_SP_Result>());
                pairs.Add("Friday", returnData);
            }
            return pairs;
        }

        //Weekly Day wise data formatting to show routine 
        public List<GetPeriodsByClassSectionShift_SP_Result> FormateProcess(List<GetPeriodsByClassSectionShift_SP_Result> dataList)
        {
            List<GetPeriodsByClassSectionShift_SP_Result> dList =
                new List<GetPeriodsByClassSectionShift_SP_Result>();

            if (dataList != null)
            {
                var p1_Obj = dataList.Where(a => a.StartTime == "07:00:00").SingleOrDefault();
                var p2_Obj = dataList.Where(a => a.StartTime == "08:00:00").SingleOrDefault();
                var p3_Obj = dataList.Where(a => a.StartTime == "09:00:00").SingleOrDefault();
                var p4_Obj = dataList.Where(a => a.StartTime == "10:00:00").SingleOrDefault();
                var p5_Obj = dataList.Where(a => a.StartTime == "11:00:00").SingleOrDefault();
                var p6_Obj = dataList.Where(a => a.StartTime == "12:00:00").SingleOrDefault();
                var p7_Obj = dataList.Where(a => a.StartTime == "13:00:00").SingleOrDefault();

                dList.Add(p1_Obj != null ? p1_Obj : new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(p2_Obj != null ? p2_Obj : new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(p3_Obj != null ? p3_Obj : new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(p4_Obj != null ? p4_Obj : new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(p5_Obj != null ? p5_Obj : new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(p6_Obj != null ? p6_Obj : new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(p7_Obj != null ? p7_Obj : new GetPeriodsByClassSectionShift_SP_Result());
            }
            else
            {
                dList.Add(new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(new GetPeriodsByClassSectionShift_SP_Result());
                dList.Add(new GetPeriodsByClassSectionShift_SP_Result());
            }
            return dList;
        }
    }

    public class RoutineVM
    {
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
        public string StratTime { get; set; }
    }
}


//for (int i = 0; i < dataList.Count; i++)
//{
//    switch (dataList[i].Day)
//    {
//        case "Saturday":
//            for (int j = 0; j < 7; j++)
//            {
//                if (dataList[i].StartTime == "07:00:00")
//                {

//                }
//            }

//            break;
//        case "Sunday":
//            break;
//        case "Monday":
//            break;
//        case "Tuesday":
//            break;
//        case "Wednesday":
//            break;
//        case "Thrusday":
//            break;
//        case "Friday":
//            break;
//        default:
//            break;
//    }
//}