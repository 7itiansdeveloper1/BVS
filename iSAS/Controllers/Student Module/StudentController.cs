using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Repository.TransportRepo.IRepository;
using ISas.Web.Models;
using ISas.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class StudentController : Controller
    {

        private IStudentSection _studentSectionRepos;
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        private IStudentUpdation _studentUpdationRepos;
        private IStudent_OptionalSubject _student_OptionalSubjectRepos;
        private ITransport_RouteMasterRepo _routeMstRepo;
        public StudentController(IStudentSection studentSection, IStudentClass studentClass, IStudentSession studentSession, IStudentUpdation studentUpdation, IStudent_OptionalSubject optionalsubject, ITransport_RouteMasterRepo routeMstRepo)
        {
            this._studentClassRepos = studentClass;
            this._studentSectionRepos = studentSection;
            this._studentSessionRepos = studentSession;
            this._studentUpdationRepos = studentUpdation;
            this._student_OptionalSubjectRepos = optionalsubject;
            _routeMstRepo = routeMstRepo;
        }

        public ActionResult AdmissionLanding()
        {
            var model = new ViewModels.TransferCertificateViewModel();
            //var clientlist = this._studentUpdationRepos.GetClientList();
            //clientlist = clientlist != null & clientlist.Any() ? clientlist : null;
            //return Json(clientlist, JsonRequestBehavior.AllowGet);
            return View(model);
        }

        //public ActionResult GetClientData()
        //{
        //    //var model = new ViewModels.TransferCertificateViewModel();
        //    ////var clientlist = this._studentUpdationRepos.GetClientList();
        //    //clientlist = clientlist != null & clientlist.Any() ? clientlist : null;
        //    //return Json(clientlist, JsonRequestBehavior.AllowGet);
        //    return View();
        //}

        public ActionResult NewAdmission()
        {
            var model = new ViewModels.TransferCertificateViewModel();
            return View(model);
        }

        public ActionResult Updation()
        {
            var model = new ViewModels.Student_UpdationViewModel();
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
            //var model = new ViewModels.TransferCertificateViewModel();
            //return View(model);
        }

        public JsonResult GetSectionsForClass(string classId)
        {
            var sections = this._studentSectionRepos.GetAllSections(classId, Session["UserID"].ToString());
            sections = sections != null & sections.Any() ? sections.OrderBy(p => p.PrintOrder) : null;
            return Json(sections, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OptionalSubject()
        {
            var model = new ViewModels.Student_OptionalSubjectViewModel();
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

        public JsonResult GetClassUpdationParametersList(string sessionId, string classId)
        {
            var parameterslist = this._studentUpdationRepos.GetUpdationParametersList(sessionId, classId, Session["UserID"].ToString());
            parameterslist = parameterslist != null & parameterslist.Any() ? parameterslist : null;
            return Json(parameterslist, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetClassofJoiningList(string sessionId, string classId, string sectionId, string mode,
            string Print, string SelectedClassSectionName)
        {
            Student_UpdationViewModel model = new Student_UpdationViewModel();
            var val = _studentUpdationRepos.GetClassofJoiningList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode);
            model.ClassofJoiningList = val.Item1.OrderBy(p => p.Student.StudentRollNo).ToList();
            model.Print = Print;
            model.ColumnType = val.Item2;
            model.SelectedParameterId = mode;
            model.UserRole = Session["DisplayTitle"].ToString();
            model.SelectedClassSectionName = SelectedClassSectionName;
            if (mode == "COJ")
            {
                model.Paramether1_DDList = _studentClassRepos.GetAllClasses().Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId
                }).ToList();
                //model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();
            }
            else if (mode == "Religion")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetReligionList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.ReligionName,
                    Value = r.ReligionId,
                    Selected = r.IsDefault
                }).ToList();
                //model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();
            }
            else if (mode == "Stream")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetStreamList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.StreamName,
                    Value = r.SteremId,
                    Selected = r.IsDefault
                }).ToList();
                //model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();
            }
            else if (mode == "Category")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetCategoryList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.CategoryName,
                    Value = r.CategoryId,
                    Selected = r.IsDefault
                }).ToList();
                //model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();
            }
            else if (mode == "House")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetHouseList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.HouseName,
                    Value = r.HouseId,
                }).ToList();
                //  model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();
            }
            else if (mode == "BGroup")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetBloodGroupList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.BloodGroupName,
                    Value = r.BloodGroupId,
                    Selected = r.IsDefault
                }).ToList();
                // model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();
            }

            else if (mode == "Club")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetClubList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).ToList();
            }

            else if (mode == "MProfession" || mode == "FProfession")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetProfessionList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.ProfessionName,
                    Value = r.ProfessionId,
                }).ToList();
                // model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();
            }

            else if (mode == "Section")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetClassSectionList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).ToList();
            }
            else if (mode == "Snacks")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetSnacksList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).ToList();
            }
            else if (mode == "FeeDefaulter")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetDefaulterList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).ToList();
            }
            else if (mode == "MOT")
            {
                model.Paramether1_DDList = _studentUpdationRepos.GetModeofTransportList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.ModeofTransportName,
                    Value = r.ModeofTransportId,
                }).ToList();
                //   model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();

                model.Paramether2_DDList = _studentUpdationRepos.GetPickedUpByList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                {
                    Text = r.PickedUpBy,
                    Value = r.PickedUpId,
                }).ToList();
                model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD2TextVal = model.Paramether2_DDList.Where(x => x.Value == r.Parameter2).FirstOrDefault() == null ? "" : model.Paramether2_DDList.Where(x => x.Value == r.Parameter2).FirstOrDefault().Text; return r; }).ToList();
            }

            else if (mode == "BUSStop")
            {
                model.Paramether1_DDList = _routeMstRepo.getRouteMstDropDownList();
                //model.Paramether2_DDList = _studentUpdationRepos.GetPickedUpByList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode).Select(r => new SelectListItem
                //{
                //    Text = r.PickedUpBy,
                //    Value = r.PickedUpId,
                //}).ToList();
               // model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD2TextVal = model.Paramether2_DDList.Where(x => x.Value == r.Parameter2).FirstOrDefault() == null ? "" : model.Paramether2_DDList.Where(x => x.Value == r.Parameter2).FirstOrDefault().Text; return r; }).ToList();
            }

            if (model.ClassofJoiningList != null && model.ClassofJoiningList.Count > 0)
                model.ClassofJoiningList = model.ClassofJoiningList.Select(r => { r.DD1TextVal = model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault() == null ? "" : model.Paramether1_DDList.Where(x => x.Value == r.Parameter1).FirstOrDefault().Text; return r; }).ToList();

            ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Images/System/loginBgleft.jpg"))));
            return PartialView("_StudentClassofJoiningListPartial", model);
        }


        public JsonResult GetOptionalSubjectParametersList(string sessionId, string classId)
        {
            var parameterslist = this._student_OptionalSubjectRepos.GetOptionalSubjectParametersList(sessionId, classId, Session["UserID"].ToString());
            parameterslist = parameterslist != null & parameterslist.Any() ? parameterslist : null;
            return Json(parameterslist, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetStudentList(string sessionId, string classId, string sectionId, string mode)
        {
            var model = new ViewModels.Student_OptionalSubjectViewModel();
            Tuple<List<StudentList>, List<SelectListItem>> res = this._student_OptionalSubjectRepos.GetStudentList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode);
            model.StudentList = res.Item1.OrderBy(p => p.Student.StudentRollNo).ToList();
            model.SelectedParameterId = mode;
            model.OptionalSubjectList = res.Item2;
            //if (mode == "3rdLanguage")
            //{
            //var subject = this._student_OptionalSubjectRepos.GetOptionalSubjectList(sessionId, classId, sectionId, Session["UserID"].ToString(), mode);
            //model.OptionalSubjectList = subject != null && subject.Any() ? subject
            //    .Select(p => new SelectListItem
            //    {
            //        Text = p.SubjectName,
            //        Value = p.SubjectId,
            //        //Selected = p.IsDefault
            //    }) : Enumerable.Empty<SelectListItem>();
            //}
            //else if (mode == "ArtEducation")
            //{
            //}
            return PartialView("_StudentListPartial", model);
        }


        [HttpPost]
        public JsonResult StudentUpdation_CRUD(Student_UpdationViewModel model)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("Parameter1");
            dt.Columns.Add("Parameter2");
            dt.Columns.Add("Parameter3");
            //int rowcount = StudentUpdateList.Count;
            if (model.ColumnType == "3C" || model.ColumnType == "2DD" || model.ColumnType == "2DDC") //mode == "Student" || mode == "Father" || mode == "Mother" || mode == "MOT"
            {
                for (int x = 0; x < model.ClassofJoiningList.Count; x++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = model.ClassofJoiningList[x].Student.StudentERPNo.Trim();
                    row[1] = model.ClassofJoiningList[x].Parameter1 != null ? model.ClassofJoiningList[x].Parameter1.Trim() : "";
                    row[2] = model.ClassofJoiningList[x].Parameter2 != null ? model.ClassofJoiningList[x].Parameter2.Trim() : "";
                    row[3] = model.ClassofJoiningList[x].Parameter3 != null ? model.ClassofJoiningList[x].Parameter3.Trim() : "";
                    dt.Rows.Add(row);
                }
            }
            else if (model.ColumnType == "1C" || model.ColumnType == "DD") //mode == "COJ" || mode == "Religion" || mode == "Category" || mode == "House" || mode == "BGroup" || mode == "MProfession" || mode == "FProfession"  || mode == "DOA" || mode == "DOB" || mode == "Gender" || mode == "MMobile" || mode == "MAadharNo" || mode == "FMobile" || mode == "FAadharNo" || mode == "StudentAadhar" || mode == "SMSNO" || mode == "FEmail" || mode == "Address"
            {
                for (int x = 0; x < model.ClassofJoiningList.Count; x++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = model.ClassofJoiningList[x].Student.StudentERPNo.Trim();
                    row[1] = model.ClassofJoiningList[x].Parameter1 != null ? model.ClassofJoiningList[x].Parameter1.Trim() : "";
                    row[2] = "";
                    row[3] = "";
                    dt.Rows.Add(row);
                }
            }
            Tuple<int, string> res = _studentUpdationRepos.StudentUpdation_CRUD(dt, Session["UserID"].ToString(), model.SelectedParameterId);
            //return Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Student_OptionalSubject_CRUD(List<ISas.Entities.Student_OptionalSubject_CRUD> StudentUpdateList, string sessionid, string mode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentERPNo");
            dt.Columns.Add("Parameter1");
            dt.Columns.Add("Parameter2");
            dt.Columns.Add("Parameter3");
            int rowcount = StudentUpdateList.Count;
            for (int x = 0; x < rowcount; x++)
            {
                DataRow row = dt.NewRow();
                row[0] = StudentUpdateList[x].StudentERPNo.Trim();
                row[1] = StudentUpdateList[x].Parameter1 != null ? StudentUpdateList[x].Parameter1.Trim() : "";
                row[2] = "";
                row[3] = "";
                dt.Rows.Add(row);
            }
            Tuple<int, string> res = _student_OptionalSubjectRepos.Student_OptionalSubject_CRUD(dt, Session["UserID"].ToString(), sessionid, mode);
            // return Json(result, JsonRequestBehavior.AllowGet);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
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
    }
}
