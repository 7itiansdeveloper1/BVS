using ISas.Repository.Academic.IRepository;
using ISas.Repository.Interface;
using ISas.Repository.StudentRepository.IRepository;
using ISas.Web.Models;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ISas.Entities.Student_Entities;
using ISas.Entities.CommonEntities;

using CrystalDecisions.CrystalReports.Engine;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Student_IdentityCardController : Controller
    {
        // GET: Student_IdentityCard
        private IAcademic_ClassSetupRepo _classReo;
        private IStudent_IdentityCardRepo _identityCardRepo;
        private ICommonRepo _commonRepo;
        public Student_IdentityCardController(IAcademic_ClassSetupRepo classReo, IStudent_IdentityCardRepo identityCardRepo, ICommonRepo commonRepo)
        {
            _classReo = classReo;
            _identityCardRepo = identityCardRepo;
            _commonRepo = commonRepo;
        }
        public ViewResult Student_IdentityCard_Filter()
        {
            ViewBag.ClassSecList = _classReo.All_ClassWithSectionList_DropDown();
            return View();
        }


        public PartialViewResult _IdentityCard_StudentDetails(string ClassSectionIds, string ReportName)
        {
            Student_IdentityCardModel model = new Student_IdentityCardModel();
            //return PartialView(new Student_IdentityCardModel { GenAdmitCard = ReportName == "ICardReport" ? false : true, StudentDetails = _identityCardRepo.Get_IdentityCard_StudentDetails(ClassSectionIds, ReportName, Session["SessionId"].ToString()) });
            model.StudentDetails = _identityCardRepo.Get_IdentityCard_StudentDetails(ClassSectionIds, ReportName, Session["SessionId"].ToString());
            model.rptname = ReportName;
            return PartialView(model);
            //return PartialView(new Student_IdentityCardModel { rptname= ReportName, StudentDetails = _identityCardRepo.Get_IdentityCard_StudentDetails(ClassSectionIds, ReportName, Session["SessionId"].ToString()) });
        }

        [HttpPost]
        public ActionResult Student_IdentityCard(Student_IdentityCardModel model, string rptname)
        {
            model.StudentDetails = model.StudentDetails.Where(r => r.Selected).ToList();
            model.HeaderDetails = _commonRepo.ReportHeaderDetails("");
            //if (model.GenAdmitCard)
            //return View("Student_AdmitCard", model);

            List<ReportHeaderEntities> reportHeader = new List<ReportHeaderEntities>();
            reportHeader.Add(model.HeaderDetails);
            DataSet studDetails =  ToDataSet.ConvertToDataSet(model.StudentDetails,"Student_IdentityCard");
            studDetails.Tables.Add(ToDataSet.NewTable("ClientInfo", reportHeader));
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), rptname + ".rpt"));
            rd.SetDataSource(studDetails);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf");
        }

        public ActionResult Student_AdmitCard(Student_IdentityCardModel model)
        {
            return View(model);
        }

    }
}