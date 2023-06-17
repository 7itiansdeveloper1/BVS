using ISas.Entities.Examination_Entities;
using ISas.Repository.ExaminationRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Examination_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Examination_AssessmentSetupController : Controller
    {
        private IExamination_AssessmentSetupRepo _assessmentsetupRepo;
        public Examination_AssessmentSetupController(IExamination_AssessmentSetupRepo assessmentSetup)
        {
            _assessmentsetupRepo = assessmentSetup;
        }

        // GET: Examination_AssessmentSetup
        public PartialViewResult LandingPage(string ExamTemplateId, string SubjectId,
            string MainSubjectId = "", string MainSubjectName = "", string IsChildSubjectSetup = "NO")
        {
            ViewBag.MainSubjectId = MainSubjectId;
            ViewBag.MainSubjectName = MainSubjectName;
            ViewBag.IsChildSubjectSetup = IsChildSubjectSetup;
            return PartialView(_assessmentsetupRepo.AssessmentList(ExamTemplateId, 0, SubjectId));
        }

        [EncryptedActionParameter]
        public ViewResult New(string ExamTemplateId, string SubjectId, string ExamTempleteName, string SubjectName,
            string MainSubjectId = "", string MainSubjectName = "", string IsChildSubjectSetup = "NO")
        {
            Examination_AssessmentSetupModels model = new Examination_AssessmentSetupModels();
            model.AssessmentNameList = _assessmentsetupRepo.AssessmentNameList();
            model.ExamTemplateId = ExamTemplateId;
            model.SubjectId = SubjectId;
            model.TransactionMode = "SAVE";

            model.Exam_TemplateName = ExamTempleteName;
            model.SubjectName = SubjectName;


            model.MainSubjectId = MainSubjectId;
            model.MainSubjectName = MainSubjectName;
            model.IsChildSubjectSetup = IsChildSubjectSetup == "NO" ? false : true;

            return View(model);
        }

        [EncryptedActionParameter]
        public ViewResult Updation(string ExamTempleteId, string AssessmentPropertyId, string SubjectId,
             string MainSubjectId = "", string MainSubjectName = "", string IsChildSubjectSetup = "NO")
        {
            Examination_AssessmentSetupModels model = _assessmentsetupRepo.GetAssessmentById(ExamTempleteId, Convert.ToInt32(AssessmentPropertyId), SubjectId);
            if (model != null)
            {
                model.MainSubjectId = MainSubjectId;
                model.MainSubjectName = MainSubjectName;
                model.IsChildSubjectSetup = IsChildSubjectSetup == "NO" ? false : true;

                model.TransactionMode = "UPDATE";
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Examination_AssessmentSetup_CRUD(Examination_AssessmentSetupModels model)
        {
            if (model.ChkBoxAddNewAssessment)
            {
                if (string.IsNullOrEmpty(model.AssessmentName))
                    ModelState.AddModelError("AssessmentName", "Assessment Name is Req..!");

                if (string.IsNullOrEmpty(model.AssessmentDisplayName))
                    ModelState.AddModelError("AssessmentDisplayName", "Display Name is Req..!");
            }
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();

                Tuple<int, string> res = _assessmentsetupRepo.Examination_AssessmentSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Examination_AssessmentSetup_Delete(string AssessmentId)
        {
            Tuple<int, string> res = _assessmentsetupRepo.Examination_AssessmentSetup_CRUD(AssessmentId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult AssessmentPropertySetup(int AssessmentPropertyId)
        {
            AssessmentPropertySetupModel model = _assessmentsetupRepo.GetAssessmentPropertyDetails(AssessmentPropertyId);
            if (string.IsNullOrEmpty(model.AssessmentNature))
                model.AssessmentNature = "T";


            return PartialView(model);
        }

        [HttpPost]
        public JsonResult AssessmentPropertySetup_CRUD(AssessmentPropertySetupModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.TransactionMode = "SAVE";
                Tuple<int, string> res = _assessmentsetupRepo.Examination_AssessmentPropertySetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

    }
}