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
    public class Library_SubjectMasterController : Controller
    {
        private ILibrary_SubjectMasterRepo _subjectRepo;
        public Library_SubjectMasterController(ILibrary_SubjectMasterRepo subjectRepo)
        {
            _subjectRepo = subjectRepo;
        }

        public PartialViewResult LandingPage()
        {
            return PartialView(_subjectRepo.GetSubjectList(""));
        }

        public ActionResult New()
        {
            Library_SubjectMasterModels model = new Library_SubjectMasterModels();
            model.IsActive = true;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string SubjectId)
        {
            Library_SubjectMasterModels model = _subjectRepo.GetSubjectById(SubjectId);
            return View(model);
        }

        [HttpPost]
        public JsonResult Library_SubjectMaster_CRUD(Library_SubjectMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _subjectRepo.Library_SubjectMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Library_SubjectMaster_DELETE(string SubjectId)
        {
            Tuple<int, string> res = _subjectRepo.Library_SubjectMaster_CRUD(SubjectId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult _BookTitleWiseReport(string SubjectId)
        {
            Tuple<List<Library_Author_BookTitleWiseReportModel>, string> result = _subjectRepo.Get_BookTitleWiseReport(SubjectId);
            ViewBag.reportName = result.Item2;
            return PartialView("~/Views/Library_AuthorMaster/_BookTitleWiseReport.cshtml", result.Item1);
        }

    }
}