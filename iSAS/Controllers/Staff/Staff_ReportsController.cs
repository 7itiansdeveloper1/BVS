using ISas.Entities.StaffEntities;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Staff
{
    [Authorize]
    [ExceptionHandler]
    public class Staff_ReportsController : Controller
    {
        private IStaff_ReportsRepo _staffReportRepo;

        public Staff_ReportsController(IStaff_ReportsRepo staffReportRepo)
        {
            _staffReportRepo = staffReportRepo;
        }

        // GET: Staff_Reports
        public ActionResult Staff_Reports_Filter()
        {
            Staff_ReportsModels model = _staffReportRepo.GetStaff_Reports_FormLoad();
            model.ReportType = "Detail";
            model.OrderBy = "Name";
            model.ReportFilterBy = "DEPT";
            return View(model);
        }

        public PartialViewResult _Staff_Reports(string StaffIds, string Reporttype, bool InActive, string OrderBy, string ReportName,
            string SelectedReportName, string ReportFilterBy)
        {
            //string tempStaffIds = string.Join(",", StaffIds);
            Staff_ReportsModels model = _staffReportRepo.GetStaff_Report(StaffIds, Reporttype, InActive, OrderBy, ReportName, ReportFilterBy);
            model.SelectedReportName = SelectedReportName;
            //ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Images/System/loginBgleft.jpg"))));
            return PartialView(model);
        }
    }
}