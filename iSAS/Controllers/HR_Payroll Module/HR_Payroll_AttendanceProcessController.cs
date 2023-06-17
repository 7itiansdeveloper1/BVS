using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Web.Mvc;

namespace ISas.Web.Controllers.HR_Payroll_Module
{
    [Authorize]
    [ExceptionHandler]
    public class HR_Payroll_AttendanceProcessController : Controller
    {
        private IHR_Payroll_AttendanceProcessRepo _attenProcess;
        private IHR_Payroll_LeaveRegisterRepo _leaveRegRepo;
        public HR_Payroll_AttendanceProcessController(IHR_Payroll_AttendanceProcessRepo attenProcess, IHR_Payroll_LeaveRegisterRepo leaveRegister)
        {
            _attenProcess = attenProcess;
            _leaveRegRepo = leaveRegister;
        }

        // GET: HR_Payroll_AttendanceProcess
        public ActionResult AttendanceProcess_Filter()
        {
            return View(_attenProcess.GetAttenProcessFormLoadDetails(Session["SessionId"].ToString()));
        }

        public PartialViewResult _AttendanceProcess_Details(string PayBandId, string MonthDate)
        {
            return PartialView(_attenProcess.GetMonthleyAttenList(PayBandId, MonthDate));
        }

        public PartialViewResult _ApplyLeave(string StaffID, string StaffName)
        {
            HR_Payroll_LeaveRegisterModels model = _leaveRegRepo.GetFormLoadDetails();
            if (model != null)
            {
                model.LeaveType = "AVAILED ON";
                model.FromDate = model.ToDate = DateTime.Now.ToShortDateString().Replace("-", "/");
                model.SelectedEmpID = StaffID;
                model.StaffName = StaffName;
                model.BalanceDays = 1;

                HR_Payroll_LeaveRegisterModels localLeave = _leaveRegRepo.GetLocalLeaveDetails(StaffID);
                if (localLeave != null)
                    model.LocalLeaveList = localLeave.LocalLeaveList;
            }
            return PartialView(model);
        }
    }
}