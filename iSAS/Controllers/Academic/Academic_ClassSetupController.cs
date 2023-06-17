using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_ClassSetupController : Controller
    {
        private IAcademic_ClassSetupRepo _classSetupRepo;
        private IStudentClass _studentClassRepos;
        public Academic_ClassSetupController(IAcademic_ClassSetupRepo classSetupRepo, IStudentClass classRepo) 
        {
            _classSetupRepo = classSetupRepo;
            _studentClassRepos = classRepo;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage(string WingName, string WingId)
        {
            return PartialView(_classSetupRepo.GetClassList(WingName, WingId, ""));
        }

        [EncryptedActionParameter]
        public ActionResult New(string WingName, string WingId, string SchoolId, string SchoolName)
        {
            Academic_ClassSetupModels model = new Academic_ClassSetupModels();
            model.WingId = WingId;
            model.WingName = WingName;
            model.Active = true;
            model.SchoolId = SchoolId;
            model.SchoolName = SchoolName;

            var classes = _studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            if (classes != null && classes.Count() > 0)
                model.PromotedClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId,
                }).ToList();
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string WingName, string WingId, string ClassId)
        {
            Academic_ClassSetupModels model = _classSetupRepo.GetClassById(WingName, WingId, ClassId);
            var classes = _studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            if (classes != null && classes.Count() > 0)
                model.PromotedClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId,
                }).ToList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Academic_ClassSetup_CRUD(Academic_ClassSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _classSetupRepo.Academic_ClassSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_ClassSetup_Delete(string ClassId)
        {
            Tuple<int, string> res = _classSetupRepo.Academic_ClassSetup_CRUD(ClassId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}