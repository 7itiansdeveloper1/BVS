using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.HR_Payroll_Module
{
    [Authorize]
    [ExceptionHandler]
    public class HR_Payroll_LeaveRegisterController : Controller
    {
        private IHR_Payroll_LeaveRegisterRepo _leaveRegRepo;
        public HR_Payroll_LeaveRegisterController(IHR_Payroll_LeaveRegisterRepo leaveRegRepo)
        {
            _leaveRegRepo = leaveRegRepo;
        }

        // GET: HR_Payroll_LeaveRegister
        public ActionResult LeaveRegister()
        {
            HR_Payroll_LeaveRegisterModels model = _leaveRegRepo.GetFormLoadDetails();
            if (model != null)
            {
                model.LeaveType = "AVAILED ON";
                model.FromDate = model.ToDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            }
            return View(model);
        }

        public PartialViewResult _LeaveDetails(string EmpID, string StaffName)
        {
            HR_Payroll_LeaveRegisterModels model = _leaveRegRepo.GetLeaveDetails(EmpID);
            if(model != null)
            {
                model.StaffCode = EmpID;
                model.StaffName = StaffName;
            }
            return PartialView(model);
        }

        public PartialViewResult _LocalLeave(string EmpID)
        {
            return PartialView(_leaveRegRepo.GetLocalLeaveDetails(EmpID));
        }


        public JsonResult GetAnnQuota(string LeaveID)
        {
            return Json(_leaveRegRepo.GetLeaveAnnualQuota(LeaveID), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult HR_Payroll_LeaveRegister_CRUD(HR_Payroll_LeaveRegisterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _leaveRegRepo.HR_Payroll_LeaveRegister_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

    }
}