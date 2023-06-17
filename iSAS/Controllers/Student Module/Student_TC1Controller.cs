using ISas.Web.Models;
using ISas.Repository.Interface;
using System;
using System.Linq;
using System.Web.Mvc;
using ISas.Entities;
using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;


namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Student_TC1Controller : Controller
    {

        private IStudentTC _studentTCRepos;
        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;

        public Student_TC1Controller(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession, IStudentTC studentTCRepos)
        {
            this._studentTCRepos = studentTCRepos;
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
        }

        public ActionResult TCLanding()
        {
            return View(this._studentTCRepos.Student_TC1_LandingPage("").ToList());
        }

        public ActionResult NewTC()
        {
            Student_TC studenVIewModel = this._studentTCRepos.Student_TC1_FormLoad();
            studenVIewModel.DropDownList._tcSubjectList = this._studentTCRepos.GetTCSubjectList().ToList();
            studenVIewModel.Function = "SAVE";
            studenVIewModel.CreationDate = DateTime.Now.ToShortDateString().Replace("-", "/");

            studenVIewModel.Session = Session["SessionId"].ToString();
            return View(studenVIewModel);
        }

        [EncryptedActionParameter]
        public ActionResult EditTC(string TCID)
        {
            Student_TC model = _studentTCRepos.Student_TC1_LandingPage(TCID).FirstOrDefault();
            model.Function = "UPDATE";
            model.Student = model.ERPNo;
            var sessions = this._studentSessionRepos.GetAllSessions();
            if (sessions != null && sessions.Count() > 0)
                model.DropDownList.SessionList = sessions.OrderByDescending(p => p.PrintOrder).Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }).ToList();

            var classes = this._studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            if (classes != null && classes.Count() > 0)
                model.DropDownList.ClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId
                }).ToList();


            var section = this._studentSectionRepos.GetAllSections(model.ClassId, Session["UserID"].ToString());
            if (section != null && section.Count() > 0)
                model.DropDownList.SectionList = section.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.SecName,
                    Value = r.SecId
                }).ToList();
            var students = this._studentTCRepos.GetStudentListForTC(model.Session, model.ClassId, model.SectionId, Session["UserID"].ToString(), null);
            if (students != null && students.Count() > 0)
                model.DropDownList.StudentList = students.OrderBy(r => r.RollNo).Select(r => new SelectListItem
                {
                    Text = r.Student,
                    Value = r.ERPNo
                }).ToList();
            model.DropDownList._tcSubjectList = this._studentTCRepos.GetTCSubjectList(model.ERPNo).ToList();
            model.DropDownList.HigherClassOptionList.Add(new SelectListItem { Text = "NO", Value = "false", Selected = false });
            model.DropDownList.HigherClassOptionList.Add(new SelectListItem { Text = "YES", Value = "true", Selected = false });
            return View(model);
        }

        public JsonResult GetSectionsForClass(string classId)
        {
            var sections = this._studentSectionRepos.GetAllSections(classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetddlStudentList(string sessionId, string classId, string sectionId)
        {

            Student_TC model = new Student_TC();
            var students = this._studentTCRepos.GetStudentListForTC(sessionId, classId, sectionId, Session["UserID"].ToString(), null);
            //model.StudentList = students != null & students.Any() ? students.OrderBy(p => p.StudentRollNo).ToList() : null;
            students = students != null & students.Any() ? students.OrderBy(p => p.RollNo).ToList() : null;
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetddlStudentList1(string sessionId, string classId, string sectionId, string erpNo)
        {
            Student_TC students = _studentTCRepos.GetStudentListForTC(sessionId, classId, sectionId, Session["UserID"].ToString(), erpNo).FirstOrDefault();
            if (students == null)
                students = new Student_TC();
            //  var singlestudent = students.Where(x => x.ERPNo == erpNo).FirstOrDefault();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StudentTC_CRUD(Student_TC model)
        {
            if (model.DropDownList._tcSubjectList.Where(r => r.IsSelected == true).Count() == 0)
                ModelState.AddModelError("SubjectErrorMsg", "No subject selected");

            if (model.PendingDue > 0)
                ModelState.AddModelError("Student", "Student has pending dues..!");

            if (ModelState.IsValid)
            {
                Tuple<int, string> res = _studentTCRepos.StudentTC1_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NSOLanding()
        {
            var model = new ViewModels.TransferCertificateViewModel();
            return View(model);
        }

        public ActionResult NewNSO()
        {
            return View();
        }

        public ActionResult PrintTC(string erpno)
        {
            DataSet _tcCertificate = this._studentTCRepos.TC_Certificate1(erpno);
            string rptname = "";
            //if (_tcCertificate.Tables[1].Rows[0]["TCType"].ToString() == "SR")
            //    rptname = "Student_TCCertificate.rpt";
            //else
            //    rptname = "Student_TCCertificateJR.rpt";
            rptname = "Student_TCCertificate1.rpt";
            string filename = "";
            filename = erpno + ".pdf";
            _tcCertificate.Tables[0].TableName = "ClientInfo";
            _tcCertificate.Tables[1].TableName = "Student_TC_Certificate";
            _tcCertificate.Tables[2].TableName = "Student_TC_Subject";
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), rptname));
            rd.SetDataSource(_tcCertificate);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/pdf", filename);
            return File(stream, "application/pdf");





        }

        public JsonResult StudentTC_CancelTC(string TC_No, string ERPNo)
        {
            Tuple<int, string> res = _studentTCRepos.StudentTC1_CRUD(TC_No, Session["SessionId"].ToString(), ERPNo);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}