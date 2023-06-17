using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_MarksVerificationController : Controller
    {
        private IExam_MarksVerificationRepo _marksVerificationRepo;

        private IStudentSession _studentSessionRepos;
        private IExam _examRepos;
        public Exam_MarksVerificationController(IStudentSession studentSession, IExam examRepo, IExam_MarksVerificationRepo marksVerificationRepo)
        {
            _studentSessionRepos = studentSession;
            _examRepos = examRepo;
            _marksVerificationRepo = marksVerificationRepo;
        }

        // GET: Exam_MarksVerification
        public ActionResult MarksVerification_Filter()
        {
            //Session["Exam_MarksVerification1To5Result"] = null;
            Exam_MarksVerificationModels model = new Exam_MarksVerificationModels();
            model.SessionList = _studentSessionRepos.GetAllSessions().OrderByDescending(r => r.PrintOrder).Select(p => new SelectListItem
            {
                Text = p.SessionDisplayName,
                Value = p.SessId
            }).ToList();

            model.ExamList = _examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ALL").OrderBy(r => r.PrintOrder).Select(p => new SelectListItem
            {
                Text = p.ExamName,
                Value = p.ExamId
            }).ToList();

            model.SessionId = Session["SessionId"].ToString();
            return View(model);
        }

        public PartialViewResult _getStudentWiseMarksDetails(string sessionid, string classid, string sectionid, string erpno, string examid, string mainsubjectid,
            string TermName, string ClassName, string SectionName, string SubjectName, string StudentName)
        {
            Exam_MarksVerificationModels model = _marksVerificationRepo.GetStudentWiseMarksDetails(Session["UserID"].ToString(), sessionid, classid, sectionid, erpno, examid, mainsubjectid);
            model.TermName = TermName; model.ClassName = ClassName; model.SectionName = SectionName;
            model.SubjectName = SubjectName; model.StudentName = StudentName;

            //Session["Exam_MarksVerification1To5Result"] = model;
            return PartialView(model);
        }

        //public ActionResult StudentWiseMarksDetailsVerification_Print()
        //{
        //    Exam_MarksVerificationModels model = new Exam_MarksVerificationModels();

        //    if (Session["Exam_MarksVerification1To5Result"] != null)
        //        model = Session["Exam_MarksVerification1To5Result"] as Exam_MarksVerificationModels;

        //    return View(model);
        //}


        // GET: Exam_MarksVerification
        public ActionResult MarksVerification1_Filter()
        {
            Exam_MarksVerificationModels model = new Exam_MarksVerificationModels();
            model.ExamList = _marksVerificationRepo.GetDropDown_OnFormLoad(Session["SessionId"].ToString(), Session["UserId"].ToString());
            model.SubjectList.Add(new SelectListItem { Text = "ACADEMIC", Value = "ACADEMIC" });
            model.SubjectList.Add(new SelectListItem { Text = "NON-ACADEMIC", Value = "NON-ACADEMIC" });
            model.SubjectList.Add(new SelectListItem { Text = "SDOMAIN", Value = "SDOMAIN" });
            return View(model);
        }

        public JsonResult GetDropDownListByMode(Exam_MarksVerificationModels _params)
        {
            if(_params != null)
            {
                _params.SessionId = Session["SessionId"].ToString();
                _params.UserId = Session["UserId"].ToString();
            }
            return Json(_marksVerificationRepo.GetDropDownListByMode(_params), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _studentWiseMarksDetails(Exam_MarksVerificationModels _params)
        {
            if (_params != null)
            {
                _params.SessionId = Session["SessionId"].ToString();
                _params.UserId = Session["UserId"].ToString();
            }
            return PartialView(_marksVerificationRepo.StudentWiseMarksDetials(_params));
        }
    }
}