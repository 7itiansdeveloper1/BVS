using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_FeeStructureMasterController : Controller
    {
        private IFee_FeeStructureMasterRepo _feeStrectureRepo;
        public Fee_FeeStructureMasterController(IFee_FeeStructureMasterRepo feeStrectRepo)
        {
            _feeStrectureRepo = feeStrectRepo;
        }

        // GET: Fee_FeeStrectureMaster
        public ActionResult LandingPage()
        {
            return PartialView(_feeStrectureRepo.GetFeeStructureList("",Session["SessionId"].ToString()));
        }

        [CustomAuthorizeFilter("NEW_WITH_VIEW")]
        public ActionResult New()
        {
            Fee_FeeStructureMasterModels model = new Fee_FeeStructureMasterModels();
            model.IsActive = true;
            return View(model);
        }

        [CustomAuthorizeFilter("UPDATE")]
        [EncryptedActionParameter]
        public ActionResult Updation(string StructId)
        {
            return View(_feeStrectureRepo.GetFeeStructureByStructId(StructId, Session["SessionId"].ToString()));
        }

        [HttpPost]
        public JsonResult Fee_FeeStructureMaster_CRUD(Fee_FeeStructureMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _feeStrectureRepo.Fee_FeeStructureMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("DELETE")]
        public JsonResult Fee_FeeStructureMaster_Delete(string StructId)
        {
            Tuple<int, string> res = _feeStrectureRepo.Fee_FeeStructureMaster_CRUD(StructId, Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Fee_StrectureDropDown_ByClassSectionId(string ClassSectionId)
        {
            return Json(_feeStrectureRepo.Fee_StrectureDropDown_ByClassSectionId(ClassSectionId), JsonRequestBehavior.AllowGet);
        }
    }
}