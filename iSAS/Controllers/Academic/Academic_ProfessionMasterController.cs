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
    public class Academic_ProfessionMasterController : Controller
    {
        private IAcademic_ProfessionMasterRepo _profMstRepo;
        public Academic_ProfessionMasterController(IAcademic_ProfessionMasterRepo profMstRepo)
        {
            _profMstRepo = profMstRepo;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return PartialView(_profMstRepo.GetProfessionList());
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Academic_ProfessionMaster_CRUD(Academic_ProfessionMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _profMstRepo.Academic_ProfessionMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_ProfessionMaster_Delete(string ProfId)
        {
            Tuple<int, string> res = _profMstRepo.Academic_ProfessionMaster_CRUD(ProfId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}