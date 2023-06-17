using ISas.Entities;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ISas.Repository.Interface;
using System.IO;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Student_ReportController : Controller
    {
        private IStudent_ReportRepo _studentReportRepo;
        private ICommonRepo _commonRepo;
        public Student_ReportController(IStudent_ReportRepo studentReport, ICommonRepo commonRepo)
        {
            _studentReportRepo = studentReport;
            _commonRepo = commonRepo;
        }

        // GET: Student_Report
        public ActionResult StudentReport_Filter()
        {
            //Session["StudentDetailReport"] = null;
            Student_ReportModel model = _studentReportRepo.GetStudentReport_FormLoad("Students", "Detail",Session["UserId"].ToString());
            model.ReportType = "Detail";
            model.OrderBy = "Name";
            model.ReportFilterType = "ClassWise";
            return View(model);
        }

        public JsonResult GetReportNameList(string ReportType,string userid)
        {
            return Json(_studentReportRepo.GetReportNameList(ReportType,Session["UserId"].ToString()), JsonRequestBehavior.AllowGet);
        }

        public ActionResult _StudentDetailReport(string ClassSectionId, string Reporttype, bool InActive,string OrderBy, string ReportName, string SelectedClassNames,
            string SelectedReportName, string WingId, string Print = "NO")
        {
            Student_ReportModel model = _studentReportRepo.GetStudentDetailReport(ClassSectionId, Reporttype, InActive, OrderBy, ReportName, WingId,Session["SessionID"].ToString());
            model.Print = Print;
            model.ReportName = SelectedReportName;
            model.ReportType = Reporttype;
            model.OrderBy = OrderBy;
            //if (Print == "YES")
            //{              
            //}
            return PartialView(model);
        }

        [HttpPost]
        [ActionName("Student_Report_Testing")]
        public ActionResult Student_Report_Crystal(Student_ReportModel model)
        {
            string filter1Value = model.hiddenClassSectionIds;
            //string filter2Value = "";
            //if (model.ReportFilter1 != null && model.ReportFilter1.Count() > 0)
            //    filter1Value = string.Join(",", model.ReportFilter1);

            //if (model.ReportFilter2 != null && model.ReportFilter2.Count() > 0)
            //    filter2Value = string.Join(",", model.ReportFilter2); ;

            DataSet reportConfig = _commonRepo.Get_ReportConfiguration("Students", model.ReportName);
            DataSet reportDetails = _studentReportRepo.GetStudentDetailReport_Crystal(model.ReportName, Session["SessionId"].ToString(), Session["UserId"].ToString(), filter1Value);

            if (reportConfig != null && reportConfig.Tables[1].Rows.Count > 0)
                for (int i = 0; i < reportConfig.Tables[1].Rows.Count; i++)
                    reportDetails.Tables[i].TableName = reportConfig.Tables[1].Rows[i]["DStblName"].ToString();

            string reportName = "";
            if (reportConfig != null && reportConfig.Tables[0].Rows.Count > 0)
                reportName = reportConfig.Tables[0].Rows[0]["RptName"].ToString();

            string imgPath = reportDetails.Tables[0].Rows[0]["Logo"].ToString();
            if (System.IO.File.Exists(Server.MapPath("~/" + imgPath + "")))
                reportDetails.Tables[0].Rows[0]["Logo"] = Server.MapPath("~/" + imgPath + "");
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), reportName));
            rd.SetDataSource(reportDetails);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf");
        }

    }
}