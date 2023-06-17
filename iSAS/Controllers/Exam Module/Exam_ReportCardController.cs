using System.Linq;
using System.Web.Mvc;
using ISas.Repository.Interface;
using ISas.Web.ViewModels;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using ISas.Web.Models;
using System;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_ReportCardController : Controller
    {
        private IStudentSession _studentSessionRepos;
        private IExam _studentClassRepos;
        private IExam _studentSectionRepos;
        private IExam _examRepos;
        private IExam_ReportCard _examReportCardRepos;

        public Exam_ReportCardController(IStudentSession studentSession, IExam exam, IExam_ReportCard examReportCard)
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
            var exam = this._examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ALL");
            model.ExamList = exam != null && exam.Any() ? exam.OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }) : Enumerable.Empty<SelectListItem>();

            model.SelectedSessionId = Session["SessionId"].ToString();
            return View(model);
        }

        public ActionResult Exam_RetestReportCardLoad()
        {
            var model = new Exam_ReportCardViewModel();
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
            return View(model);
        }

        public JsonResult BindClassddl(string examId)
        {
            //var model = new Exam_ReportCardViewModel();
            ViewBag.SelectedExamId = examId;
            var classes = this._examRepos.GetClassListForReportCard(examId, Session["UserID"].ToString(),Session["SessionId"].ToString());
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
        public PartialViewResult BindClassStudentsGrid(string sessionId, string classId, string sectionId, string examId,
            string SelectedExamName)
        {
            Exam_ReportCardViewModel _exam_ReportCardViewModel = new Exam_ReportCardViewModel();
            _exam_ReportCardViewModel.SelectedSessionId = sessionId;
            _exam_ReportCardViewModel.SelectedClassId = classId;
            _exam_ReportCardViewModel.SelectedSectionId = sectionId;
            _exam_ReportCardViewModel.SelectedExamId = examId;
            var classstudentlist = this._examReportCardRepos.GetClassStudentList(sessionId, classId, sectionId, Session["UserID"].ToString());
            _exam_ReportCardViewModel.ClassStudentsList = classstudentlist.OrderBy(p => p.RollNo).ToList();

            _exam_ReportCardViewModel.SelectedExamName = SelectedExamName;
            return PartialView("_ClassStudentsListPartial", _exam_ReportCardViewModel);
        }


        public PartialViewResult BindClassRetestStudentsGrid(string sessionId, string classId, string sectionId, string examId,
           string SelectedExamName)
        {
            Exam_ReportCardViewModel _exam_ReportCardViewModel = new Exam_ReportCardViewModel();
            _exam_ReportCardViewModel.SelectedSessionId = sessionId;
            _exam_ReportCardViewModel.SelectedClassId = classId;
            _exam_ReportCardViewModel.SelectedSectionId = sectionId;
            _exam_ReportCardViewModel.SelectedExamId = examId;
            var classstudentlist = this._examReportCardRepos.GetClassRetestStudentList(sessionId, classId, sectionId, Session["UserID"].ToString());
            _exam_ReportCardViewModel.ClassStudentsList = classstudentlist.OrderBy(p => p.RollNo).ToList();

            _exam_ReportCardViewModel.SelectedExamName = SelectedExamName;
            return PartialView("_ClassRetestStudentsListPartial", _exam_ReportCardViewModel);
        }
        [HttpPost]
        public ActionResult StudentReportCard_Multi(Exam_ReportCardViewModel model)
        {
            string sessionId = model.SelectedSessionId; string classId = model.SelectedClassId;
            string sectionId = model.SelectedSectionId; string examId = model.SelectedExamId;
            string erpno = string.Join(",", model.ClassStudentsList.Where(r => r.Select).Select(r => r.ERPNo));
            string studentname = "";

            DataSet _gradeCard6to8 = this._examReportCardRepos.GradeCard_ReportTemplate(classId, examId, sectionId, erpno, sessionId);
            string rptname = _gradeCard6to8.Tables[0].Rows[0][0].ToString();
            string _examName = model.SelectedExamName;
            string _resultdate = _gradeCard6to8.Tables[0].Rows[0][1].ToString(); ;
            string _reportwithheader = "";
            string _reportwithmark = "";
            studentname = "ReportCard_" + DateTime.Now.ToShortDateString() + ".pdf";

            //DataSet _gradeCard1to2 = _examRepos.GradeCard_GradeCard1to2(classId, sectionId, sessionId, erpno, examId);
            if (rptname == "BVS_GradeCard1to2.rpt" || rptname == "BVS_GradeCard3to5.rpt")
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";

                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard1";
                _gradeCard6to8.Tables[5].TableName = "BVS_Achievement";
                _gradeCard6to8.Tables[6].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[7].TableName = "BVS_Attendnace";
            }
            else if (rptname == "BVS_GradeCard6to8.rpt")
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            }
            else if (rptname == "BVS_GradeCard9to10.rpt" || rptname == "BVS_GradeCard9to10Term2.rpt")
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            }
            else
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard11to12_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard11to12_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GradeCard11to12_Part3";
                _gradeCard6to8.Tables[6].TableName = "BVS_GradeCard11to12_Part4";
                _gradeCard6to8.Tables[7].TableName = "BVS_Achievement";
                _gradeCard6to8.Tables[8].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[9].TableName = "BVS_Attendnace";

            }
            DataTable dt = new DataTable();
            dt.Columns.Add("reportwithheader");
            dt.Columns.Add("reportwithmark");
            dt.Columns.Add("ExamName");
            dt.Columns.Add("ResultDate");
            DataRow row = dt.NewRow();
            row["reportwithheader"] = _reportwithheader;
            row["reportwithmark"] = _reportwithmark;
            row["ExamName"] = _examName;
            row["ResultDate"] = _resultdate;
            dt.TableName = "ReportOption";
            dt.Rows.Add(row);
            _gradeCard6to8.Tables.Add(dt);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), rptname));
            rd.SetDataSource(_gradeCard6to8);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            GC.Collect();
            return File(stream, "application/pdf", studentname);

        }
        public ActionResult StudentReportCard(string sessionId, string classId, string sectionId, string examId, string erpno, string studentname,string SelectedExamName)
        {
            DataSet _gradeCard6to8 = this._examReportCardRepos.GradeCard_ReportTemplate(classId, examId, sectionId, erpno, sessionId);
            string rptname = _gradeCard6to8.Tables[0].Rows[0][0].ToString();
            string _examName = SelectedExamName;
            string _resultdate = _gradeCard6to8.Tables[0].Rows[0][1].ToString(); ;
            string _reportwithheader = "";
            string _reportwithmark = "";
            studentname = studentname + ".pdf";

            //DataSet _gradeCard1to2 = _examRepos.GradeCard_GradeCard1to2(classId, sectionId, sessionId, erpno, examId);
            if (rptname == "BVS_GradeCard1to2.rpt" || rptname == "BVS_GradeCard3to5.rpt")
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard1";
                _gradeCard6to8.Tables[5].TableName = "BVS_Achievement";
                _gradeCard6to8.Tables[6].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[7].TableName = "BVS_Attendnace";
            }
            else if (rptname == "BVS_GradeCard6to8.rpt")
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            }

            else if (rptname == "BVS_GradeCard9to10.rpt" || rptname == "BVS_GradeCard9to10Term2.rpt")
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            }

            else
            {
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard11to12_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard11to12_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GradeCard11to12_Part3";
                _gradeCard6to8.Tables[6].TableName = "BVS_GradeCard11to12_Part4";
                _gradeCard6to8.Tables[7].TableName = "BVS_Achievement";
                _gradeCard6to8.Tables[8].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[9].TableName = "BVS_Attendnace";

            }
            DataTable dt = new DataTable();
            dt.Columns.Add("reportwithheader");
            dt.Columns.Add("reportwithmark");
            dt.Columns.Add("ExamName");
            dt.Columns.Add("ResultDate");
            DataRow row = dt.NewRow();
            row["reportwithheader"] = _reportwithheader;
            row["reportwithmark"] = _reportwithmark;
            row["ExamName"] = _examName;
            row["ResultDate"] = _resultdate;
            dt.TableName = "ReportOption";
            dt.Rows.Add(row);
            _gradeCard6to8.Tables.Add(dt);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), rptname));
            rd.SetDataSource(_gradeCard6to8);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            GC.Collect();
            return File(stream, "application/pdf", studentname);
        }
        [HttpPost]
        public ActionResult StudentRetestReportCard_Multi(Exam_ReportCardViewModel model)
        {
            string sessionId = model.SelectedSessionId; string classId = model.SelectedClassId;
            string sectionId = model.SelectedSectionId; string examId = model.SelectedExamId;
            string erpno = string.Join(",", model.ClassStudentsList.Where(r => r.Select).Select(r => r.ERPNo));
            string studentname = "";

            DataSet _gradeCard6to8 = this._examReportCardRepos.GradeCard_ReportTemplate(classId, examId, sectionId, erpno, sessionId);
            //string rptname = _gradeCard6to8.Tables[0].Rows[0][0].ToString();
            string rptname = "BVS_GradeCard11to12_Retest.rpt";
            string _examName = model.SelectedExamName;
            string _resultdate = _gradeCard6to8.Tables[0].Rows[0][1].ToString(); ;
            string _reportwithheader = "";
            string _reportwithmark = "";
            studentname = "ReportCard_" + DateTime.Now.ToShortDateString() + ".pdf";

            
            //if (rptname == "BVS_GradeCard1to2.rpt" || rptname == "BVS_GradeCard3to5.rpt")
            //{
            //    _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
            //    _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";

            //    _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard";
            //    _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard1";
            //    _gradeCard6to8.Tables[5].TableName = "BVS_Achievement";
            //    _gradeCard6to8.Tables[6].TableName = "BVS_GeneralRemark";
            //    _gradeCard6to8.Tables[7].TableName = "BVS_Attendnace";
            //}
            //else if (rptname == "BVS_GradeCard6to8.rpt")
            //{
            //    _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
            //    _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
            //    _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
            //    _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
            //    _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
            //    _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            //}
            //else if (rptname == "BVS_GradeCard9to10.rpt" || rptname == "BVS_GradeCard9to10Term2.rpt")
            //{
            //    _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
            //    _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
            //    _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
            //    _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
            //    _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
            //    _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            //}
            //else
            //{
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard11to12_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard11to12_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GradeCard11to12_Part3";
                _gradeCard6to8.Tables[6].TableName = "BVS_GradeCard11to12_Part4";
                _gradeCard6to8.Tables[7].TableName = "BVS_Achievement";
                _gradeCard6to8.Tables[8].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[9].TableName = "BVS_Attendnace";

            //}
            DataTable dt = new DataTable();
            dt.Columns.Add("reportwithheader");
            dt.Columns.Add("reportwithmark");
            dt.Columns.Add("ExamName");
            dt.Columns.Add("ResultDate");
            DataRow row = dt.NewRow();
            row["reportwithheader"] = _reportwithheader;
            row["reportwithmark"] = _reportwithmark;
            row["ExamName"] = _examName;
            row["ResultDate"] = _resultdate;
            dt.TableName = "ReportOption";
            dt.Rows.Add(row);
            _gradeCard6to8.Tables.Add(dt);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), rptname));
            rd.SetDataSource(_gradeCard6to8);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            GC.Collect();
            return File(stream, "application/pdf", studentname);

        }

        public ActionResult StudentRetestReportCard(string sessionId, string classId, string sectionId, string examId, string erpno, string studentname, string SelectedExamName)
        {
            DataSet _gradeCard6to8 = this._examReportCardRepos.GradeCard_ReportTemplate(classId, examId, sectionId, erpno, sessionId);
            //string rptname = _gradeCard6to8.Tables[0].Rows[0][0].ToString();
            string rptname = "BVS_GradeCard11to12_Retest.rpt";
            string _examName = SelectedExamName;
            string _resultdate = _gradeCard6to8.Tables[0].Rows[0][1].ToString(); ;
            string _reportwithheader = "";
            string _reportwithmark = "";
            studentname = studentname + ".pdf";

            //if (rptname == "BVS_GradeCard1to2.rpt" || rptname == "BVS_GradeCard3to5.rpt")
            //{
            //    _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
            //    _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
            //    _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard";
            //    _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard1";
            //    _gradeCard6to8.Tables[5].TableName = "BVS_Achievement";
            //    _gradeCard6to8.Tables[6].TableName = "BVS_GeneralRemark";
            //    _gradeCard6to8.Tables[7].TableName = "BVS_Attendnace";
            //}
            //else if (rptname == "BVS_GradeCard6to8.rpt")
            //{
            //    _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
            //    _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
            //    _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
            //    _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
            //    _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
            //    _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            //}

            //else if (rptname == "BVS_GradeCard9to10.rpt" || rptname == "BVS_GradeCard9to10Term2.rpt")
            //{
            //    _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
            //    _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
            //    _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard6to8_Part1";
            //    _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard6to8_Part2";
            //    _gradeCard6to8.Tables[5].TableName = "BVS_GeneralRemark";
            //    _gradeCard6to8.Tables[6].TableName = "BVS_Attendnace";
            //}

            //else
            //{
                _gradeCard6to8.Tables[1].TableName = "BVS_ClientInfo";
                _gradeCard6to8.Tables[2].TableName = "BVS_StudentInformation";
                _gradeCard6to8.Tables[3].TableName = "BVS_GradeCard11to12_Part1";
                _gradeCard6to8.Tables[4].TableName = "BVS_GradeCard11to12_Part2";
                _gradeCard6to8.Tables[5].TableName = "BVS_GradeCard11to12_Part3";
                _gradeCard6to8.Tables[6].TableName = "BVS_GradeCard11to12_Part4";
                _gradeCard6to8.Tables[7].TableName = "BVS_Achievement";
                _gradeCard6to8.Tables[8].TableName = "BVS_GeneralRemark";
                _gradeCard6to8.Tables[9].TableName = "BVS_Attendnace";

            //}

            DataTable dt = new DataTable();
            dt.Columns.Add("reportwithheader");
            dt.Columns.Add("reportwithmark");
            dt.Columns.Add("ExamName");
            dt.Columns.Add("ResultDate");
            DataRow row = dt.NewRow();
            row["reportwithheader"] = _reportwithheader;
            row["reportwithmark"] = _reportwithmark;
            row["ExamName"] = _examName;
            row["ResultDate"] = _resultdate;
            dt.TableName = "ReportOption";
            dt.Rows.Add(row);
            _gradeCard6to8.Tables.Add(dt);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), rptname));
            rd.SetDataSource(_gradeCard6to8);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            rd.Close();
            rd.Dispose();
            GC.Collect();
            return File(stream, "application/pdf", studentname);
        }

        [HttpPost]
        public FileContentResult Download(string pageTitle, string viewName, object model)
        {
            //string htmlText = this.htmlViewRenderer.RenderViewToString(this, viewName, model);

            // Let the html be rendered into a PDF document through iTextSharp.
            byte[] buffer = null;

            // Return the PDF as a binary stream to the client.
            return File(buffer, "application/pdf", "file.pdf");
        }
    }
}
