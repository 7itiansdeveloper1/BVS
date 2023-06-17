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
    public class Exam_AchievementController : Controller
    {
        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        //private IStudentUpdation _studentUpdationRepos;
        private IExam _examRepos;
        private IExam_AchievementRepository _examAchievementRepos;
        //private IStudent_OptionalSubject _student_OptionalSubjectRepos;
        public Exam_AchievementController(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession, IExam exam,IExam_AchievementRepository examachievement)
        {
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
            this._examRepos = exam;
            this._examAchievementRepos = examachievement;
            //this._student_OptionalSubjectRepos = optionalsubject;
        }

        public ActionResult Exam_Achievement_Load()
        {
            var model = new ViewModels.Exam_AchievementViewModel ();
            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            var exam = this._examAchievementRepos.GetExamNameList(Session["UserID"].ToString());
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

        //public JsonResult BindStudentddl(string classId,string sectioinId)
        //{
        //    var students = this._examAchievementRepos.GetStudentList(Session["UserID"].ToString(),classId,sectioinId);
        //    students = students != null & students.Any() ? students.OrderBy(p => p.StudentRollNo) : null;
        //    return Json(students, JsonRequestBehavior.AllowGet);
        //}

        public PartialViewResult BindStudentAchievementGrid(string sessionId, string classId, string sectionId, string examId, 
            string TermName, string ClassName, string SectionName)
        {
            ViewModels.Exam_AchievementViewModel model = new ViewModels.Exam_AchievementViewModel();
            var achievementlist = this._examAchievementRepos.GetStudentAchievementList(sessionId, classId, sectionId, examId);
            model.StudentAchievementList = achievementlist.OrderBy(p => p.Student.StudentRollNo).ToList();

            model.TermName = TermName;
            model.ClassName = ClassName;
            model.SectionName = SectionName;


            Session["SessionStudentAchievements"] = null;
            Session["SessionStudentAchievements"] = model;
            return PartialView("_StudentAchievementListPartial", model);
        }


        public ActionResult StudentAchievement_Print()
        {
            ViewModels.Exam_AchievementViewModel model = new ViewModels.Exam_AchievementViewModel();
            if (Session["SessionStudentAchievements"] != null)
                model = Session["SessionStudentAchievements"] as ViewModels.Exam_AchievementViewModel;

            if (model != null && model.StudentAchievementList != null && model.StudentAchievementList.Count > 0)
                model.StudentAchievementList = model.StudentAchievementList.Where(r => !string.IsNullOrEmpty(r.Achievement)).ToList();

            return View(model);
        }



        [HttpPost]
        public JsonResult Exam_Achievement_CRUD(List<ISas.Entities.StudentAchievementTable> StudentAchievementList, string sessionId, string examId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("Achievement");
            dt.Columns.Add("P1");
            dt.Columns.Add("P2");
            int rowcount = StudentAchievementList.Count;
            for (int x = 0; x < rowcount; x++)
            {
                DataRow row = dt.NewRow();
                row[0] = StudentAchievementList[x].StudentERPNo.Trim();
                row[1] = StudentAchievementList[x].Achievement != null ? StudentAchievementList[x].Achievement.Trim() : "";
                row[2] = "";
                row[3] = "";
                dt.Rows.Add(row);
            }
            Tuple<int, string> res = _examAchievementRepos.Exam_Achievement_CRUD(dt, sessionId, examId, Session["UserID"].ToString());
            //eturn Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    
    }
}
