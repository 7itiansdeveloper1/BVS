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
    public class Examination_ChildSubjectSetupController : Controller
    {
        private IExamination_ChildSubjectSetupRepo _childSubjectRepo;
        public Examination_ChildSubjectSetupController(IExamination_ChildSubjectSetupRepo subjectMstRepo)
        {
            _childSubjectRepo = subjectMstRepo;
        }

        #region Child Subject Master
        public PartialViewResult LandingPage(string TemplateId, string SubjectId)
        {
            return PartialView(_childSubjectRepo.GetChildSubjectList(TemplateId, SubjectId, 0));
        }

        [EncryptedActionParameter]
        public ViewResult New(string TemplateId, string TempleteName, string SubjectId, string SubjectName)
        {
            Examination_ChildSubjectSetupModels model = new Examination_ChildSubjectSetupModels();
            model.ExamTemplateId = TemplateId;
            model.Exam_TemplateName = TempleteName;
            model.SubjectId = SubjectId;
            model.SubjectName = SubjectName;
            model.TransactionMode = "SAVE";
            return View(model);
        }
        [EncryptedActionParameter]
        public ViewResult Updation(string TemplateId, string SubjectId, string SubjectPropertyId)
        {
            Examination_ChildSubjectSetupModels model = _childSubjectRepo.GetChildSubjectById(TemplateId, SubjectId, Convert.ToInt32(SubjectPropertyId));
            if (model != null)
                model.TransactionMode = "UPDATE";
            return View(model);
        }
        [HttpPost]
        public JsonResult Examination_ChildSubjectSetup_CRUD(Examination_ChildSubjectSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _childSubjectRepo.Examination_ChildSubjectSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Examination_ChildSubjectSetup_Delete(int PropertyId)
        {
            Tuple<int, string> res = _childSubjectRepo.Examination_ChildSubjectSetup_CRUD(PropertyId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}