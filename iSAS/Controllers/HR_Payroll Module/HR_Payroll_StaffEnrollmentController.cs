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
    public class HR_Payroll_StaffEnrollmentController : Controller
    {
        private IHR_Payroll_StaffEnrollmentRepo _staffEnrolRepo;
        public HR_Payroll_StaffEnrollmentController(IHR_Payroll_StaffEnrollmentRepo staffEnrolRepo)
        {
            _staffEnrolRepo = staffEnrolRepo;
        }

        [EncryptedActionParameter]
        public ActionResult LandingPage(string PayBandID, string PayBandName)
        {
            return View(_staffEnrolRepo.GetStaffEnrollmentDetails(PayBandID, PayBandName));
        }

        [HttpPost]
        public JsonResult HR_Payroll_StaffEnrollment_CRUD(HR_Payroll_StaffEnrollmentModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _staffEnrolRepo.HR_Payroll_StaffEnrollment_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HR_Payroll_StaffEnrollment_Delete(string PayBandID, string StaffID)
        {
            Tuple<int, string> res = _staffEnrolRepo.HR_Payroll_StaffEnrollment_CRUD(PayBandID, StaffID);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}