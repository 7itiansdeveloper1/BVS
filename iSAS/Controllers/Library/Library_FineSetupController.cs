using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class Library_FineSetupController : Controller
    {
        private ILibrary_FineSetupRepo _fineSetupRepo;
        public Library_FineSetupController(ILibrary_FineSetupRepo fineSetupRepo)
        {
            _fineSetupRepo = fineSetupRepo;
        }

        public PartialViewResult _LandingPage(string LibId)
        {
            return PartialView(_fineSetupRepo.Get_FinSetupList(LibId, null, Session["UserId"].ToString()));
        }

        [EncryptedActionParameter]
        public ViewResult New(string LibId, string LibName)
        {
            return View(new Library_FineSetupModels { LibraryId = LibId, LibraryName = LibName, IsActive = true });
        }

        public PartialViewResult _Update(string LibId, string FineId)
        {
            return PartialView(_fineSetupRepo.Get_FinSetupById(LibId, FineId, Session["UserId"].ToString()));
        }

        [HttpPost]
        public JsonResult Library_FineSetup_CRUD(Library_FineSetupModels model)
        {
            if (ModelState.IsValid)

            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _fineSetupRepo.Library_FineSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Library_FineSetup_Delete(string FineId)
        {
            Tuple<int, string> res = _fineSetupRepo.Library_FineSetup_CRUD(FineId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}