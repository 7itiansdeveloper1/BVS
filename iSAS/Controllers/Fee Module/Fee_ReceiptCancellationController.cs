using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Web.Mvc;
using System.Linq;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_ReceiptCancellationController : Controller
    {
        private IFee_ReceiptCancellationRepo _feeCancellationRepo;

        public Fee_ReceiptCancellationController(IFee_ReceiptCancellationRepo feeCancellationRepo)
        {
            _feeCancellationRepo = feeCancellationRepo;
        }

        // GET: Fee_ReceiptCancellation
        public PartialViewResult LandingPage(string fromDate, string toDate, string QueryFor)
        {
            return PartialView(_feeCancellationRepo.GetReceiptDetailList(fromDate, toDate, QueryFor, null, null, null, Session["UserId"].ToString(), Session["SessionId"].ToString()));
        }

        [CustomAuthorizeFilter("NEW_WITH_VIEW")]
        public ActionResult New()
        {
            return View();
        }

        public PartialViewResult _DishonurReceiptDetails(string ReceiptNo, string TransReferenceNo, string ERPNo, string Selected_StudentName)
        {
            Fee_ReceiptCancellationModels model = _feeCancellationRepo.GetDishonurReceiptDetails(Session["SessionId"].ToString(), ReceiptNo, TransReferenceNo, ERPNo);
            model.Selected_StudentName = Selected_StudentName;
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Fee_ReceiptCancellation_CRUD(Fee_ReceiptCancellationModels model)
        {
            if (ModelState.IsValid)
            {
                model.SessionId = Session["SessionId"].ToString();
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _feeCancellationRepo.Fee_ReceiptCancellation_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }


        #region Update Fee Receipt
        public PartialViewResult _UpdateFeeReceipt(string FeeType, string ERPNo, string RecNo)
        {
            return PartialView(_feeCancellationRepo.GetReceiptDetailById(null, null, "GeAlltReceiptList", FeeType, ERPNo, RecNo, Session["UserId"].ToString(), Session["SessionId"].ToString()));
        }


        [HttpPost]
        public JsonResult TempleteClassSetup_CRUD(ReceiptDetailModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _feeCancellationRepo.Fee_Receipt_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}