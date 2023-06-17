using ISas.Entities.Examination_Entities;
using ISas.Repository.ExaminationRepository.IRepository;
using ISas.Web.Models;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Examination_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Examination_ReportCardController : Controller
    {
        private IExamination_ReportCardRepo _reportCardRepo;
        public Examination_ReportCardController(IExamination_ReportCardRepo reportCardRepo)
        {
            _reportCardRepo = reportCardRepo;
        }

        // GET: Examination_ReportCard
        public ViewResult Examination_ReportCard_Search()
        {
            return View(new Examination_ReportCardModels { ExamList = _reportCardRepo.Get_ReportCardDropDowns(Session["SessionId"].ToString(), Session["UserId"].ToString(), null, "FormLoad", null) });
        }
        public JsonResult GetReportCardDropDowns(string ClassId, string Mode, string ExamId)
        {
            return Json(_reportCardRepo.Get_ReportCardDropDowns(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, Mode, ExamId), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _ReportCard_StudentDetails(string ClassId, string SectionId, string ExamId)
        {
            return PartialView(_reportCardRepo.Get_ReportCardStudentDetails(Session["SessionId"].ToString(), Session["UserId"].ToString(), ClassId, SectionId, ExamId));
        }

        [HttpPost]
        public ActionResult ReportCard_POST(Examination_ReportCardModels parm)
        {
            TempData["Temp_ReportCardParam"] = parm;
            //parm.SessionId = Session["SessionId"].ToString();
            //Examination_ReportCard_HtmlPrintModel model = _reportCardRepo.Get_StudentReportDetails(parm);
            //return new ViewAsPdf("ReportCard_HtmlPrint", model) { FileName = "ReportCard.pdf" };
            return RedirectToAction("ReportCard_HtmlPrint");
        }

        public ViewResult ReportCard_HtmlPrint()
        {
            Examination_ReportCardModels parm = TempData["Temp_ReportCardParam"] as Examination_ReportCardModels;
            return View(_reportCardRepo.Get_StudentReportDetails(parm));
        }
    }
}