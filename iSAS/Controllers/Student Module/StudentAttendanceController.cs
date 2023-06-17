using ISas.Repository.Interface;
using ISas.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using ISas.Web.Models;
using System.Configuration;
using ISas.Entities;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class StudentAttendanceController : Controller
    {
        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        private IStudentAttendance _studentAttendanceRepos;
        
        public StudentAttendanceController(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession,IStudentAttendance studentAttendance)
        {
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
            this._studentAttendanceRepos = studentAttendance;
        }
        //DailyAttendanceLandingViewModel model = new DailyAttendanceLandingViewModel();
        public ActionResult DailyAttendanceLanding()
        {
            var model = new DailyAttendanceLandingViewModel();
            var dailyattendancesummary = this._studentAttendanceRepos.GetDailyAttendanceSummary(Convert.ToString(Session["SessionId"]), DateTime.Today.Date);
            //model.DailyAttenanceSummaryList = dailyattendancesummary != null && dailyattendancesummary.Any() ? dailyattendancesummary.OrderBy(p => p.ClassPrintOrder).ThenBy(p => p.SectionPrintOrder).ToList();
            model.DailyAttenanceSummaryList = dailyattendancesummary.OrderBy(p => p.ClassPrintOrder).ThenBy(p => p.SectionPrintOrder).ToList();
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult DailyAttendance(string ClassId = null, string SectionId = null)
        {
            DailyAttendanceViewModel model = new DailyAttendanceViewModel();
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

            if (string.IsNullOrEmpty(ClassId))
            {
                model.SelectedClassId = ClassId;
                model.SelectedSectionId = SectionId;
                model.SectionList = _studentSectionRepos.GetAllSections(ClassId, Session["UserID"].ToString()).Select(r=> new SelectListItem {
                     Text = r.SecName,
                     Value = r.SecId
                }).ToList();
            }

            model.AttendanceDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.IsSendSMS = Convert.ToBoolean(ConfigurationManager.AppSettings["SendStudentAbsentees"].ToString());
            return View(model);
        }
        public JsonResult GetSectionsForClass(string classId)
        {
            var sections = this._studentSectionRepos.GetAllSections(classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveStudentAttendance(List<ISas.Entities.ClassAttendance> ClassAttendanceList,string attdate, bool IsSendSMS)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("StudentMorningAttendance");
            dt.Columns.Add("StudentAfternoonAttendance");
            dt.Columns.Add("StudentLeaveType");
            int item = ClassAttendanceList.Count;
            for (int x = 0; x < item;x++ )
            {
                DataRow row = dt.NewRow();
                row[0] = ClassAttendanceList[x].StudentERPNo.Trim();
                row[1] = ClassAttendanceList[x].StudentMorningAttendnace.Trim();
                row[2] = ClassAttendanceList[x].StudentAfternoonAttendnace.Trim();
                row[3] = ClassAttendanceList[x].StudentLeaveType.Trim();
                dt.Rows.Add(row);
            }
            item=dt.Rows.Count;
            Tuple<int, string> res = _studentAttendanceRepos.SaveClassAttendance(dt, Session["UserID"].ToString(), Convert.ToDateTime(Convert.ToDateTime(attdate).Date.ToString("dd/MM/yyyy")),Session["SessionId"].ToString(), IsSendSMS);
            //return Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteStudentAttendance(string classId,string sectionId,string attdate)
        {
            Tuple<int, string> res = _studentAttendanceRepos.DeleteClassAttendance(classId, Convert.ToDateTime(Convert.ToDateTime(attdate).Date.ToString("dd/MM/yyyy")), Session["SessionId"].ToString(),sectionId );
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetAttandaceList(string sessionId, string classId, string sectionId, string attdate)
        {
            DailyAttendanceViewModel model = new DailyAttendanceViewModel();
            Tuple<IEnumerable<StudentAttendance>, string, bool> result = this._studentAttendanceRepos.GetClassStudent(sessionId, classId, sectionId, Session["UserID"].ToString(),  Convert.ToDateTime( Convert.ToDateTime(attdate).Date.ToString("dd/MM/yyyy")));
            model.StudentAttendanceList = result.Item1.OrderBy(p => p.Student.StudentRollNo).ToList();//!=null&students.Any()? students.OrderBy(p=>p.Student.StudentRollNo):  new List<Entities.StudentAttendance>();
           // var classteacher =  //this._studentAttendanceRepos.GetClassTeacher(sessionId, classId, sectionId, Session["UserID"].ToString(), Convert.ToDateTime(Convert.ToDateTime(attdate).Date.ToString("dd/MM/yyyy")));
            model.ClassTeacherName = result.Item2;
            model.IsOffToday = result.Item3;
            return PartialView("_StudentAttendanceListPartial", model);
        }
    }
}
