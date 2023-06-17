using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_DynamicReportWizardController : Controller
    {
        private IAcademic_DynamicReportWizardRepo _reportWizard;
        public Academic_DynamicReportWizardController(IAcademic_DynamicReportWizardRepo reportWizard)
        {
            _reportWizard = reportWizard;
        }

        // GET: Academic_DynamicReportWizard
        public ActionResult LandingPage()
        {
            return View(_reportWizard.GetReportWizardList());
        }

        public ActionResult New()
        {
            Academic_DynamicReportWizardModels model = _reportWizard.NewReportData();
            model.ReportType = "Detail";
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string ReportId, string ModuleId)
        {
            return View(_reportWizard.GetReportDetailsbyId(ReportId, ModuleId));
        }

        public JsonResult GetReportFeildList(string ModuleId)
        {
            return Json(_reportWizard.ReportFeildList(ModuleId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_DynamicReportWizard_CRUD(Academic_DynamicReportWizardModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _reportWizard.Academic_DynamicReportWizard_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}