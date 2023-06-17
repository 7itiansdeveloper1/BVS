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
    public class Academic_HouseMasterController : Controller
    {
        private IAcademic_HouseMasterRepo _houseMstRepo;
        public Academic_HouseMasterController(IAcademic_HouseMasterRepo houseMst)
        {
            _houseMstRepo = houseMst;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return PartialView(_houseMstRepo.GetHouseList());
        }

        public ActionResult New()
        {
            Academic_HouseMasterModels model = new Academic_HouseMasterModels();
            model.Active = true;
            return View(model);
        }

        [HttpPost]
        public JsonResult Academic_HouseMaster_CRUD(Academic_HouseMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _houseMstRepo.Academic_HouseMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_HouseMaster_Delete(string HouseId)
        {
            Tuple<int, string> res = _houseMstRepo.Academic_HouseMaster_CRUD(HouseId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}