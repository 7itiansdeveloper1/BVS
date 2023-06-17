using ISas.Web.ViewModels;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Repository.Interface;
using System.IO;
using ISas.Web.Models;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Student_PhotoUploadController : Controller
    {
        //
        // GET: /Student_PhotoUpload/

        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        private IStudentAttendance _studentAttendanceRepos;

        public Student_PhotoUploadController(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession
            , IStudentAttendance studentAttendance)
        {
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
            this._studentAttendanceRepos = studentAttendance;
        }
        public ActionResult PhotoUpload()
        {
            var model = new Student_PhotoUploadViewModel();
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

        public ActionResult PhotoViewUpload()
        {
            var model = new Student_PhotoUploadViewModel();
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

        public JsonResult GetSectionsForClass(string classId)
        {
            var sections = this._studentSectionRepos.GetAllSections(classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _GetClassPhotoList(string sessionId, string classId, string sectionId)
        {
            var model = new Student_PhotoUploadViewModel();
            var classphotolist = this._studentAttendanceRepos.GetClassPhotoList(sessionId, classId, sectionId, Session["UserID"].ToString());
            model.ClassPhotoList = classphotolist.OrderBy(p => p.Student.StudentRollNo).ToList();//!=null&students.Any()? students.OrderBy(p=>p.Student.StudentRollNo):  new List<Entities.StudentAttendance>();
            return PartialView("_ClassPhotoListPartial", model);
        }

        public ActionResult _ClassPhotoListPartial()
        {
            return View();
        }

        public PartialViewResult _GetClassPhotoViewList(string sessionId, string classId, string sectionId)
        {
            var model = new Student_PhotoUploadViewModel();
            var classphotolist = this._studentAttendanceRepos.GetClassPhotoList(sessionId, classId, sectionId, Session["UserID"].ToString());
            model.ClassPhotoList = classphotolist.OrderBy(p => p.Student.StudentRollNo).ToList();//!=null&students.Any()? students.OrderBy(p=>p.Student.StudentRollNo):  new List<Entities.StudentAttendance>();
            return PartialView("_ClassPhotoViewListPartial", model);
        }

        [HttpPost]
        public JsonResult UploadFile(string id, string StudentERPNo, string ForDocType)
        {
            string myFullFileName = "";
            string mySavetoFileName = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (file.ContentLength > 2100000)
                    {
                        return Json("File size is too big..! max file size is allowed is approx 2mb", JsonRequestBehavior.AllowGet);
                    }
                    if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg")
                    {
                        myFullFileName = Path.GetFileName(file.FileName);
                        myFullFileName = Guid.NewGuid() + myFullFileName;
                        mySavetoFileName = "Images/StudentIamges/" + myFullFileName;
                        myFullFileName = "~/Images/StudentIamges/" + myFullFileName;
                        file.SaveAs(Server.MapPath(myFullFileName));
                    }
                    else
                    {
                        return Json("Only supports jpg, png, gif, jpeg formats ..!", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            Tuple<int, string> res = _studentAttendanceRepos.UploadStudentPhoto(StudentERPNo, mySavetoFileName, ForDocType);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        //public string UploadStudentPhoto_CRUD(string refno, string imageurl, string reftype)
        //{
        //    Tuple<int, string> res = (refno, imageurl, reftype);
        //    return result;
        //}
    }
}
