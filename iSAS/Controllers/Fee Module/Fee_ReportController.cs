using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using ISas.Repository.Interface;
using System.IO;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_ReportController : Controller
    {
        private IFee_ReportRepo _feeReportRepo;
        private ICommonRepo _commonRepo;
        public Fee_ReportController(IFee_ReportRepo feeReportRepo, ICommonRepo commonRepo)
        {
            _feeReportRepo = feeReportRepo;
            _commonRepo = commonRepo;
        }
        // GET: Fee_Report

        [CustomAuthorizeFilter("NEW")]
        public ActionResult Fee_Report_Filter()
        {
            Fee_ReportModels model = _feeReportRepo.GetFee_Report_FormLoad("Fee", "Detail", Session["UserId"].ToString(), Session["SessionId"].ToString());
            model.FeeType = "FT";
            model.ReportType = "Detail";
            model.FromDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.ToDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            return View(model);
        }

        public ActionResult _Fee_ReportDetail(Fee_ReportModels model)
        {

            model.SessionId = Session["SessionID"].ToString();
            model = _feeReportRepo.GetStudentDetailReport(model);
            if (System.IO.File.Exists(Server.MapPath("~/" + model.ImageURL + "")))
            ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/" + model.ImageURL + ""))));
            return PartialView(model);
        }

        [HttpPost]
        [ActionName("Report_Crystal")]
        public ActionResult Student_Report_Crystal(Fee_ReportModels model
            //string FromDate , string  ToDate,
            //    string  ReportName, string ClassSectionIds, string SelectedFeeHeadsId, string selectedFeeHeadsId,
            //    string  FeeCategory, string FeeType, string FeeMode, string Print
            )
        {
            //Fee_ReportModels model = new Fee_ReportModels();
            model.SessionId = Session["SessionID"].ToString();
            //model.FromDate = FromDate;
            //model.ToDate = ToDate;
            //model.ReportName = ReportName;
            //model.ClassSectionIds = ClassSectionIds;
            //model.SelectedFeeHeadsId = selectedFeeHeadsId;
            //model.FeeCategory = FeeCategory;
            //model.FeeType = FeeType;
            //model.FeeMode = FeeMode;
            //model.Print = Print;

            DataSet reportConfig = _commonRepo.Get_ReportConfiguration("Fee", model.ReportName);
            DataSet reportDetails = _feeReportRepo.GetFeeReport_Crystal(model);

            if (reportConfig != null && reportConfig.Tables[1].Rows.Count > 0)
                for (int i = 0; i < reportConfig.Tables[1].Rows.Count; i++)
                    reportDetails.Tables[i].TableName = reportConfig.Tables[1].Rows[i]["DStblName"].ToString();

            string reportName = "";
            if (reportConfig != null && reportConfig.Tables[0].Rows.Count > 0)
                reportName = reportConfig.Tables[0].Rows[0]["RptName"].ToString();

            string imgPath = reportDetails.Tables[0].Rows[0]["LogoURL"].ToString();
            if (System.IO.File.Exists(Server.MapPath("~/" + imgPath + "")))
                reportDetails.Tables[0].Rows[0]["LogoURL"] = Server.MapPath("~/" + imgPath + "");

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), reportName));
            rd.SetDataSource(reportDetails);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf");

            //model.SessionId = Session["SessionID"].ToString();
            //DataSet reportConfig = _commonRepo.Get_ReportConfiguration("Fee", model.ReportName);
            //DataSet reportDetails = _feeReportRepo.GetFeeReport_Crystal(model);

            //if (reportConfig != null && reportConfig.Tables[1].Rows.Count > 0)
            //    for (int i = 0; i < reportConfig.Tables[1].Rows.Count; i++)
            //        reportDetails.Tables[i].TableName = reportConfig.Tables[1].Rows[i]["DStblName"].ToString();

            //string reportName = "";
            //if (reportConfig != null && reportConfig.Tables[0].Rows.Count > 0)
            //    reportName = reportConfig.Tables[0].Rows[0]["RptName"].ToString();

            //string imgPath = reportDetails.Tables[0].Rows[0]["LogoURL"].ToString();
            //if (System.IO.File.Exists(Server.MapPath("~/" + imgPath + "")))
            //    reportDetails.Tables[0].Rows[0]["LogoURL"] = Server.MapPath("~/" + imgPath + "");

            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), reportName));
            //rd.SetDataSource(reportDetails);
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();

            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/pdf");
        }

        //[CustomAuthorizeFilter("REPORT")]
        //public ActionResult _Fee_ReportDetail_PrintPage(Fee_ReportModels model)
        //{
        //    model.SessionId = Session["SessionID"].ToString();
        //    model = _feeReportRepo.GetStudentDetailReport(model);

        //    if (System.IO.File.Exists(Server.MapPath("~/" + model.ImageURL + "")))
        //        ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/" + model.ImageURL + ""))));
        //    return View(model);
        //}

    }
}