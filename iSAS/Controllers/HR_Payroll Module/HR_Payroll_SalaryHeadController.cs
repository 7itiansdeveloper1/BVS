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
    public class HR_Payroll_SalaryHeadController : Controller
    {
        private IHR_Payroll_SalaryHeadRepo _salaryHeasMstRepo;
        public HR_Payroll_SalaryHeadController(IHR_Payroll_SalaryHeadRepo salaryHeasMstRepo)
        {
            _salaryHeasMstRepo = salaryHeasMstRepo;
        }

        public PartialViewResult LandingPage()
        {
            return PartialView(_salaryHeasMstRepo.GetSalaryHeadList(""));
        }

        public ActionResult New()
        {
            return View();
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string HeadID)
        {
            return View(_salaryHeasMstRepo.GetSalaryHeadByID(HeadID));
        }

        [HttpPost]
        public JsonResult HR_Payroll_SalaryHead_CRUD(HR_Payroll_SalaryHeadModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _salaryHeasMstRepo.HR_Payroll_SalaryHead_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HR_Payroll_SalaryHead_Delete(string HeadID)
        {
            Tuple<int, string> res = _salaryHeasMstRepo.HR_Payroll_SalaryHead_CRUD(HeadID);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}