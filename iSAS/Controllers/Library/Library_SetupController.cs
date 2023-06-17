using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class Library_SetupController : Controller
    {
        private ILibrary_SetupRepo _libSetupRepo;

        public Library_SetupController(ILibrary_SetupRepo libSetupRepo)
        {
            _libSetupRepo = libSetupRepo;
        }

        // GET: Library_BookTitleMaster
        public PartialViewResult LandingPage()
        {
            return PartialView(_libSetupRepo.GetLibraryList(""));
        }

        public ActionResult New()
        {
            Library_SetupModels model = new Library_SetupModels();
            model.LibCat= "ACC";
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string LibraryId)
        {
            Library_SetupModels model = _libSetupRepo.GetLibraryById(LibraryId);
            return View(model);
        }

        [HttpPost]
        public JsonResult Library_Setup_CRUD(Library_SetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                
                Tuple<int, string> res = _libSetupRepo.Library_Setup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color =  res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Library_Setup_DELETE(string LibraryId)
        {
            Tuple<int, string> res = _libSetupRepo.Library_Setup_CRUD(LibraryId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}