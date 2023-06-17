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
    public class Fee_FeeHeadMasterController : Controller
    {
        private IFee_FeeHeadMasterRepo _feeHeadMstRepo;
        public Fee_FeeHeadMasterController(IFee_FeeHeadMasterRepo feeHeadRepo)
        {
            _feeHeadMstRepo = feeHeadRepo;
        }

        // GET: Fee_FeeHeadMaster
        [CustomAuthorizeFilter("VIEW")]
        public ActionResult LandingPage()
        {
            return View(_feeHeadMstRepo.GetFeeHeadMasterList(""));
        }

        [CustomAuthorizeFilter("NEW")]
        public ActionResult New()
        {
            Fee_FeeHeadMasterModels model = new Fee_FeeHeadMasterModels();
            model.IsActive = true;
            return View(model);
        }

        [CustomAuthorizeFilter("UPDATE")]
        [EncryptedActionParameter]
        public ActionResult Updation(string HeadId)
        {
            return View(_feeHeadMstRepo.GetFeeHeadByHeadID(HeadId));
        }

        [HttpPost]
        public JsonResult Fee_FeeHeadMaster_CRUD(Fee_FeeHeadMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _feeHeadMstRepo.Fee_FeeHeadMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("DELETE")]
        public JsonResult Fee_FeeHeadMaster_Delete(string HeadId)
        {
            Tuple<int, string> res = _feeHeadMstRepo.Fee_FeeHeadMaster_CRUD(HeadId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Fee_FeeHeadMaster_UpdatePrintOrder(string HeadId, int PrintOrder)
        {
            Tuple<int, string> res = _feeHeadMstRepo.Fee_FeeHeadMaster_CRUD(HeadId, PrintOrder, Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}