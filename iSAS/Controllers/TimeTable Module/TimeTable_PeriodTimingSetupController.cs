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
    public class TimeTable_PeriodTimingSetupController : Controller
    {
        private ITimeTable_PeriodTimingSetupRepo _timeSetupRepo;
        public TimeTable_PeriodTimingSetupController(ITimeTable_PeriodTimingSetupRepo timeSetupRepo)
        {
            _timeSetupRepo = timeSetupRepo;
        }


        [EncryptedActionParameter]
        public ActionResult New(string ClassSectionId, string ClassSectionName, string ClassTeacher)
        {
            //ClassSectionId = "CLS0000001SEC01";
            //ClassSectionName = "I A (40)";
            //ClassTeacher = "Alka Batra";

            TimeTable_PeriodTimingSetupModels model = new TimeTable_PeriodTimingSetupModels();
            model.SeasonList = _timeSetupRepo.GetSeasonList();
            model.ClassSectionId = ClassSectionId;
            model.ClassTeacherName = ClassTeacher;
            model.ClassSectionName = ClassSectionName;

            return View(model);
        }

        public PartialViewResult _PeriodTimingDetails(string ClassSectionId, string SeasonId)
        {
            return PartialView(_timeSetupRepo.GetPeriodTimingDetails(ClassSectionId, SeasonId));
        }

        public JsonResult TimeTable_PeriodTimingSetup_CRUD(TimeTable_PeriodTimingSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _timeSetupRepo.TimeTable_PeriodTimingSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TimeTable_PeriodTimingSetup_CopyToClass(string SeasonId, string FromClassSecId, string ToClassSecId)
        {
            Tuple<int, string> res = _timeSetupRepo.TimeTable_PeriodTimingSetup_CRUD(SeasonId, FromClassSecId, ToClassSecId, Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}