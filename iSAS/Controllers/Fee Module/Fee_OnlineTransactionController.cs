using ISas.Entities.FeesEntities;
using ISas.Entities.Test;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    
    [ExceptionHandler]
    public class Fee_OnlineTransactionController : Controller
    {
        IFee_OnlineTransactionRepo _fee_OnlineTransactionRepo;
        public Fee_OnlineTransactionController(IFee_OnlineTransactionRepo fee_OnlineTransactionRepo)
        {
            _fee_OnlineTransactionRepo = fee_OnlineTransactionRepo;
        }
        public ActionResult OnlineTransaction()
        {
            Fee_OnlineTransactionModel model = new Fee_OnlineTransactionModel();
            model.rectiptDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            return View(model);
        }

        public PartialViewResult _onlineTrnasaction(string daterange = "",string paymentstatus="")
        {
            string fromdate = null, todate = null;
            int l = daterange.Length;
            if (l > 0)
            {
                fromdate = daterange.Substring(0, 10);
                todate = daterange.Substring(13, 10);
            }
            return PartialView(_fee_OnlineTransactionRepo.OnlineSettlement_Transaction(fromdate, todate, paymentstatus, Session["SessionId"].ToString()));
        }
        [HttpPost]
        public JsonResult FeeReceipt_CRUD(Fee_OnlineTransactionModel model)
        {
            if (ModelState.IsValid)
            {
                string tnxid = string.Join(",", model.onlineTransctionsList.Where(r => r.isReadyForReceipt && r.isSelected).Select(r => r.tnxId).ToList());
                Tuple<int, string> res = _fee_OnlineTransactionRepo.OnlineTransaction_CRUD(tnxid, Session["SessionId"].ToString(), model.rectiptDate, Session["UserId"].ToString());
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys, Color = "Warning" }, JsonRequestBehavior.AllowGet);

        }
    }
}