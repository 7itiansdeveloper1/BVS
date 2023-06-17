using System.Linq;
using System.Web.Mvc;
using ISas.Repository.Interface;
using ISas.Web.ViewModels;
using System.Data;
using ISas.Web.Models;
using ISas.Entities;
using System;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class AttendanceRegisterController : Controller
    {
        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        private IAttendanceRegister _attendanceRegisterRepos;
        public AttendanceRegisterController(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession, IStudentAttendance studentAttendance, IAttendanceRegister attendanceRegister)
        {
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
            this._attendanceRegisterRepos = attendanceRegister;
        }

        [HttpGet]
        public ActionResult AttendanceRegister()
        {
            var model = new AttendanceRegisterViewModel();
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
                    Value = p.SessId
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
        public JsonResult GetAttRegisterMonthList(string userId, string sessionId)
        {
            var attregdate = this._attendanceRegisterRepos.GetAttendanceRegisterMonthList(Session["UserID"].ToString(), sessionId, null, null);
            attregdate = attregdate != null & attregdate.Any() ? attregdate : null;
            return Json(attregdate, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult GetClassAttendanceRegister(string sessionId, string classId, string sectionId, string monthName)
        {
            var data = new AttendanceRegisterViewModel();
            data = _attendanceRegisterRepos.GetClassAttendanceRegister(sessionId, classId, sectionId, monthName);
            return PartialView("_StudentAttendanceRegisterListPartial", data);
        }
        public PartialViewResult _StudentAttendanceRegisterListPartial()
        {
            return PartialView();
        }


        #region New Attendance Register
        [HttpGet]
        public ActionResult AttendanceRegister_New()
        {
            var model = new AttendanceRegisterViewModel();
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
                    Value = p.SessId
                }) : Enumerable.Empty<SelectListItem>();
            return View(model);
        }


        public PartialViewResult _StudentAttenDetails(string sessionId, string classId, string sectionId, string monthName)
        {
            AttendanceRegisterViewModel model = new AttendanceRegisterViewModel();
            model.AttenDetails_New = _attendanceRegisterRepos.StudentAttenDetails(sessionId, classId, sectionId, monthName);

            if (model == null)
                model = new AttendanceRegisterViewModel();

            model.SelectedSessionId = sessionId;
            model.SelectedClassId = classId;
            model.SelectedSectionId = sectionId;
            model.SelectedAttRegDate = monthName;
            return PartialView(model);
        }

        [EncryptedActionParameter]
        public ViewResult _StudentAttendanceSummary(string sessionId, string classId, string sectionId, string monthName, string ERPNo)
        {
            StudentAttendanceDetailsModel model = _attendanceRegisterRepos.StudentAttenDetails(sessionId, classId, sectionId, monthName).Where(r => r.ERPNo == ERPNo).FirstOrDefault();
            if (model != null)
                model.AttenDate = monthName;

            return View(model);
        }

        #endregion
    }
}
