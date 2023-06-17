using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_SectionSetupController : Controller
    {
        private IAcademic_SectionSetupRepo _sectionSetupRepo;
        private IAcademic_SectionMasterRepo _sectionMstRepo;
        public Academic_SectionSetupController(IAcademic_SectionSetupRepo sectionSetup, IAcademic_SectionMasterRepo sectionMst)
        {
            _sectionSetupRepo = sectionSetup;
            _sectionMstRepo = sectionMst;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage(int Class_Strength, string Class_Name, string Class_ClassId)
        {
            ViewBag.teacherList = _sectionSetupRepo.GetTeacherList();
            return PartialView(_sectionSetupRepo.GetSectionSetupDetailsList(Class_Strength, Class_Name, Class_ClassId, ""));
        }

        [EncryptedActionParameter]
        public ActionResult New(string Class_Strength, string Class_Name, string Class_ClassId,
            string WingName, string WingId, string SchoolId, string SchoolName)
        {
            Academic_SectionSetupModels model = new Academic_SectionSetupModels();
            model.Class_Strength = Convert.ToInt32(Class_Strength);
            model.Class_Name = Class_Name;
            model.Class_ClassId = Class_ClassId;
            model.ClassId = Class_ClassId;


            model.WingName = WingName;
            model.WingId = WingId;
            model.SchoolId = SchoolId;
            model.SchoolName = SchoolName;


            model.SectionList = _sectionMstRepo.GetAcademic_SectionMasterList("").Select(r => new SelectListItem
            {
                Text = r.SecName,
                Value = r.SecId
            }).ToList();

            model.TeacherList = _sectionSetupRepo.GetTeacherList();
            return View(model);
        }

        //[EncryptedActionParameter]
        //public ActionResult Updation(int Class_Strength, string Class_Name, string Class_ClassId, string SectionId)
        //{
        //    return View(_sectionSetupRepo.GetSectionSetupDetailsList(Class_Strength, Class_Name, Class_ClassId, SectionId));
        //}

        [HttpPost]
        public JsonResult Academic_SectionSetup_CRUD(Academic_SectionSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _sectionSetupRepo.Academic_SectionSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_SectionSetup_Delete(string ClassId, string SectionId)
        {
            Tuple<int, string> res = _sectionSetupRepo.Academic_SectionSetup_CRUD(ClassId, SectionId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}