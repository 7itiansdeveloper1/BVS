using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ISas.Repository.Interface;
using ISas.Web.ViewModels;
using System.Data;
using ISas.Web.Models;
using ISas.Entities;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class ExamController : Controller
    {
        private IStudentSession _studentSessionRepos;
        private IExam _studentClassRepos;
        private IExam _studentSectionRepos;
        private IExam _examRepos;
        private bool ismarked = true;
        private ICommonRepo _commonRepo;
        public ExamController(IStudentSession studentSession, IExam exam, ICommonRepo commonRepo)
        {
            this._studentClassRepos = exam;
            this._studentSectionRepos = exam;
            this._studentSessionRepos = studentSession;
            this._examRepos = exam;
            _commonRepo = commonRepo;
        }
        public ActionResult MarksEntry1to5()
        {
            var model = new MarksEntry1to5ViewModel();
            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            var exam = this._examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ACTIVE");
            model.ExamList = exam != null && exam.Any() ? exam.OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }) : Enumerable.Empty<SelectListItem>();

            model.SelectedSessionId = Session["SessionId"].ToString();
            Session["StudentMarksDetails"] = null;
            return View(model);
        }
        public ActionResult MarksEntryRetest()
        {
            var model = new MarksEntry1to5ViewModel();
            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            var exam = this._examRepos.GetExamNameList_Retest(Session["SessionId"].ToString(), Session["UserId"].ToString());
            model.ExamList = exam != null && exam.Any() ? exam.OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }) : Enumerable.Empty<SelectListItem>();

            model.SelectedSessionId = Session["SessionId"].ToString();
            Session["StudentMarksDetails"] = null;
            return View(model);
        }


        [HttpPost]
        public JsonResult SendEmail()
        {
            MarksEntry1to5ViewModel model = new MarksEntry1to5ViewModel();
            if (Session["StudentMarksDetails"] == null)
                return Json(new { status = "failed", Msg = "No Data Available to send..!" }, JsonRequestBehavior.AllowGet);

            model = Session["StudentMarksDetails"] as MarksEntry1to5ViewModel;

            string htmlview = CommonController.RenderViewToString(this.ControllerContext, "StudentMarkListPrintPage", model);

            if (_commonRepo.SendEmail("7itians@gmail.com", "Marks Entry Details", htmlview, true, null))
                return Json(new { status = "success", Msg = "Email Send Successfully..!" }, JsonRequestBehavior.AllowGet);

            return Json(new { status = "failed", Msg = "Fail to send Email..!" }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult BindClassddl(string examId)
        {
            var classes = this._examRepos.GetClassList(examId, Session["UserID"].ToString());
            classes = classes != null & classes.Any() ? classes.OrderBy(p => p.PrintOrder) : null;
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindClassddlForReportCard(string examId)
        {
            var classes = this._examRepos.GetClassListForReportCard(examId, Session["UserID"].ToString(),Session["SessionId"].ToString());
            classes = classes != null & classes.Any() ? classes.OrderBy(p => p.PrintOrder) : null;
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindSectionddl(string examId, string classId)
        {
            var sections = this._examRepos.GetSectionList(examId, classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindSubjectddl(string examId, string classId, string sectionId)
        {
            var subjects = this._examRepos.GetSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            //var subjects = this._examRepos.GetSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            subjects = subjects != null & subjects.Any() ? subjects.OrderBy(p => p.PrintOrder) : null;
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindAssessmentddl(string examId, string classId, string sectionId, string subjectId)
        {
            var assesments = this._examRepos.GetAssessmentList(examId, Session["UserID"].ToString(), classId, sectionId, subjectId);
            assesments = assesments != null & assesments.Any() ? assesments.OrderBy(p => p.PrintOrder) : null;
            return Json(assesments, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindGradesddl(string classId, string subjectId)
        {
            var grades = this._examRepos.GetGradeList(classId, subjectId);
            grades = grades != null & grades.Any() ? grades : null;
            return Json(grades, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult BindStudentMarkGrid(string examId, string classId, string sectionId, string sessionId, string subjectId, string assessmentId,
            string SessionName, string TermName, string ClassName, string SectionName, string SubjectName, string AssessmentName)
        {
            var model = new MarksEntry1to5ViewModel();
            model.SessionName = SessionName;
            model.TermName = TermName;
            model.ClassName = ClassName;
            model.SectionName = SectionName;
            model.SubjectName = SubjectName;
            model.AssessmentName = AssessmentName;
            
            DataTable dt = this._examRepos.MaxMarkConfiguration(examId, classId, sectionId,subjectId,assessmentId);
            
            model.MaxMark = dt.Rows[0][0].ToString(); ;
            model.Weightage = dt.Rows[0][1].ToString(); ;
            model.LastModifiedBy = dt.Rows[0][2].ToString();
            var subjecatcategory = this._examRepos.GetSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            ismarked = subjecatcategory.Where(u => u.SubjectId == subjectId).Select(x => x.IsMarkBased).SingleOrDefault();
            if (ismarked)
                model.SubjectCategory = "Marks";
            else
                model.SubjectCategory = "Grade";
            var studentmarkslist = this._examRepos.GetStudentMarkList(examId, Session["UserID"].ToString(), classId, sectionId, subjectId, assessmentId, sessionId);
            model.StudentsMarkList = studentmarkslist.OrderBy(p => p.Student.StudentRollNo).ToList();
            model.GradeList = this._examRepos.GetGradeList(classId, subjectId).Select(p => new SelectListItem
            {
                Text = p.GradingName,
                Value = p.GradingId
            }).ToList();

            Session["StudentMarksDetails"] = null;
            Session["StudentMarksDetails"] = model;
            return PartialView("_StudentMarkListPartial", model);
        }
        public PartialViewResult BindRetestStudentMarkGrid(string examId, string classId, string sectionId, string sessionId, string subjectId, string assessmentId,
            string SessionName, string TermName, string ClassName, string SectionName, string SubjectName, string AssessmentName)
        {
            var model = new MarksEntry1to5ViewModel();
            model.SessionName = SessionName;
            model.TermName = TermName;
            model.ClassName = ClassName;
            model.SectionName = SectionName;
            model.SubjectName = SubjectName;
            model.AssessmentName = AssessmentName;

            DataTable dt = this._examRepos.RetestMaxMarkConfiguration(examId, classId, sectionId, subjectId, assessmentId);

            model.MaxMark = dt.Rows[0][0].ToString(); ;
            model.Weightage = dt.Rows[0][1].ToString(); ;
            model.LastModifiedBy = dt.Rows[0][2].ToString();
            var subjecatcategory = this._examRepos.GetSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            ismarked = subjecatcategory.Where(u => u.SubjectId == subjectId).Select(x => x.IsMarkBased).SingleOrDefault();
            if (ismarked)
                model.SubjectCategory = "Marks";
            else
                model.SubjectCategory = "Grade";
            var studentmarkslist = this._examRepos.GetRetestStudentMarkList(examId, Session["UserID"].ToString(), classId, sectionId, subjectId, assessmentId, sessionId);
            model.StudentsMarkList = studentmarkslist.OrderBy(p => p.Student.StudentRollNo).ToList();
            model.GradeList = this._examRepos.GetGradeList(classId, subjectId).Select(p => new SelectListItem
            {
                Text = p.GradingName,
                Value = p.GradingId
            }).ToList();

            Session["StudentMarksDetails"] = null;
            Session["StudentMarksDetails"] = model;
            return PartialView("_RetestStudentMarkListPartial", model);
        }
        public ActionResult StudentMarkListPrintPage(bool IsConfirmation = false)
        {
            MarksEntry1to5ViewModel model = null;

            if (Session["StudentMarksDetails"] != null)
                model = Session["StudentMarksDetails"] as MarksEntry1to5ViewModel;

            if (model == null)
                model = new MarksEntry1to5ViewModel();

            if (IsConfirmation)
                return PartialView(model);

            return View(model);
        }


        [HttpPost]
        public JsonResult Student_MarksEntry_CRUD(MarksEntry1to5ViewModel model)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("isAbsent");
            dt.Columns.Add("iSML");
            dt.Columns.Add("isExempt");
            dt.Columns.Add("Mark");
            dt.Columns.Add("Grade");
            for (int x = 0; x < model.StudentsMarkList.Count; x++)
            {
                DataRow row = dt.NewRow();
                row[0] = model.StudentsMarkList[x].Student.StudentERPNo.Trim();
                row[1] = model.StudentsMarkList[x].StudentsMarkFromDBList.IsStudentAbsent;
                row[2] = model.StudentsMarkList[x].StudentsMarkFromDBList.IsStudentonML;
                row[3] = model.StudentsMarkList[x].StudentsMarkFromDBList.IsStudentExempt;
                row[4] = model.StudentsMarkList[x].StudentsMarkFromDBList.StudentMark ?? "";
                row[5] = model.StudentsMarkList[x].StudentsMarkFromDBList.StudentGrade ?? ""; //!= null ? StudentMarkList[x].StudentGrade.Trim() : "";
                dt.Rows.Add(row);
            }
            Tuple<int, string> res = this._examRepos.SaveClassMarks(dt, model.SelectedSessionId, model.SelectedExamId, Session["UserID"].ToString(), model.SelectedClassId, model.SelectedSectionId, model.SelectedSubjectId, model.SelectedAssessmentId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Student_MarksEntry_Retest_CRUD(MarksEntry1to5ViewModel model)
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("isAbsent");
            dt.Columns.Add("iSML");
            dt.Columns.Add("isExempt");
            dt.Columns.Add("Mark");
            dt.Columns.Add("Grade");
            for (int x = 0; x < model.StudentsMarkList.Count; x++)
            {
                if(model.StudentsMarkList[x].StudentsMarkFromDBList.IsRetestStudent)
                { 
                DataRow row = dt.NewRow();
                row[0] = model.StudentsMarkList[x].Student.StudentERPNo.Trim();
                row[1] = model.StudentsMarkList[x].StudentsMarkFromDBList.IsStudentAbsent;
                row[2] = model.StudentsMarkList[x].StudentsMarkFromDBList.IsStudentonML;
                row[3] = model.StudentsMarkList[x].StudentsMarkFromDBList.IsStudentExempt;
                row[4] = model.StudentsMarkList[x].StudentsMarkFromDBList.StudentMark ?? "";
                row[5] = model.StudentsMarkList[x].StudentsMarkFromDBList.StudentGrade ?? ""; //!= null ? StudentMarkList[x].StudentGrade.Trim() : "";
                dt.Rows.Add(row);
                }
            }
            Tuple<int, string> res = this._examRepos.SaveClassMarks_Retest(dt, model.SelectedSessionId, model.SelectedExamId, Session["UserID"].ToString(), model.SelectedClassId, model.SelectedSectionId, model.SelectedSubjectId, model.SelectedAssessmentId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RetestStudent_CRUD(string sessionId, string erpNo, string userId, bool value, string subjectId, string assessmentId)
        {
            Tuple<int, string> res = this._examRepos.RetestStudent_CRUD(sessionId, erpNo, Session["UserId"].ToString(), value, subjectId, assessmentId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        //-----------------------------------Co-Scholastic Marks Entry-----------------------------------------

        public ActionResult MarksEntryCoScholastic()
        {
            var model = new MarksEntryCoScholasticViewModel();

            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            var exam = this._examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ACTIVE");
            model.ExamList = exam != null && exam.Any() ? exam.OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }) : Enumerable.Empty<SelectListItem>();
            return View(model);
        }
        public JsonResult BindParentSubjectddl(string examId, string classId, string sectionId)
        {
            var subjects = this._examRepos.GetParentSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            //var subjects = this._examRepos.GetSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            subjects = subjects != null & subjects.Any() ? subjects.OrderBy(p => p.PrintOrder) : null;
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindStudentddl(string sessionId, string classId, string sectionId, string subjectId)
        {
            var classstudentlist = this._examRepos.GetClassStudentList(Session["UserID"].ToString(), classId, sectionId, subjectId, sessionId);
            //var subjects = this._examRepos.GetSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            classstudentlist = classstudentlist != null & classstudentlist.Any() ? classstudentlist.OrderBy(p => p.RollNo) : null;
            return Json(classstudentlist, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult BindStudentCoScholasticMarkList(string examId, string classId, string sectionId, string subjectId, string erpNo)
        {
            var model = new MarksEntryCoScholasticViewModel();
            var studentCoScholasticMarkList = this._examRepos.GetStudentCoScholasticMarkList(examId, Session["UserID"].ToString(), classId, sectionId, subjectId, erpNo);
            model.StudentsCoScholasticMarkList = studentCoScholasticMarkList.OrderBy(p => Convert.ToInt32(p.StudentCoScholsticMarkFromDBList.SubjectId)).ToList();
            return PartialView("_StudentMarkListCoScholasticPartial", model);
        }
        [HttpPost]
        public JsonResult Student_CoScholasticMarksEntry_CRUD(List<ISas.Entities.StudentCoScholasticMarkFromTableList> StudentCoScholasticMarkList, string sessionId, string examId, string classId, string sectionId, string erpNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SubjectId");
            dt.Columns.Add("Parameter1");
            dt.Columns.Add("Parameter2");
            dt.Columns.Add("Parameter3");
            int rowcount = StudentCoScholasticMarkList.Count;
            for (int x = 0; x < rowcount; x++)
            {
                DataRow row = dt.NewRow();
                row[0] = StudentCoScholasticMarkList[x].SubjectId.ToString();
                row[1] = StudentCoScholasticMarkList[x].Parameter1.ToString();
                row[2] = StudentCoScholasticMarkList[x].Parameter2.ToString();
                row[3] = StudentCoScholasticMarkList[x].Parameter3.ToString();
                dt.Rows.Add(row);
            }
            string result = this._examRepos.Student_CoScholasticMarksEntry_CRUD(dt, sessionId, examId, Session["UserID"].ToString(), classId, sectionId, erpNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------Co-Scholastic Marks Entry Version 1 -----------------------------------------

        public ActionResult MarksEntryCoScholastic1()
        {
            ActivityMarksEntryModel model = new ActivityMarksEntryModel();
            model.SessionList = this._studentSessionRepos.GetAllSessions().OrderBy(r => r.PrintOrder).Select(p => new SelectListItem
            {
                Text = p.SessionDisplayName,
                Value = p.SessId,
            }).ToList();

            model.ExamList = _examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ACTIVE").Select(p => new SelectListItem
            {
                Text = p.ExamName,
                Value = p.ExamId
            }).ToList();

            model.SelectedSessionId = Session["SessionId"].ToString();
            return View(model);
        }

        public JsonResult BindParentSubjectddl1(string examId, string classId, string sectionId)
        {
            var subjects = this._examRepos.GetParentSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            //var subjects = this._examRepos.GetSubjectList(examId, Session["UserID"].ToString(), classId, sectionId);
            subjects = subjects != null & subjects.Any() ? subjects.OrderBy(p => p.PrintOrder) : null;
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindAssessmentddl1(string examId, string classId, string sectionId, string subjectId)
        {
            var assesments = this._examRepos.GetAssessmentList(examId, Session["UserID"].ToString(), classId, sectionId, subjectId);
            assesments = assesments != null & assesments.Any() ? assesments.OrderBy(p => p.PrintOrder) : null;
            return Json(assesments, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult BindStudentActivityMarkList(string examId, string assessmentId, string classId, string sectionId, string subjectId, string erpNo,string sessionId)
        {
            ActivityMarksEntryModel model = new ActivityMarksEntryModel();

            DataSet ds = _examRepos.GetActivityHeaderList(examId, Session["UserID"].ToString(), classId, sectionId, subjectId, assessmentId);
            model.StudentsActivityMarkList = _examRepos.GetStudentActivityMarkList1(examId, assessmentId, Session["UserID"].ToString(), classId, sectionId, subjectId, sessionId).ToList();
            //model.StudentsActivityMarkList = new List<StudentActivityMarkList>(); //studentActivityMarkList.OrderBy(p => Convert.ToInt32(p.Student.StudentRollNo)).ToList();


            int rowCount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                model.HeaderList.Add(new HeaderList { SubjectName = ds.Tables[0].Rows[i][0].ToString() });
            }
            return PartialView("_StudentActivityCoScholasticPartial", model);
        }

        [HttpPost]
        public JsonResult Student_CoScholasticMarksEntry_CRUD1(ActivityMarksEntryModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                Tuple<int, string> res = _examRepos.Student_ActivityMark_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///*************************** Marks Verification******************************
        /// </summary>
        /// <returns></returns>

        public ActionResult Exam_MarksVerification()
        {
            //Session["Exam_MarksVerification"] = null;
            Exam_MarksVerificationModel model = new Exam_MarksVerificationModel();
            model.SessionList = _studentSessionRepos.GetAllSessions().OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }).ToList();


            model.ExamList = _examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ALL").OrderBy(p => p.PrintOrder).Select(p => new SelectListItem
            {
                Text = p.ExamName,
                Value = p.ExamId
            }).ToList();

            model.SelectedSessionId = Session["SessionId"].ToString();
            return View(model);
        }
        public PartialViewResult BindMarksVerificationList(string sessionId, string classId, string sectionId, string examId, string subjectId,
            string SelectedTermName, string SelectedClassName, string SelectedSectionName, string SelectedSubjectName)
        {
            //DataSet ds = this._examRepos.GetActivityHeaderList(examId, Session["UserID"].ToString(), classId, sectionId, subjectId, assessmentId);
            Exam_MarksVerificationModel model = _examRepos.GetClassVerificationList(sessionId, classId, sectionId, examId, subjectId);
            //var classVerificationList = this._examRepos.GetClassVerificationList(sessionId, classId, sectionId, examId, subjectId);
            //model.ClassVerificationList = classVerificationList.OrderBy(p => Convert.ToInt32(p.RollNo)).ToList();

            model.SelectedTermName = SelectedTermName; model.SelectedClassName = SelectedClassName;
            model.SelectedSectionName = SelectedSectionName; model.SelectedSubjectName = SelectedSubjectName;

            //Session["Exam_MarksVerification"] = model;
            return PartialView("_Exam_MarksVerificationListPartial", model);
        }

        //public ActionResult Exam_MarksVerification_PrintPage()
        //{
        //    Exam_MarksVerificationModel model = new Exam_MarksVerificationModel();
        //    if (Session["Exam_MarksVerification"] != null)
        //        model = Session["Exam_MarksVerification"] as Exam_MarksVerificationModel;
        //    return View(model);
        //}

        public ActionResult Exam_MarksVerificationCoScholastic()
        {
           // Session["Exam_MarksVerificationCoScholastic"] = null;
            Exam_MarksVerificationCoScholasticModels model = new Exam_MarksVerificationCoScholasticModels();
            model.SessionList = _studentSessionRepos.GetAllSessions().OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }).ToList();
            model.ExamList = _examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ALL").OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }).ToList();

            model.SelectedSessionId = Session["SessionId"].ToString();
            return View(model);
        }
        public PartialViewResult BindCoScholasticMarksVerificationList(string sessionId, string classId, string sectionId, string examId, string subjectId,
            string SelectedTermName, string SelectedClassName, string SelectedSectionName, string SelectedSubjectName)
        {
            Exam_MarksVerificationCoScholasticModels model = _examRepos.GetClassVerificationCoScholasticList(sessionId, classId, sectionId, examId, subjectId);
            //var classCoScholasticVerificationList = this._examRepos.GetClassVerificationCoScholasticList(sessionId, classId, sectionId, examId, subjectId);
            //model.ClassVerificationCoScholasticList = classCoScholasticVerificationList.OrderBy(p => Convert.ToInt32(p.RollNo)).ToList();

            model.SelectedTermName = SelectedTermName; model.SelectedClassName = SelectedClassName;
            model.SelectedSectionName = SelectedSectionName; model.SelectedSubjectName = SelectedSubjectName;

            //Session["Exam_MarksVerificationCoScholastic"] = model;
            return PartialView("_Exam_MarksVerificationCoScholasticListPartial", model);
        }

        //public ActionResult Exam_MarksVerificationCoScholastic_PrintPage()
        //{
        //    Exam_MarksVerificationCoScholasticModels model = new Exam_MarksVerificationCoScholasticModels();
        //    if (Session["Exam_MarksVerificationCoScholastic"] != null)
        //        model = Session["Exam_MarksVerificationCoScholastic"] as Exam_MarksVerificationCoScholasticModels;
        //    return View(model);
        //}
    }
}
