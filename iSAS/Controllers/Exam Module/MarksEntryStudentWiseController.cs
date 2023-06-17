using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class MarksEntryStudentWiseController : Controller
    {
        private IStudentSession _studentSessionRepos;
        private IExam _examRepos;
        private IMarksEntry_StudentWiseRepo _studentWiseMarksEntryRepo;
        private ICommonRepo _commonRepo;
        public MarksEntryStudentWiseController(IStudentSession studentSession, IExam exam, IMarksEntry_StudentWiseRepo studentWiseMarksEntry, ICommonRepo commonRepo)
        {
            this._studentSessionRepos = studentSession;
            this._examRepos = exam;
            this._studentWiseMarksEntryRepo = studentWiseMarksEntry;
            _commonRepo = commonRepo;
        }

        
        public ActionResult MarksEntry_StudentWise()
        {
            Session["StudentWiseMarksEntryDetails"] = null;
            MarksEntryStudentWiseModel model = new MarksEntryStudentWiseModel();
            model.SessionList = this._studentSessionRepos.GetAllSessions().OrderByDescending(r => r.PrintOrder).Select(p => new SelectListItem
            {
                Text = p.SessionDisplayName,
                Value = p.SessId
            }).ToList();

            model.SessionId = Session["SessionId"].ToString();

            model.ExamList = this._examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ACTIVE").OrderBy(r=> r.PrintOrder).Select(p => new SelectListItem
            {
                Text = p.ExamName,
                Value = p.ExamId
            }).ToList();
            return View(model);
        }

        
        public JsonResult BindSubjectddl(string examId, string classId, string sectionId, string sessionId)
        {
            Tuple<List<SelectListItem>, List<SelectListItem>>  marksWithStudentList = _studentWiseMarksEntryRepo.GetMainSubjectWithStudentList(Session["UserID"].ToString(), examId, classId, sectionId, sessionId);
            return Json(new {subejctList = marksWithStudentList.Item1, studentList = marksWithStudentList.Item2 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _StudentWiseMarkList(string examid, string classid, string sectioinid, string mainsubjectid, string sessionid, string erpno,
            string TermName, string ClassName, string SectionName, string MainSubjectName, string StudentName
            )
        {
            MarksEntryStudentWiseModel model = _studentWiseMarksEntryRepo.GetStudentWiseMarkList(Session["UserID"].ToString(), examid, classid, sectioinid, mainsubjectid, sessionid, erpno);

            model.TermName = TermName;model.ClassName = ClassName;model.SectionName = SectionName;
            model.MainSubjectName = MainSubjectName;model.StudentName = StudentName;

            Session["StudentWiseMarksEntryDetails"] = null;
            Session["StudentWiseMarksEntryDetails"] = model;
            return PartialView(model);
        }

        public ActionResult _StudentWiseMarkList_Print(bool IsConfirmation = false)
        {
            MarksEntryStudentWiseModel model = new MarksEntryStudentWiseModel();
            if (Session["StudentWiseMarksEntryDetails"] != null)
                model = Session["StudentWiseMarksEntryDetails"] as MarksEntryStudentWiseModel;

            if (IsConfirmation)
                return PartialView(model);

            return View(model);
        }


        [HttpPost]
        public JsonResult SendEmail()
        {
            MarksEntryStudentWiseModel model = new MarksEntryStudentWiseModel();
            if (Session["StudentWiseMarksEntryDetails"] == null)
                return Json(new { status = "failed", Msg = "No Data Available to send..!" }, JsonRequestBehavior.AllowGet);

            model = Session["StudentWiseMarksEntryDetails"] as MarksEntryStudentWiseModel;

            string htmlview = CommonController.RenderViewToString(this.ControllerContext, "_StudentWiseMarkList_Print", model);

            if( _commonRepo.SendEmail("shailendrakmr70@gmail.com", "Marks Entry Student Wise Details", htmlview, true, null))
                return Json(new { status = "success", Msg = "Email Send Successfully..!" }, JsonRequestBehavior.AllowGet);

            return Json(new { status = "failed", Msg = "Fail to send Email..!" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SubjectWiseMarksEntry_CRUD(MarksEntryStudentWiseModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = Session["UserID"].ToString();
                string messege = _studentWiseMarksEntryRepo.SubjectWiseMarksEntry_CRUD(model);
                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }

            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}
