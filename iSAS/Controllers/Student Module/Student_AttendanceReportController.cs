using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Student_AttendanceReportController : Controller
    {
        private IStudent_AttendanceReportRepo _studAttnRepo;

        public Student_AttendanceReportController(IStudent_AttendanceReportRepo studAttnRepo)
        {
            _studAttnRepo = studAttnRepo;
        }

        // GET: Student_AttendanceReport
        public ActionResult Student_AttendanceReport_Filter()
        {
            Student_AttendanceReportModels model = _studAttnRepo.Student_AttendanceReport_FormLoad("Student Attendance", "Detail");
            model.ReportType = "Detail";
            model.OrderBy = "Name";
            model.FromDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.ToDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            return View(model);
        }

        public PartialViewResult _Student_AttendanceReport(string fromdate, string todate, string ClassSectionId, string InActive,
           string ReportName, string Print = "NO")
        {
            Student_AttendanceReportModels model = _studAttnRepo.Student_AttendanceReport(fromdate, todate, ClassSectionId, InActive, ReportName, Session["SessionID"].ToString(), "");
            model.Print = Print;
            return PartialView(model);
        }


        //public PartialViewResult _Student_AttendanceDetailReport(string fromdate, string todate, string ClassSectionId, bool InActive,
        //string ReportName)
        //{
        //    Student_AttendanceReportModels model = _studAttnRepo.Student_AttendanceDetailReport(fromdate, todate, ClassSectionId, InActive, ReportName, Session["SessionID"].ToString(), "");

        //    ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Images/System/loginBgleft.jpg"))));
        //    return PartialView(model);
        //}
    }
}