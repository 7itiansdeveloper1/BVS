using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_RemarkController : Controller
    {
        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        //private IStudentUpdation _studentUpdationRepos;
        private IExam _examRepos;
        private IExam_RemarkRepository _examRemarkRepos;
        //private IStudent_OptionalSubject _student_OptionalSubjectRepos;
        public Exam_RemarkController(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession, IExam exam, IExam_RemarkRepository examremark)
        {
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
            this._examRepos = exam;
            this._examRemarkRepos = examremark;
            //this._student_OptionalSubjectRepos = optionalsubject;
        }

        public ActionResult Exam_Remark_Load()
        {
            var model = new ViewModels.Exam_RemarkViewModel ();
            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            var exam = this._examRemarkRepos.GetExamNameList(Session["UserID"].ToString());
            model.ExamList = exam != null && exam.Any() ? exam.OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }) : Enumerable.Empty<SelectListItem>();

            model.SelectedSessionId = Session["SessionId"].ToString();
            return View(model);
        }

        public JsonResult BindClassddl(string examId)
        {
            //var classes = this._examRepos.GetClassList(examId, Session["UserID"].ToString());
            var classes = this._studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            classes = classes != null & classes.Any() ? classes.OrderBy(p => p.PrintOrder) : null;
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindSectionddl(string classId)
        {
            var sections = this._studentSectionRepos.GetAllSections(classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }
        
        public PartialViewResult BindStudentRemarkGrid(string sessionId, string classId, string sectionId, string examId,string mode, 
            string TermName, string ClassName, string SectionName )
        {
            ViewModels.Exam_RemarkViewModel model = new ViewModels.Exam_RemarkViewModel();
            model.TermName = TermName;
            model.ClassName = ClassName;
            model.SectionName = SectionName;

            var remarklist = this._examRemarkRepos.GetStudentRemarkList(sessionId, classId, sectionId, examId, mode);
            model.StudentRemarkList = remarklist.OrderBy(p => p.Student.StudentRollNo).ToList();
            var remark = this._examRemarkRepos.GetRemarkList(sessionId, classId, sectionId, examId);
            model.RemarkList = remark != null && remark.Any() ? remark
                .Select(p => new SelectListItem
                {
                    Text = p.RemarkText,
                    Value = p.RemarkId,
                    //Selected = p.IsDefault
                }) : Enumerable.Empty<SelectListItem>();
            model.SelectedRemarkOption = mode;

            Session["SessionStudentRemarkList"] = null;
            Session["SessionStudentRemarkList"] = model;
            return PartialView("_StudentRemarkListPartial", model);
        }

        public ActionResult StudentRemark_Print()
        {
            ViewModels.Exam_RemarkViewModel model = new ViewModels.Exam_RemarkViewModel();
            if(Session["SessionStudentRemarkList"] != null)
                model = Session["SessionStudentRemarkList"] as ViewModels.Exam_RemarkViewModel;

            return View(model);
        }


        [HttpPost]
        public JsonResult Exam_Remark_CRUD(List<ISas.Entities.StudentRemarkTable> StudentRemarkList, string sessionId, string examId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("RemarkId");
            dt.Columns.Add("P1");
            dt.Columns.Add("P2");
            int rowcount = StudentRemarkList.Count;
            for (int x = 0; x < rowcount; x++)
            {
                DataRow row = dt.NewRow();
                row[0] = StudentRemarkList[x].StudentERPNo.Trim();
                row[1] = StudentRemarkList[x].RemarkId != null ? StudentRemarkList[x].RemarkId : "-1";
                row[2] = "";
                row[3] = "";
                dt.Rows.Add(row);
            }
            Tuple<int, string> res = _examRemarkRepos.Exam_Remark_CRUD(dt, sessionId, examId, Session["UserID"].ToString());
            //return Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    
    }
}
