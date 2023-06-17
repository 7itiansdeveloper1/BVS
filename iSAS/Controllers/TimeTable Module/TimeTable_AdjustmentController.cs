using ISas.Entities.TimeTable_Entities;
using ISas.Repository.TimeTable_Repo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.TimeTable_Module
{
    [Authorize]
    [ExceptionHandler]
    public class TimeTable_AdjustmentController : Controller
    {
        private ITimeTable_AdjustmentRepo _adjustmentRepo;
        public TimeTable_AdjustmentController(ITimeTable_AdjustmentRepo adjustmentRepo)
        {
            _adjustmentRepo = adjustmentRepo;
        }
        // GET: TimeTable_Adjustment
        public ActionResult LandingPage()
        {
            TimeTable_Adjustment_FormLoadModel model = _adjustmentRepo.TimeTable_Adjustment_FormLoad(DateTime.Now.ToShortDateString());
            if (model != null)
                model.AdjustmentDate = DateTime.Now.ToShortDateString();
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult New(string Date, string TeacherId, string TeacherName)
        {
            TimeTable_Adjustment_TransactionModel model = new TimeTable_Adjustment_TransactionModel();
            model.AbsentDate = Date;
            model.AbsentStaffId = TeacherId;
            model.AbsentStaffName = TeacherName;
            //model.EffectedClassList = _adjustmentRepo.GetEffectedClassList(Date, TeacherId);
            return View(model);
        }
        
        public ActionResult DayAdjustment()
        {
            return View(_adjustmentRepo.TimeTable_Adjustment_FormLoad());
        }

        public ActionResult _DayAdjustment(string adjustmentDate,string wingid,string reportname)
        {
            return PartialView(_adjustmentRepo.DayAdjustment(adjustmentDate, wingid,reportname));
        }
        public ActionResult _AdjustPeriod(string TeacherId, string Date)
        {
            return PartialView(_adjustmentRepo.GetEffectedPeriodWithAvailableStaff(TeacherId, Date, null));
        }

        public JsonResult TimeTable_Adjustment_CRUD(TimeTable_Adjustment_TransactionModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _adjustmentRepo.TimeTable_Adjustment_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}