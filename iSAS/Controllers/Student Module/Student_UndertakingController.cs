using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using ISas.Repository.Interface;
using ISas.Repository.StudentRepository.IRepository;
using ISas.Web.Models;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Student_UndertakingController : Controller
    {
        // GET: Student_Undertaking

        private IStudent_CertificateRepo _studentCertificate;
        public Student_UndertakingController(IStudent_CertificateRepo studentCertificate)
        {
            _studentCertificate = studentCertificate;
        }
        public ActionResult New()
        {
            return View();
        }

        public ActionResult DownloadForm()
        {
            DataTable dt = this._studentCertificate.Student_Undertaking_DownloadForm(Session["UserId"].ToString(),Session["SessionId"].ToString());
            string rptname = "UndertakingForm.rpt";
            string _fileName = "UndertakingForm_"+ dt.Rows[0][1].ToString() + ".pdf";
            dt.TableName = "UnderTaking";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), rptname));
            rd.SetDataSource(dt);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            GC.Collect();
            return File(stream, "application/pdf", _fileName);
        }


    }
}