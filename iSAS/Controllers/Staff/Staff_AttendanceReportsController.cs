using ISas.Entities.StaffEntities;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Staff
{
    [Authorize]
    [ExceptionHandler]
    public class Staff_AttendanceReportsController : Controller
    {
        private IStaff_AttendanceReportsRepo _staffAttnRepo;

        public Staff_AttendanceReportsController(IStaff_AttendanceReportsRepo staffAttnRepo)
        {
            _staffAttnRepo = staffAttnRepo;
        }

        // GET: Staff_Reports
        public ActionResult Staff_AttendanceReports_Filter()
        {
            Staff_AttendanceReportsModels model = _staffAttnRepo.Staff_AttendanceReports_FormLoad();
            model.ReportType = "Detail";
            model.OrderBy = "Name";
            model.ReportFilterBy = "DEPT";
            model.FromDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.ToDate = model.FromDate;
            return View(model);
        }

        public PartialViewResult _Staff_AttendanceReports(string StaffIds, string Reporttype, bool InActive, string OrderBy, string ReportName,
            string SelectedReportName, string ReportFilterBy, string FromDate, string ToDate)
        {
            Staff_AttendanceReportsModels model = _staffAttnRepo.GetStaff_AttendanceReports(StaffIds, Reporttype, InActive, OrderBy, ReportName, ReportFilterBy, FromDate, ToDate);
            model.SelectedReportName = SelectedReportName;
            return PartialView(model);
        }
    }
}