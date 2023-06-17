using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Entities.Exam_Entities;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_TargetListController : Controller
    {
        //private IExam _examRepos;
        private IStudentSession _studentSessionRepos;
        private IExam_TargetListRepo _examTargetRepos;
        public Exam_TargetListController(IStudentSession studentSession, IExam_TargetListRepo examTargetRepos, ICommonRepo commonRepo)
        {
            this._studentSessionRepos = studentSession;
            _examTargetRepos = examTargetRepos;
        }

        public ActionResult Exam_TargetList()
        {
            Exam_TargetModels model = new Exam_TargetModels();
            model.SessionList = _studentSessionRepos.GetAllSessions().OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }).ToList();
            model.ClassSectionList = _examTargetRepos.TargetListXI_Transaction_GetClassList(Session["UserId"].ToString(), Session["SessionId"].ToString(),"", "");
            model.SessionId = Session["SessionId"].ToString();
            return View(model);
        }
        public JsonResult BindSubjectddl(string classSectionId)
        {
            var subjects = _examTargetRepos.TargetListXI_Transaction_GetSubjectList(Session["UserId"].ToString(), Session["SessionId"].ToString(), classSectionId, "");
            subjects = subjects != null & subjects.Any() ? subjects: null;
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult BindTargetList(string classSectionId,string subjectId,string subjectName,string className)
        {

            Exam_TargetModels model = _examTargetRepos.TargetListXI_Transaction_GetTargetList(Session["UserId"].ToString(), Session["SessionId"].ToString(), classSectionId, subjectId, subjectName, className);
            return PartialView(model);
        }

    }
}