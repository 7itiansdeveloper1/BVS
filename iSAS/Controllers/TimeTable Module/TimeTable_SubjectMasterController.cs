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
    public class TimeTable_SubjectMasterController : Controller
    {
        
        private ITimeTable_SubjectMasterRepo _timetableSubjectRepo;
        public TimeTable_SubjectMasterController(ITimeTable_SubjectMasterRepo timetableSubjectRepo)
        {
            _timetableSubjectRepo = timetableSubjectRepo;
        }

        
        public ActionResult LandingPage()
        {
            return PartialView(_timetableSubjectRepo.TimeTable_SubjectMaster_Transaction());
        }
        
        public ActionResult New()
        {
            TimeTable_SubjectMasterModel model = new TimeTable_SubjectMasterModel();
            model.timeTableSubject.IsActive = true;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string subjectId)
        {
            return View(_timetableSubjectRepo.TimeTable_SubjectMaster_Transaction(subjectId));
        }
        

        [HttpPost]
        public JsonResult TimeTable_SubjectMaster_CRUD(TimeTable_SubjectMasterModel model)
        {
            if (ModelState.IsValid)
            {
                model.timeTableSubject.userId = Session["UserId"].ToString();
                Tuple<int, string> res = _timetableSubjectRepo.TimeTable_SubjectMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        
    }
}