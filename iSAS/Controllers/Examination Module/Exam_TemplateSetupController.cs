using ISas.Entities.Exam_Entities;
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
    public class Exam_TemplateSetupController : Controller
    {
        private IExam_TemplateSetupRepo _examTempleteRepo;
        public Exam_TemplateSetupController(IExam_TemplateSetupRepo templeteRepo)
        {
            _examTempleteRepo = templeteRepo;
        }

        #region Templete Setup
        // GET: Exam_TemplateSetup
        public PartialViewResult LandingPage()
        {
            return PartialView(_examTempleteRepo.GetExamTempleteList(""));
        }

        public ViewResult New()
        {
            return View(new Exam_TemplateSetupModels());
        }

        [EncryptedActionParameter]
        public ViewResult Updation(string TemplateId)
        {
            return View(_examTempleteRepo.GetExamTempleteById(TemplateId));
        }

        [HttpPost]
        public JsonResult Exam_TemplateSetup_CRUD(Exam_TemplateSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.TransactionMode = "SAVE";
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _examTempleteRepo.Exam_TemplateSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Exam_TemplateSetup_Delete(string TempleteId)
        {
            Tuple<int, string> res = _examTempleteRepo.Exam_TemplateSetup_CRUD(TempleteId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        #endregion




        #region Class Setup
        public PartialViewResult _ClassSetupDetails(string Exam_TemplateId)
        {
            Exam_Template_ClassSetupModels model = new Exam_Template_ClassSetupModels();
            model.ClassList = _examTempleteRepo.TempleteClassSetupDetails(Exam_TemplateId);
            model.ExamTemplateId = Exam_TemplateId;
            return PartialView(model);
        }


        [HttpPost]
        public JsonResult TempleteClassSetup_CRUD(Exam_Template_ClassSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _examTempleteRepo.TempleteClassSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Grading Setup
        public PartialViewResult _GradingSetupDetails(string Exam_TemplateId)
        {
            return PartialView(_examTempleteRepo.TempleteGradingSetup_FormLoad(Exam_TemplateId));
        }


        [HttpPost]
        public JsonResult TempleteGradingSetup_CRUD(Exam_Template_GradingSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _examTempleteRepo.TempleteGradingSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}