using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using ISas.Repository.Academic.IRepository;
using ISas.Entities.Academic;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_DocumentMasterController : Controller
    {
        private IAcademic_DocumentMasterRepo _documentMasterRepo;
        public Academic_DocumentMasterController(IAcademic_DocumentMasterRepo documentMasterRepo)
        {
            _documentMasterRepo = documentMasterRepo;
        }
        public ActionResult LandingPage()
        {
            return PartialView(_documentMasterRepo.GetDocumentList(null));
        }
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Academic_DocumentMaster_CRUD(Academic_DocumentMasterModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _documentMasterRepo.Academic_DocumentMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        [EncryptedActionParameter]
        public ActionResult Updation(string docno)
        {
            return View(_documentMasterRepo.GetDocumentByDocNo(docno));
        }
        public JsonResult Academic_DocumentMaster_Delete(string docno)
        {
            Tuple<int, string> res = _documentMasterRepo.Academic_DocumentMaster_CRUD(docno,Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}