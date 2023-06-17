using ISas.Entities.Student_Entities;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.StudentRepository.IRepository;
using ISas.Web.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Student_CertificateController : Controller
    {
        private IAcademic_SessionMasterRepo _sessionMasterRepo;
        private IAcademic_ClassSetupRepo _classSecREpo;
        private IStudent_CertificateRepo _certificateRepo;
        public Student_CertificateController(IAcademic_SessionMasterRepo sessionMasterRepo, IAcademic_ClassSetupRepo classSecREpo,
            IStudent_CertificateRepo certificateRepo
            )
        {
            _sessionMasterRepo = sessionMasterRepo;
            _classSecREpo = classSecREpo;
            _certificateRepo = certificateRepo;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return  View(_certificateRepo.getCertificateList(Session["SessionId"].ToString(), ""));
        }

        public ActionResult New()
        {
            Student_CertificateModels model = new Student_CertificateModels();
            model.SessionList = _sessionMasterRepo.getAllSessionWithDefaultSelected();
            model.ClassSecList = _classSecREpo.All_ClassList_DropDown();
            model.CertificateTypeList = _certificateRepo.getCertificateListDropDown();
            model.SessionId = model.SessionList.Where(r => r.Selected).FirstOrDefault().Value;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string studCertificateId)
        {
            Student_CertificateModels model = _certificateRepo.getCertificateById(Session["SessionId"].ToString(), studCertificateId);
            model.SessionList = _sessionMasterRepo.getAllSessionWithDefaultSelected();
            model.ClassSecList = _classSecREpo.All_ClassList_DropDown();
            model.CertificateTypeList = _certificateRepo.getCertificateListDropDown();
            return View(model);
        }

        [HttpPost]
        public JsonResult Student_Certificate_CRUD(Student_CertificateModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.Mode = "SAVE";
                Tuple<int, string> res = _certificateRepo.Student_Certificate_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Student_Certificate_CANCEL(string studCertificateId)
        {
            Tuple<int, string> res = _certificateRepo.Student_Certificate_CRUD(studCertificateId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        [EncryptedActionParameter]
        public ActionResult Student_CertificateDetails(string erpNo, string certificateId, string sessionId)
        {
            DataSet feeDetails = _certificateRepo.getStudentCertificateDetails(erpNo, certificateId, Session["UserId"].ToString(), sessionId );
            feeDetails.Tables[0].TableName = "ClientInfo";
            feeDetails.Tables[1].TableName = "StudentInformation";
            feeDetails.Tables[2].TableName = "Student_CertificateDetails";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), feeDetails.Tables[3].Rows[0]["RptName"].ToString()));
            rd.SetDataSource(feeDetails);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf");
        }
    }
}