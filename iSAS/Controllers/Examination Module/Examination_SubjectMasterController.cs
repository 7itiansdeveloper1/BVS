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
    public class Examination_SubjectMasterController : Controller
    {
        private IExamination_SubjectMasterRepo _subjectMstRepo;
        public Examination_SubjectMasterController(IExamination_SubjectMasterRepo subjectMstRepo)
        {
            _subjectMstRepo = subjectMstRepo;
        }

        #region Subject Master
        public PartialViewResult LandingPage(string TemplateId)
        {
            return PartialView(_subjectMstRepo.GetSubjectMstList(TemplateId, ""));
        }

        [EncryptedActionParameter]
        public ViewResult New(string TemplateId, string TempleteName)
        {
            Examination_SubjectMasterModels model = _subjectMstRepo.SubjectMaster_FormLoad();
            if (model != null)
            {
                model.TemplateId = TemplateId;
                model.TempleteName = TempleteName;
                model.TransactionMode = "SAVE";
            }
            return View(model);
        }
        [EncryptedActionParameter]
        public ViewResult Updation(string TemplateId, string SubjectId)
        {
            Examination_SubjectMasterModels model = _subjectMstRepo.GetSubjectMstById(TemplateId, SubjectId);
            if (model != null)
                model.TransactionMode = "UPDATE";
            return View(model);
        }
        [HttpPost]
        public JsonResult Examination_SubjectMaster_CRUD(Examination_SubjectMasterModels model)
        {
            if (model.chkboxAddNewSubject)
            {
                if (string.IsNullOrEmpty(model.SubjectName))
                    ModelState.AddModelError("SubjectName", "Subject Name is Req..!");

                if (string.IsNullOrEmpty(model.SubjectDisplayName))
                    ModelState.AddModelError("SubjectDisplayName", "Display Name is Req..!");
            }

            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _subjectMstRepo.Examination_SubjectMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Examination_SubjectMaster_Delete(int PropertyId)
        {
            Tuple<int, string> res = _subjectMstRepo.Examination_SubjectMaster_CRUD(PropertyId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public PartialViewResult _SubjectPropertySetup(string PropertyId, char SubjectType)
        {
            Examination_SubjectPropertyModel model = _subjectMstRepo.GetSubjectSetupPropertyDetails(PropertyId);

            if (model != null)
                model.SubjectType = SubjectType;

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Examination_SubjectPropertySetup_CRUD(Examination_SubjectPropertyModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.TransactionMode = "SAVE";
                Tuple<int, string> res = _subjectMstRepo.Examination_SubjectPropertySetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}