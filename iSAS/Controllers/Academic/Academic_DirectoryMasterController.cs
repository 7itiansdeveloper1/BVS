using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_DirectoryMasterController : Controller
    {
        private IAcademic_DirectoryMasterRepo _directoryMaster;
        public Academic_DirectoryMasterController(IAcademic_DirectoryMasterRepo directoryMaster)
        {
            _directoryMaster = directoryMaster;
        }

        public PartialViewResult _LandingPage()
        {
            return PartialView(_directoryMaster.Get_DirectoryList(null, Session["UserId"].ToString()));
        }

        public ViewResult New()
        {
            return View(new Academic_DirectoryMasterModels { IsActive = true });
        }

        public PartialViewResult _Update(string DirectoryId)
        {
            return PartialView(_directoryMaster.Get_DirectoryById(DirectoryId, Session["UserId"].ToString()));
        }

        [HttpPost]
        public JsonResult Academic_DirectoryMaster_CRUD(Academic_DirectoryMasterModels model)
        {
            if (ModelState.IsValid)

            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _directoryMaster.Academic_DirectoryMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_DirectoryMaster_Delete(string DirectoryId)
        {
            Tuple<int, string> res = _directoryMaster.Academic_DirectoryMaster_CRUD(DirectoryId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

    }
}