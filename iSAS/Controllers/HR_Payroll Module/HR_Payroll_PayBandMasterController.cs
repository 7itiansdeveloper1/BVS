using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.HR_Payroll_Module
{
    [Authorize]
    [ExceptionHandler]
    public class HR_Payroll_PayBandMasterController : Controller
    {
        private IHR_Payroll_PayBandMasterRepo _payBandMaster;
        public HR_Payroll_PayBandMasterController(IHR_Payroll_PayBandMasterRepo payBandMaster)
        {
            _payBandMaster = payBandMaster;
        }

        public PartialViewResult LandingPage()
        {
            return PartialView(_payBandMaster.GetPayBandList(""));
        }

        public ActionResult New()
        {
            return View();
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string PayBandID)
        {
            return View(_payBandMaster.GetPayBandByID(PayBandID));
        }

        [HttpPost]
        public JsonResult HR_Payroll_PayBandMaster_CRUD(HR_Payroll_PayBandMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _payBandMaster.HR_Payroll_PayBandMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HR_Payroll_PayBandMaster_Delete(string PayBandID)
        {
            Tuple<int, string> res = _payBandMaster.HR_Payroll_PayBandMaster_CRUD(PayBandID);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}