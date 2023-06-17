using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Entities.Exam_Entities;
using ISas.Repository.Interface;
using ISas.Entities;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]

    public class Exam_ResultController : Controller
    {
        private IStudentSession _studentSessionRepos;
        //private IExam _studentClassRepos;
        //private IExam _studentSectionRepos;
        private IExam_Result _examResultRepos;
        //private IExam_ReportCard _examReportCardRepos;

        public Exam_ResultController(IStudentSession studentSession, IExam_Result examresult)
        {
            //this._studentClassRepos = exam;
            //this._studentSectionRepos = exam;
            this._studentSessionRepos = studentSession;
            this._examResultRepos = examresult;
            //this._examReportCardRepos = examReportCard;
        }


        public ActionResult Exam_ViewResult()
        {
            var model = new ResultViewModel();
            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            var exam = this._examResultRepos.GetExamList(Session["SessionId"].ToString(), Session["UserId"].ToString());
            model.ExamList = exam != null && exam.Any() ? exam.OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }) : Enumerable.Empty<SelectListItem>();

            model.SelectedSessionId = Session["SessionId"].ToString();
            return View(model);
        }
        public PartialViewResult _ReportCardView(string SessionId, string ExamId)
        {
            StudentGradeCardViewModel model = this._examResultRepos.GetStudentGardeCardView(ExamId, Session["UserId"].ToString(), SessionId);
            return PartialView(model);
        }
    }
}