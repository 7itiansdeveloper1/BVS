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
    public class Examination_RemarksCenterController : Controller
    {
        private IExamination_RemarksCenterRepo _remarkscenterRepo;
        public Examination_RemarksCenterController(IExamination_RemarksCenterRepo remarkscenterRepo)
        {
            _remarkscenterRepo = remarkscenterRepo;
        }
        #region Remarks Templete
        public PartialViewResult _LandingPage_RemarksTemplete()
        {
            return PartialView(_remarkscenterRepo.GetRemarksTempleteList(null));
        }

        public ViewResult New_RemarksTemplete()
        {
            return View(new Examination_RemarksTempleteModels { IsRemarkTemplate = true });
        }

        public PartialViewResult _Edit_RemarksTemplete(string TempleteId)
        {
            return PartialView(_remarkscenterRepo.GetRemarksTempleteById(TempleteId));
        }

        [HttpPost]
        public JsonResult RemarksTemplete_CRUD(Examination_RemarksTempleteModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _remarkscenterRepo.Examination_RemarksTemplete_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Class Steup
        public PartialViewResult _RemarksTemplete_ClassSetup(string TempleteId)
        {
            Examination_TempleteClassSetupModel model = new Examination_TempleteClassSetupModel();
            model.RemarksTempleteId = TempleteId;
            model.ClassList = _remarkscenterRepo.GetRemarksTempleteClassList(TempleteId);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult RemarksTemplete_ClassSetup_CRUD(Examination_TempleteClassSetupModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _remarkscenterRepo.RemarksTempleteClass_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Remarks Center Templete
        public PartialViewResult _LandingPage_RemarksCenter(string TempleteId)
        {
            return PartialView(_remarkscenterRepo.GetRemarksCenterList(TempleteId, null));
        }

        [EncryptedActionParameter]
        public ViewResult New_RemarksCenter(string TempleteId, string TempleteName)
        {
            return View(new Examination_RemarksCenterModels { TempleteName = TempleteName, IsActive = true, TempleteId = TempleteId });
        }

        public PartialViewResult _Edit_RemarksCenter(string TempleteId, string RemarksId)
        {
            return PartialView(_remarkscenterRepo.GetRemarksCenterById(TempleteId, RemarksId));
        }

        [HttpPost]
        public JsonResult RemarksCenter_CRUD(Examination_RemarksCenterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _remarkscenterRepo.Examination_RemarksCenter_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}