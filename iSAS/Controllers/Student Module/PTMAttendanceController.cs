using ISas.Repository.Interface;
using ISas.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using ISas.Web.Models;
using System;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class PTMAttendanceController : Controller
    {
        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        private IStudentAttendance _studentAttendanceRepos;
        private IPtmAttendance _ptmAttendanceRepos;

        public PTMAttendanceController(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession, IStudentAttendance studentAttendance,IPtmAttendance ptmAttendance)
        {
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
            this._studentAttendanceRepos = studentAttendance;
            this._ptmAttendanceRepos = ptmAttendance;
        }
        
        public ActionResult PTMAttendance()
        {
            var model = new  PTMAttendanceViewModel();
            var classes = this._studentClassRepos.GetAllClasses(Session["UserID"].ToString());

            model.ClassList = classes != null && classes.Any() ? classes.OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ClassName,
                    Value = p.ClassId
                }) : Enumerable.Empty<SelectListItem>();

            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId,
                    Selected = p.SessId == Session["SessionId"].ToString()
                }) : Enumerable.Empty<SelectListItem>();
            model.SelectedSessionId = Session["SessionId"].ToString();
            return View(model);
        }
        public JsonResult GetSectionsForClass(string classId)
        {
            var sections = this._studentSectionRepos.GetAllSections(classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAttendanceType()
        {
            var model = new PTMAttendanceViewModel();
            var ptmdate = this._ptmAttendanceRepos.GetPTMCategoryList();
            //model.PTMCategoryList = ptmdate;
            ptmdate = ptmdate != null & ptmdate.Any() ? ptmdate : null;
            return Json(ptmdate, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPTMDatesList(string classId, string userId, string SessionId, string categoryId)
        {
            var ptmdate = this._ptmAttendanceRepos.GetPTMDatesList(classId, Session["UserID"].ToString(), SessionId, categoryId);
            ptmdate = ptmdate != null & ptmdate.Any() ? ptmdate : null;
            return Json(ptmdate, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveClasstPTMAttendance(List<ISas.Entities.ClassPTMAttendance> ClassPTMAttendanceList, string attDate,string sessionId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("Student");
            dt.Columns.Add("Father");
            dt.Columns.Add("Mother");
            int item = ClassPTMAttendanceList.Count;
            for (int x = 0; x < item; x++)
            {
                DataRow row = dt.NewRow();
                row[0] = ClassPTMAttendanceList[x].StudentERPNo.Trim();
                row[1] = ClassPTMAttendanceList[x].Student.Trim();
                row[2] = ClassPTMAttendanceList[x].Father.Trim();
                row[3] = ClassPTMAttendanceList[x].Mother.Trim();
                dt.Rows.Add(row);
            }
            item = dt.Rows.Count;

            Tuple<int, string> res = _ptmAttendanceRepos.SaveClassPTMAttendance(dt, Session["UserID"].ToString(), attDate, sessionId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPTMAttandaceList(string sessionId, string classId, string sectionId, string attdate)
        {

            var model = new  PTMAttendanceViewModel();
            var students = this._ptmAttendanceRepos.GetPTMClassStudent(sessionId, classId, sectionId, Session["UserID"].ToString(), attdate);
            //var students = this._studentAttendanceRepos.GetClassStudent(sessionId, classId, sectionId, Session["UserID"].ToString(), attdate);
            model.StudentPTMAttendanceList = students.OrderBy(p => p.Student.StudentRollNo).ToList();//!=null&students.Any()? students.OrderBy(p=>p.Student.StudentRollNo):  new List<Entities.StudentAttendance>();
            var classteacher = this._ptmAttendanceRepos.GetClassTeacher();
            model.ClassTeacherName = classteacher;
            return PartialView("_StudentPTMAttendanceListPartial", model);
        }
    }
}
