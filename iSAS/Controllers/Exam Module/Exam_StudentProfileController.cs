using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_StudentProfileController : Controller
    {
        private IExam_StudentProfileRepo _examStudentProfileRepo;
        private IExam _examRepos;
        private IStudentSession _studentSessionRepos;
        public Exam_StudentProfileController(IExam_StudentProfileRepo studentProfileRepo, IExam examRepo, IStudentSession sessionRepo)
        {
            _examStudentProfileRepo = studentProfileRepo;
            _examRepos = examRepo;
            _studentSessionRepos = sessionRepo;
        }

        public ActionResult New()
        {
            Exam_StudentProfileModels model = new Exam_StudentProfileModels();
            model.SessionList = _studentSessionRepos.GetAllSessions().OrderByDescending(r => r.PrintOrder).Select(p => new SelectListItem
            {
                Text = p.SessionDisplayName,
                Value = p.SessId,
                Selected = p.IsDefault
            }).ToList();
            model.ExamList = _examRepos.GetExamNameList(Session["SessionId"].ToString(), Session["UserId"].ToString(),"ACTIVE").OrderBy(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.ExamName,
                    Value = p.ExamId
                }).ToList();

            model.SessionId = Session["SessionId"].ToString();
            model.ClassList = _examStudentProfileRepo.GetUserWiseClass(Session["UserID"].ToString());
            return View(model);
        }

        public JsonResult GetUserWiseSectionList(string ClassId)
        {
            return Json(_examStudentProfileRepo.GetUserWiseSection(Session["UserID"].ToString(), ClassId), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _StudentDetails(string SessionId, string ClassId, string SectionId, string ExamId)
        {
            Exam_StudentProfileModels model = new Exam_StudentProfileModels();
            model.StudentDetailList = _examStudentProfileRepo.Profile_Attendnace_Cascading(Session["UserID"].ToString()
               , SessionId, ClassId, SectionId, ExamId, "GetStudentList");

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Exam_StudentProfile_CRUD(Exam_StudentProfileModels model)
        {
            if (ModelState.IsValid)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ERPNo"));
                dt.Columns.Add(new DataColumn("OpenDay"));
                dt.Columns.Add(new DataColumn("Attendnace"));
                dt.Columns.Add(new DataColumn("Height"));
                dt.Columns.Add(new DataColumn("Weight"));

                for(int i=0;  i < model.StudentDetailList.Count; i++)
                {
                    DataRow row = dt.NewRow();
                    row[0] = model.StudentDetailList[i].ERPNo;
                    row[1] = model.StudentDetailList[i].OpenDay;
                    row[2] = model.StudentDetailList[i].Attendance;
                    row[3] = model.StudentDetailList[i].Height;
                    row[4] = model.StudentDetailList[i].Weight;

                    dt.Rows.Add(row);
                }

                string messege = "";
                messege = _examStudentProfileRepo.Profile_Attendnace_CRUD(Session["UserID"].ToString() , model.SessionId, model.ClassId, model.SectionId, model.ExamId, dt);

                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}