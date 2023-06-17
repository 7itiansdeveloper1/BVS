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
    public class HR_Payroll_SalaryRegisterController : Controller
    {
        private IHR_Payroll_SalaryRegisterRepo _salaryRegRepo;
        public HR_Payroll_SalaryRegisterController(IHR_Payroll_SalaryRegisterRepo salaryRegRepo)
        {
            _salaryRegRepo = salaryRegRepo;
        }

        // GET: HR_Payroll_AttendanceProcess
        public ActionResult SalaryRegister_Filter()
        {
            return View(_salaryRegRepo.GetSalaryRegisterFormLoadDetails(Session["SessionId"].ToString()));
        }

        public PartialViewResult _SalaryRegister_Details(string PayBandId, string MonthDate)
        {
            ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Images/System/loginBgleft.jpg"))));
            return PartialView(_salaryRegRepo.GetSalaryRegisterList(PayBandId, MonthDate));
        }

        [HttpPost]
        public JsonResult HR_Payroll_SalaryRegister_CRUD(HR_Payroll_SalaryRegisterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _salaryRegRepo.HR_Payroll_SalaryRegister_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}