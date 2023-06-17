using ISas.Entities.TransportEntities;
using ISas.Repository.TransportRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Transport
{
    [Authorize]
    [ExceptionHandler]
    public class Transport_VehicleSetupController : Controller
    {
        private ITransport_VehicleSetupRepo _vehicleRepo;
        private List<SelectListItem> vehTypeList;
        public Transport_VehicleSetupController(ITransport_VehicleSetupRepo vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
            vehTypeList = new List<SelectListItem>();
            vehTypeList.Add(new SelectListItem { Text = "Bus", Value = "Bus" });
            vehTypeList.Add(new SelectListItem { Text = "Van", Value = "Van" });
            vehTypeList.Add(new SelectListItem { Text = "Rickshwa", Value = "Rickshwa" });
        }

        // GET: Transport_StopSetup
        public ActionResult LandingPage()
        {
            return View(_vehicleRepo.GetVehicleList(""));
        }

        public ActionResult New()
        {
            Transport_VehicleSetupModel model = new Transport_VehicleSetupModel();
            model.VehicleTypeList = vehTypeList;
            model.BloodGrpList = _vehicleRepo.GetBloodGroupList();
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string VehicleId)
        {
            Transport_VehicleSetupModel model = _vehicleRepo.GetVehicleById(VehicleId);
            model.VehicleTypeList = vehTypeList;
            model.BloodGrpList = _vehicleRepo.GetBloodGroupList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Transport_VehicleSetup_CRUD(Transport_VehicleSetupModel model)
        {
            if (ModelState.IsValid)
            {
                //model.UserId = Session["UserId"].ToString();
                //model.CRUDMode = "SAVE";
                Tuple<int, string> res = _vehicleRepo.Transport_VehicleSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Transport_VehicleSetup_Delete(string VehicleId)
        {
            Tuple<int, string> res = _vehicleRepo.Transport_VehicleSetup_CRUD(VehicleId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult _VehicleRouteSetup(string VehicleId)
        {
            return PartialView(_vehicleRepo.GetVehicleRouteDetails(VehicleId));
        }

        [HttpPost]
        public JsonResult VehicleRouteSetup_CRUD(VehicleRouteSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _vehicleRepo.VehicleRouteSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}