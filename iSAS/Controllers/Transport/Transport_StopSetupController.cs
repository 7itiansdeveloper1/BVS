using ISas.Entities.TransportEntities;
using ISas.Repository.TransportRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Transport
{
    [Authorize]
    [ExceptionHandler]
    public class Transport_StopSetupController : Controller
    {
        private ITransport_StopSetupRepo _stopRepo;
        public Transport_StopSetupController(ITransport_StopSetupRepo stopRepo)
        {
            _stopRepo = stopRepo;
        }

        // GET: Transport_StopSetup
        public ActionResult LandingPage(string RouteId)
        {
            return PartialView(_stopRepo.GetStopList(RouteId, ""));
        }

        public ActionResult New(string RouteId, string RouteName)
        {
            Transport_StopSetupModels model = new Transport_StopSetupModels();
            model.RouteId = RouteId;
            model.RouteName = RouteName;
            model.Active = true;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string RouteId, string StopId)
        {
            return View(_stopRepo.GetStopById(RouteId, StopId));
        }

        [HttpPost]
        public JsonResult Transport_StopSetup_CRUD(Transport_StopSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _stopRepo.Transport_StopSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Transport_StopSetup_Delete(string StopId)
        {
            Tuple<int, string> res = _stopRepo.Transport_StopSetup_CRUD(StopId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}