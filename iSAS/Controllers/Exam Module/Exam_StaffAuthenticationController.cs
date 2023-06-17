using ISas.Entities.TimeTable_Entities;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Repository.TimeTable_Repo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ISas.Web.Controllers.TimeTable_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Exam_StaffAuthenticationController : Controller
    {
        private IStaff_StaffDetailMasterRepo _staffRepo;
        private ITimeTable_StaffWorkLoadSetupRepo _workloadSetupRepo;
        public Exam_StaffAuthenticationController(IStaff_StaffDetailMasterRepo staffRepo, ITimeTable_StaffWorkLoadSetupRepo workloadSetup)
        {
            _staffRepo = staffRepo;
            _workloadSetupRepo = workloadSetup;
        }

        public ActionResult TeacherList()
        {
          
            return View(_staffRepo.GetTeacherList());
        }

        #region Staff Class Setup
        public PartialViewResult _StaffClassSetup(string StaffId)
        {
            return PartialView("~/Views/TimeTable_StaffWorkLoadSetup/_StaffClassSetup", _workloadSetupRepo.StaffClassSetup_FormLoad(StaffId));
        }
        #endregion


        #region Exam Subject Setup
        public PartialViewResult _ExamSubjectSetup(string StaffId)
        {
            StaffSubjectSetupModels model = new StaffSubjectSetupModels();
            model.ClassSecList = _workloadSetupRepo.StaffClassSetup_FormLoad(StaffId).ClassList.Where(r => r.Selected).ToList();
            model.StaffId = StaffId;
            return PartialView(model);
        }

        public PartialViewResult _SubjectList(string StaffId, string ClassSecId)
        {
            return PartialView(_workloadSetupRepo.ExamSubjectSetup_FormLoad(StaffId, ClassSecId));
        }

        public JsonResult ExamSubjectSetup_CRUD(StaffSubjectSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.SessionId = Session["SessionId"].ToString();
                Tuple<int, string> res = _workloadSetupRepo.ExamSubjectClassSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}