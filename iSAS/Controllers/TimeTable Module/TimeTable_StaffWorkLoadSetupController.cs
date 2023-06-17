using ISas.Entities.TimeTable_Entities;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Repository.TimeTable_Repo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.TimeTable_Module
{
    [Authorize]
    [ExceptionHandler]
    public class TimeTable_StaffWorkLoadSetupController : Controller
    {
        private IStaff_StaffDetailMasterRepo _staffRepo;
        private ITimeTable_StaffWorkLoadSetupRepo _workloadSetupRepo;
        public TimeTable_StaffWorkLoadSetupController(IStaff_StaffDetailMasterRepo staffRepo, ITimeTable_StaffWorkLoadSetupRepo workloadSetup)
        {
            _staffRepo = staffRepo;
            _workloadSetupRepo = workloadSetup;
        }

        // GET: TimeTable_StaffWorkLoadSetup
        public ActionResult TeacherList()
        {
            return View(_staffRepo.GetTeacherList()); //.Where(r=> r.StaffType == "T")
        }

        #region Staff Class Setup
        public PartialViewResult _StaffClassSetup(string StaffId)
        {
            return PartialView(_workloadSetupRepo.StaffClassSetup_FormLoad(StaffId));
        }

        public JsonResult StaffClassSetup_CRUD(StaffClassSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _workloadSetupRepo.StaffClassSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Staff Subject Setup
        public PartialViewResult _StaffSubjectSetup(string StaffId)
        {
            StaffSubjectSetupModels model = new StaffSubjectSetupModels();
            model.ClassSecList = _workloadSetupRepo.StaffClassSetup_FormLoad(StaffId).ClassList.Where(r => r.Selected).ToList();
            model.StaffId = StaffId;
            return PartialView(model);
        }

        public PartialViewResult _SubjectList(string StaffId, string ClassSecId)
        {
            return PartialView(_workloadSetupRepo.StaffSubjectSetup_FormLoad(StaffId, ClassSecId));
        }

        public JsonResult StaffSubjectSetup_CRUD(StaffSubjectSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _workloadSetupRepo.StaffSubjectClassSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}