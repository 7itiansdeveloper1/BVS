using ISas.Entities.TimeTable_Entities;
using ISas.Repository.TimeTable_Repo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.TimeTable_Module
{
    [Authorize]
    [ExceptionHandler]
    public class TimeTable_SetupController : Controller
    {
        private ITimeTable_SetupRepo _timeTableRepo;
        public TimeTable_SetupController(ITimeTable_SetupRepo timeTableRepo)
        {
            _timeTableRepo = timeTableRepo;
        }

        public ActionResult LandingPage()
        {
            return View(_timeTableRepo.GetLandingPageDetails());
        }

        [EncryptedActionParameter]
        public ActionResult New(string ClassSectionId, string ClassSectionName, string ClassTeacherName)
        {
            TimeTable_SetupModels model = _timeTableRepo.TimeTable_Setup_FormLoad(ClassSectionId);

            if (model == null)
                model = new TimeTable_SetupModels();

            model.ClassSectionId = ClassSectionId;
            model.ClassSectionName = ClassSectionName;
            model.ClassTeacherName = ClassTeacherName;
            return View(model);
        }


        [EncryptedActionParameter]
        public ActionResult New_V1(string ClassSectionId, string ClassSectionName, string ClassTeacherName)
        {
            TimeTable_SetupModels model = _timeTableRepo.TimeTable_Setup_FormLoad_V1(ClassSectionId);

            if (model == null)
                model = new TimeTable_SetupModels();
            model.ClassSectionId = ClassSectionId;
            model.ClassSectionName = ClassSectionName;
            model.ClassTeacherName = ClassTeacherName;
            return View(model);
        }


        [EncryptedActionParameter]
        public ActionResult New_V2(string ClassSectionId, string ClassSectionName, string ClassTeacherName)
        {
            TimeTable_SetupModels model = _timeTableRepo.TimeTable_Setup_FormLoad(ClassSectionId);

            if (model == null)
                model = new TimeTable_SetupModels();

            model.ClassSectionId = ClassSectionId;
            model.ClassSectionName = ClassSectionName;
            model.ClassTeacherName = ClassTeacherName;
            return View(model);
        }



        
        public PartialViewResult New_V2_Inner(string ClassSectionId, string ClassSectionName, string ClassTeacherName)
        {
            TimeTable_SetupModels model = _timeTableRepo.TimeTable_Setup_FormLoad(ClassSectionId);

            if (model == null)
                model = new TimeTable_SetupModels();

            model.ClassSectionId = ClassSectionId;
            model.ClassSectionName = ClassSectionName;
            model.ClassTeacherName = ClassTeacherName;
            return PartialView(model);
        }


        public PartialViewResult _AddSubject(string p,string d,string classsectionid,string classsection,string classteacher)
        {
            return PartialView(_timeTableRepo.getSubjects(p,d,classsectionid, classsection,  classteacher));
        }

        public PartialViewResult _TeacherList(string ClassSectionId, string SubjectId)
        {
            TimeTable_SetupModels model = new TimeTable_SetupModels();
            model.TeacherList = _timeTableRepo.GetTeacherListBySubjectId(ClassSectionId, SubjectId);
            return PartialView(model);
        }

        public JsonResult TimeTable_Setup_CRUD(TimeTable_SetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _timeTableRepo.TimeTable_Setup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TimeTable_Setup_CRUD_V1(TimeTable_SetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _timeTableRepo.TimeTable_Setup_CRUD_V1(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult TimeTable_Setup_CRUD_V2(string classsectionId, string p, string d, string pdValue,string mode)
        {
                Tuple<int, string> res = _timeTableRepo.TimeTable_Setup_CRUD_V2(classsectionId,  p, d, pdValue, Session["UserId"].ToString(), mode);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            
        }


        public JsonResult CheckStaffAvailabilityForTimeTable(string StaffId, string Period, int DayNo)
        {
            Tuple<bool, string> res = _timeTableRepo.CheckStaffAvailabilityForTimeTableMapping(StaffId, Period, DayNo);
            return Json(new { status = res.Item1, Msg = res.Item2, Color = res.Item1 == true ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}