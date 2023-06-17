using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class Library_AuthorMasterController : Controller
    {
        private ILibrary_AuthorMasterRepo _authorMstRepo;

        public Library_AuthorMasterController(ILibrary_AuthorMasterRepo authorMst)
        {
            _authorMstRepo = authorMst;
        }

        // GET: Library_BookTitleMaster
        public PartialViewResult LandingPage(string AuthorType)
        {
            return PartialView(_authorMstRepo.GetAuthorList("", AuthorType));
        }

        public ActionResult New(string AuthorType = "AUT")
        {
            Library_AuthorMasterModels model = new Library_AuthorMasterModels();
            model.AuthorType = AuthorType;
            model.IsActive = true;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string AuthorId, string AuthorType)
        {
            Library_AuthorMasterModels model = _authorMstRepo.GetAuthorById(AuthorId, AuthorType);
            return View(model);
        }


        [EncryptedActionParameter]
        public ViewResult IndividualReport(string AuthorType, string AuthId)
        {
            //Library_Author_IndividualReportModel
            return View(_authorMstRepo.Get_IndividualReport(AuthorType, AuthId));
        }

        public PartialViewResult _BookTitleWiseReport(string AuthorType, string AuthId, string TitleId)
        {
            Tuple<List<Library_Author_BookTitleWiseReportModel>, string> result = _authorMstRepo.Get_BookTitleWiseReport(AuthId, AuthorType, TitleId);
            ViewBag.reportName = result.Item2;
            return PartialView(result.Item1);
        }

        [HttpPost]
        public JsonResult Library_AuthorMaster_CRUD(Library_AuthorMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _authorMstRepo.Library_AuthorMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Library_AuthorMaster_DELETE(string AuthorId)
        {
            Tuple<int, string> res = _authorMstRepo.Library_AuthorMaster_CRUD(AuthorId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}