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
    public class Transport_RouteMasterController : Controller
    {
        private ITransport_RouteMasterRepo _transportRepo;
        public Transport_RouteMasterController(ITransport_RouteMasterRepo transportRepo)
        {
            _transportRepo = transportRepo;
        }

        public ActionResult LandingPage()
        {
            return PartialView(_transportRepo.GetRouteMasterList(""));
        }

        public ActionResult New()
        {
            Transport_RouteMasterModels model = new Transport_RouteMasterModels();
            model.Active = true;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string RouteId)
        {
            return View(_transportRepo.GetRouteMasterById(RouteId));
        }

        [HttpPost]
        public JsonResult Transport_RouteMaster_CRUD(Transport_RouteMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _transportRepo.Transport_RouteMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Transport_RouteMaster_Delete(string RouteId)
        {
            Tuple<int, string> res = _transportRepo.Transport_RouteMaster_CRUD(RouteId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}