using ISas.Entities.DashboardEntities;
using ISas.Entities.TimeTable_Entities;
using ISas.Repository.DashboardRepository.IRepository;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Dashboard
{
    [Authorize]
    [ExceptionHandler]
    public class DashBoard_StaffStataticsController : Controller
    {
        private IDashBoard_StaffStataticsRepo _staffStatics;
        private IStaff_StaffDetailMasterRepo _staffRepo;
        public DashBoard_StaffStataticsController(IDashBoard_StaffStataticsRepo staffStatics, IStaff_StaffDetailMasterRepo staffRepo)
        {
            this._staffRepo = staffRepo;
            _staffStatics = staffStatics;
        }
        public ViewResult MyClassInfo()
        {
            return View(_staffStatics.GetClassInfo(Session["SessionId"].ToString(), Session["UserId"].ToString()));
        }

        public ViewResult SalryDetails()
        {
            return View(_staffStatics.GetSalaryDetails(Session["SessionId"].ToString(), Session["UserId"].ToString()));
        }

        //public JsonResult GetSalaryDetails()
        //{
        //    var graphData = _staffStatics.GetSalaryDetails(Session["SessionId"].ToString(), Session["UserId"].ToString()).Select(r => new
        //    {
        //        country = r.SalMonthName,
        //        visits = r.InHand,
        //    }).ToList();
        //    return Json(graphData, JsonRequestBehavior.AllowGet);
        //}


        public JsonResult GetLeaveBalanceSummary()
        {
            List<LeaveBalanceDetailsModel> leaveBalances = _staffStatics.GetStaffLeaveBalanceDetails(Session["UserId"].ToString(), DateTime.Now.Month, DateTime.Now.Year);
            var graphData = leaveBalances.Select(r => new SelectListItem
            {
                Text = r.LvCode,
                Value = r.LeaveAvailed.ToString(),
            }).ToList();

            if (graphData != null)
                graphData.Add(new SelectListItem { Text = "Balance", Value = leaveBalances.Sum(r => r.LeaveAvailable).ToString() });

            return Json(graphData, JsonRequestBehavior.AllowGet);
        }

        public ViewResult BookHistory()
        {
            return View(_staffStatics.GetBookHistory(Session["SessionId"].ToString(), Session["UserId"].ToString()));
        }

        public ViewResult SMSDetails()
        {
            return View(_staffStatics.GetSMSDetails(Session["SessionId"].ToString(), Session["UserId"].ToString()));
        }

        public ViewResult MyTimeTable(string StaffId, string StaffName)
        {
            TimeTable_SetupModels model = _staffRepo.GetStaffTimeTable(StaffId);
            if (model != null)
                model.ClassTeacherName = StaffName;
            return View(model);

        }

        public ViewResult AttendanceDetails()
        {
            Staff_AttendanceDetailsModel model = _staffStatics.GetStaffAttendanceInfo_FormLoad(Session["UserId"].ToString(), DateTime.Now.Month, DateTime.Now.Year);
            if (model != null)
            {
                model.FromDate = DateTime.Now.ToShortDateString().Replace("-", "/");
                model.ToDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            }
            return View(model);
        }

        public PartialViewResult _StaffAttenMonthCalenderPartial()
        {
            return PartialView();
        }

        public ActionResult AsyncUpdateCalender(int month, int year)
        {
            List<Tuple<string, string>> attendenceDetails = _staffStatics.GetStaffAttenDetails(month, year, Session["UserId"].ToString());
            WeekForMonth model = DashboardController.getCalender(month, year, attendenceDetails, Session["UserId"].ToString());
            model.MonthNameWithYear = DateHelpers.GetMonthByName(month) + " " + year.ToString();
            return PartialView("_StaffAttenMonthCalenderPartial", model);
        }




        [HttpPost]
        public JsonResult DashBoard_StaffStatatics_CRUD(Staff_AttendanceDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                Tuple<int, string> res = _staffStatics.DashBoard_StaffStatatics_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}