using ISas.Entities.Exam_Entities;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_ClassSubjectSetupController : Controller
    {
        private IExam_ClassSubjectSetupRepo _exam_ClassSubjectSetupRepo;

        public Exam_ClassSubjectSetupController(IExam_ClassSubjectSetupRepo exam_ClassSubjectSetupRepo)
        {
            _exam_ClassSubjectSetupRepo = exam_ClassSubjectSetupRepo;
        }
        public ActionResult LandingPage(string classsectionid)
        {
            return PartialView(_exam_ClassSubjectSetupRepo.ClassSubjectSetup_classSubject("classsubject",classsectionid));
        }

        public ActionResult New()
        {
            return View(_exam_ClassSubjectSetupRepo.ClassSubjectSetup("formLoad"));
        }

        public JsonResult GetAssessmentList(string classsectionid,string subjectid)
        {
            return Json(_exam_ClassSubjectSetupRepo.ClassSubjectSetup_assessment("assessment", classsectionid, subjectid), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Exam_ClassSubjectSetup_CRUD(Exam_ClassSubjectSetup model)
        {
            if (ModelState.IsValid)
            {
                Tuple<int, string> res = _exam_ClassSubjectSetupRepo.ClassSubjectSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}