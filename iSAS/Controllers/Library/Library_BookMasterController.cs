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
    public class Library_BookMasterController : Controller
    {
        private ILibrary_BookMasterRepo _bookRepo;
        public Library_BookMasterController(ILibrary_BookMasterRepo bookRepo)
        {
            _bookRepo = bookRepo;
        }
        // GET: Library_BookMaster
        public ActionResult New()
        {
            Library_BookMasterModels model = _bookRepo.GetFormLoadDetails();
            model.BookEntry = "SIN";

            model.BillDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.BookDate = model.BillDate;
            model.WDDate = model.BillDate;
            return View(model);
        }

        public PartialViewResult _New(string LibId, string AccNo, string BtnClick)
        {
            Library_BookMasterModels formLoadval = _bookRepo.GetFormLoadDetails();
            Library_BookMasterModels model = _bookRepo.GetBookById(LibId, AccNo, BtnClick);
            if (model != null)
            {
                model.LibraryList = formLoadval.LibraryList;
                model.ClassList = formLoadval.ClassList;
                model.SubjectList = formLoadval.SubjectList;
                model.SupplierList = formLoadval.SupplierList;
                model.PublisherList = formLoadval.PublisherList;
                model.AuthorList = formLoadval.AuthorList;
                model.CoAuthorList = formLoadval.CoAuthorList;
                model.TitleList = formLoadval.TitleList;
                model.SubTitleList = formLoadval.SubTitleList;
            }
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Library_BookMaster_CRUD(Library_BookMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _bookRepo.Library_BookMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}