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
    public class Academic_WingSetupController : Controller
    {
        private IAcademic_WingSetupRepo _wingSetupRepo;
        public Academic_WingSetupController(IAcademic_WingSetupRepo wingsetupRepo)
        {
            _wingSetupRepo = wingsetupRepo;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage(string SchoolName, int SchoolId)
        {
            return PartialView(_wingSetupRepo.GetWingList(SchoolName, SchoolId, ""));
        }

        [EncryptedActionParameter]
        public ActionResult New(string SchoolName, string SchoolId)
        {
            Academic_WingSetupModels model = new Academic_WingSetupModels();
            model.SchoolId = Convert.ToInt32(SchoolId);
            model.SchoolName = SchoolName;
            model.PrintOrder = 1;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string SchoolName, string SchoolId, string WingId)
        {
            return View(_wingSetupRepo.GetWingDetailsById(SchoolName, Convert.ToInt32(SchoolId), WingId));
        }

        [HttpPost]
        public JsonResult Academic_WingSetup_CRUD(Academic_WingSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _wingSetupRepo.Academic_WingSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_WingSetup_Delete(string WingId)
        {
            Tuple<int, string> res = _wingSetupRepo.Academic_WingSetup_CRUD(WingId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}