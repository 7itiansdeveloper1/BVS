using System.Linq;
using System.Web.Mvc;
using ISas.Repository.Interface;
using ISas.Web.ViewModels;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using ISas.Web.Models;
using System;

namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_ProcessingController : Controller
    {
        private IStudentSession _studentSessionRepos;
        private IExam _studentClassRepos;
        private IExam _studentSectionRepos;
        private IExam _examRepos;
        private IExam_ReportCard _examReportCardRepos;

        public Exam_ProcessingController(IStudentSession studentSession, IExam exam, IExam_ReportCard examReportCard)
        {
            this._studentClassRepos = exam;
            this._studentSectionRepos = exam;
            this._studentSessionRepos = studentSession;
            this._examRepos = exam;
            this._examReportCardRepos = examReportCard;
        }

        public ActionResult Exam_ReportCardLoad()
        {
            var model = new Exam_ReportCardViewModel();
            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            var exam = this._examRepos.GetExamNameListForMR(Session["SessionId"].ToString(), Session["UserId"].ToString());
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
            //var model = new Exam_ReportCardViewModel();
            ViewBag.SelectedExamId = examId;
            var classes = this._examRepos.GetClassList(examId, Session["UserID"].ToString());

            classes = classes != null & classes.Any() ? classes.OrderBy(p => p.PrintOrder) : null;
            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindSectionddl(string examId, string classId)
        {
            //var model = new Exam_ReportCardViewModel();
            var sections = this._examRepos.GetSectionList(examId, classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult BindClassStudentsGrid(string sessionId, string classId, string sectionId, string examId)
        {
            var _exam_ReportCardViewModel = new Exam_ReportCardViewModel();
            _exam_ReportCardViewModel.SelectedSessionId = sessionId;
            _exam_ReportCardViewModel.SelectedClassId = classId;
            _exam_ReportCardViewModel.SelectedSectionId = sectionId;
            _exam_ReportCardViewModel.SelectedExamId = examId;
            var classstudentlist = this._examReportCardRepos.GetClassStudentList(sessionId, classId, sectionId, Session["UserID"].ToString());
            _exam_ReportCardViewModel.ClassStudentsList = classstudentlist.OrderBy(p => p.RollNo).ToList();
            return PartialView("_ClassStudentsListPartial", _exam_ReportCardViewModel);
        }

        [HttpPost]
        public JsonResult Processing_Class(Exam_ReportCardViewModel model)
        {
            string sessionId = model.SelectedSessionId; string classId = model.SelectedClassId;
            string sectionId = model.SelectedSectionId; string examId = model.SelectedExamId;
            string erpnos = string.Join(",", model.ClassStudentsList.Where(r => r.Select).Select(r => r.ERPNo));
            Tuple<int, string> res = _examReportCardRepos.Exam_Processing_Multi(classId, sectionId, sessionId, examId, erpnos);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult Processing_Student(string sessionId, string classId, string sectionId, string examId, string erpno)
        {
            Tuple<int, string> res = _examReportCardRepos.Exam_Processing_Single(classId, sectionId, sessionId, examId, erpno);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult StudentMarkRegister_Multi(Exam_ReportCardViewModel model)
        //{
        //    string sessionId = model.SelectedSessionId; string classId = model.SelectedClassId;
        //    string sectionId = model.SelectedSectionId; string examId = model.SelectedExamId;
        //    string erpno = string.Join(",", model.ClassStudentsList.Where(r => r.Select).Select(r => r.ERPNo)); string studentname = "";
        //    DataTable dt1 = this._examReportCardRepos.GradeCard_ReportTemplate1(classId, examId);
        //    DataSet _markRegister = new DataSet();
        //    string rptname = dt1.Rows[0][0].ToString();
        //    string _markregistername = "";
        //    studentname = "ReportCard_" + DateTime.Now.ToShortDateString() + ".pdf";
        //    if (rptname == "BVS_GradeCard1to2.rpt" || rptname == "BVS_GradeCard3to5.rpt")
        //    {
        //        _markregistername = "BVS_MarkRegister.rpt";
        //        _markRegister = _examReportCardRepos.MarkRegister_MarkRegister1to5(classId, sectionId, sessionId, erpno, examId);
        //        _markRegister.Tables[0].TableName = "BVS_ClientInfo";
        //        _markRegister.Tables[1].TableName = "BVS_StudentInformation";
        //        _markRegister.Tables[2].TableName = "BVS_MarkRegister";
        //        _markRegister.Tables[3].TableName = "BVS_MarkRegisterAttendance";
        //        _markRegister.Tables[4].TableName = "BVS_MarkRegisterRemarkTerm1";
        //        _markRegister.Tables[5].TableName = "BVS_MarkRegisterRemarkTerm2";
        //        _markRegister.Tables[6].TableName = "BVS_MarkRegisterRemarkTerm3";
        //    }
        //    else if (rptname == "BVS_GradeCard9to10.rpt")
        //    {
        //        _markregistername = "BVS_MR9to10.rpt";
        //        _markRegister = _examReportCardRepos.MarkRegister_MarkRegister9to10(classId, sectionId, sessionId, erpno, examId);
        //        _markRegister.Tables[0].TableName = "BVS_ClientInfo";
        //        _markRegister.Tables[1].TableName = "BVS_StudentInformation";
        //        _markRegister.Tables[2].TableName = "BVS_MR_Part1";
        //        _markRegister.Tables[3].TableName = "BVS_MR_Part1GK";
        //        _markRegister.Tables[4].TableName = "BVS_MR_Part1EVS";
        //        _markRegister.Tables[5].TableName = "BVS_MR_Part2";
        //        _markRegister.Tables[6].TableName = "BVS_MR_Part2_DISC";
        //        _markRegister.Tables[7].TableName = "BVS_MR_Term1_Remark";
        //        _markRegister.Tables[8].TableName = "BVS_MR_Term1_Attendance";
        //        _markRegister.Tables[9].TableName = "BVS_MR_Term2_Remark";
        //        _markRegister.Tables[10].TableName = "BVS_MR_Term2_Attendance";
        //    }
        //    else if (rptname == "BVS_GradeCard11to12.rpt")
        //    {
        //        _markregistername = "BVS_MR11to12.rpt";
        //        _markRegister = _examReportCardRepos.MarkRegister_MarkRegister11to12(classId, sectionId, sessionId, erpno, examId);
        //        _markRegister.Tables[0].TableName = "BVS_ClientInfo";
        //        _markRegister.Tables[1].TableName = "BVS_StudentInformation";
        //        _markRegister.Tables[2].TableName = "BVS_MR__11_12_PART1";
        //        _markRegister.Tables[3].TableName = "BVS_MR__11_12_PART2";
        //        _markRegister.Tables[4].TableName = "BVS_MR__11_12_PART3";
        //        _markRegister.Tables[5].TableName = "BVS_MR__11_12_PART4";
        //        _markRegister.Tables[6].TableName = "BVS_MR__11_12_Achivement";
        //        _markRegister.Tables[7].TableName = "BVS_MR__11_12_Attendance";
        //    }
        //    else
        //    {
        //        _markregistername = "BVS_MR6to8.rpt";
        //        _markRegister = _examReportCardRepos.MarkRegister_MarkRegister6to8(classId, sectionId, sessionId, erpno, examId);
        //        _markRegister.Tables[0].TableName = "BVS_ClientInfo";
        //        _markRegister.Tables[1].TableName = "BVS_StudentInformation";
        //        _markRegister.Tables[2].TableName = "BVS_MR_Term1_Part1";
        //        _markRegister.Tables[3].TableName = "BVS_MR_Term1_Part1GK";
        //        _markRegister.Tables[4].TableName = "BVS_MR_Term1_Part1EVS";
        //        _markRegister.Tables[5].TableName = "BVS_MR_Term1_Part2";
        //        _markRegister.Tables[6].TableName = "BVS_MR_Term1_Remark";
        //        _markRegister.Tables[7].TableName = "BVS_MR_Term1_Attendance";
        //        _markRegister.Tables[8].TableName = "BVS_MR_Term2_Part1";
        //        _markRegister.Tables[9].TableName = "BVS_MR_Term2_Part1GK";
        //        _markRegister.Tables[10].TableName = "BVS_MR_Term2_Part1EVS";
        //        _markRegister.Tables[11].TableName = "BVS_MR_Term2_Part2";
        //        _markRegister.Tables[12].TableName = "BVS_MR_Term2_Remark";
        //        _markRegister.Tables[13].TableName = "BVS_MR_Term2_Attendance";

        //    }
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), _markregistername));
        //    rd.SetDataSource(_markRegister);
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return File(stream, "application/pdf", studentname);
        //}

    }
}