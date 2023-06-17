using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Repository.ExamRepository.Repository;
using ISas.Entities.Exam_Entities;

namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_ReportController : Controller
    {
        //private IStudentSession _studentSessionRepos;
        private IExam_ReportsRepo  _examReportsRepos;
        public Exam_ReportController (IExam_ReportsRepo examReportsRepos)
        {
            _examReportsRepos = examReportsRepos;
        }
        public ActionResult Exam_Report()
        {
            Exam_ReportModels model = new Exam_ReportModels();
            model.ReportNameList = _examReportsRepos.Exam_Report_Transaction_GetReportList(Session["SessionId"].ToString(),Session["UserId"].ToString());
            return View(model);
        }
        public JsonResult BindClassddl(string reportId)
        {
            var classList = _examReportsRepos.Exam_Report_Transaction_GetClassList( Session["SessionId"].ToString(), reportId, Session["UserId"].ToString() );
            classList = classList != null & classList.Any() ? classList : null;
            return Json(classList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindSubjectddl(string reportId,string classSectionId)
        {
            var subjects = _examReportsRepos.Exam_Report_Transaction_GetSubjectList(Session["SessionId"].ToString(), reportId,classSectionId, Session["UserId"].ToString());
            subjects = subjects != null & subjects.Any() ? subjects : null;
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _ExamDetailReport(string reportId, string classSectionId,string subjectId)
        {
            Exam_ReportModels model = new Exam_ReportModels();
            model = _examReportsRepos.GetReportData(Session["SessionId"].ToString(), reportId, classSectionId, subjectId, Session["UserId"].ToString());
            return PartialView(model);
        }

    }
}