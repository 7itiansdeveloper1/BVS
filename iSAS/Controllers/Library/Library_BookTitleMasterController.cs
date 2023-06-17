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
    public class Library_BookTitleMasterController : Controller
    {
        private ILibrary_BookTitleMasterRepo _bookTitleRepo;

        public Library_BookTitleMasterController(ILibrary_BookTitleMasterRepo titleRepo)
        {
            _bookTitleRepo = titleRepo;
        }

        // GET: Library_BookTitleMaster
        public PartialViewResult LandingPage(string TitleType)
        {
            ViewBag.titleType = TitleType;
            return PartialView(_bookTitleRepo.GetBookTitleList("", TitleType));
        }

        public ActionResult New(string TitleType = "Title")
        {
            Library_BookTitleMasterModels model = new Library_BookTitleMasterModels();
            model.TitleType = TitleType;
            model.IsActive = true;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string BookTitleId, string TitleType)
        {
            Library_BookTitleMasterModels model = _bookTitleRepo.GetBookTitleById(BookTitleId, TitleType);
            return View(model);
        }

        public PartialViewResult _BookTitleWiseReport(string TitleId, string TitleType)
        {
            Tuple<List<Library_Author_BookTitleWiseReportModel>, string> result = _bookTitleRepo.Get_BookTitleWiseReport(TitleId, TitleType);
            ViewBag.reportName = result.Item2;
            return PartialView("~/Views/Library_AuthorMaster/_BookTitleWiseReport.cshtml", result.Item1);
        }


        [HttpPost]
        public JsonResult Library_BookTitleMaster_CRUD(Library_BookTitleMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _bookTitleRepo.Library_BookTitleMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Library_BookTitleMaster_DELETE(string BookTitleId)
        {
            Tuple<int, string> res = _bookTitleRepo.Library_BookTitleMaster_CRUD(BookTitleId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}