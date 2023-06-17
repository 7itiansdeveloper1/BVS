using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using ISas.Repository.DashboardRepository.Repository;

namespace ISas.Web.Controllers.Dashboard
{
    [Authorize]
    public class CertificatesController : Controller
    {
        private ICertificateRepo _certificateRepo = new CertificateRepo();
        public ActionResult GetCertificate()
        {
            List<CertificateModels> cetificateList =new List<CertificateModels>();
            cetificateList = _certificateRepo.Dashboard_CertificatePath(Session["UserId"].ToString(),Session["SessionId"].ToString());
            return View(cetificateList);
        }
        public ActionResult GetCertifricateReport(string studentName,string certificateDate,string reportName )
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("student");
            dt.Columns.Add("certificateDate");
            DataRow row = dt.NewRow();
            row["certificateDate"] = certificateDate;
            row["student"] = studentName;
            dt.Rows.Add(row);
            DataSet _certificate = new DataSet();
            _certificate.Tables.Add(dt);
            _certificate.Tables[0].TableName = "Certificate";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), reportName));
            rd.SetDataSource(_certificate);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", studentName+".pdf");
        }
    }
}