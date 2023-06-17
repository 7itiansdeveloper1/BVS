using ISas.Entities.FeesEntities;
using ISas.Entities.CommonEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Web.Mvc;
using System.Linq;
using ISas.Entities;
using ISas.Repository.Interface;
using ISas.Repository.DashboardRepository.IRepository;
using ISas.Web.Models;
using System.Collections.Generic;
using System.Configuration;
using ISas.Entities.DashboardEntities;


namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_TransactionController : Controller
    {
        private IFee_TransactionRepo _feeTrnRepo;
        private ICommonRepo _commonRepo;
        private IStudentFeeDetailsRepo _studentFeeDetailsRepo;
        private string TextVal = ConfigurationManager.AppSettings["NoOfMonthToFeePaid"].ToString();
        public Fee_TransactionController(IFee_TransactionRepo feeTrnRepo, ICommonRepo commonrepo, IStudentFeeDetailsRepo feeDetails)
        {
            this._feeTrnRepo = feeTrnRepo;
            this._commonRepo = commonrepo;
            this._studentFeeDetailsRepo = feeDetails;
        }

        public ActionResult LandingPage()
        {
            Fee_Tran_LandingModel model = new Fee_Tran_LandingModel();

            string today = DateTime.Now.ToShortDateString();
            model = _feeTrnRepo.GetCollectionDetails(today, today,Session["UserId"].ToString(), Session["SessionId"].ToString());
            model.FromDate = today.Replace("-", "/");
            model.ToDate = today.Replace("-", "/");
            return View(model);
        }

        public JsonResult GetCollectionDetails(string fromDate, string toDate)
        {
            Fee_Tran_LandingModel model = _feeTrnRepo.GetCollectionDetails(fromDate, toDate, Session["UserId"].ToString(), Session["SessionId"].ToString());
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _ReceiptDetails(string fromDate, string toDate)
        {
            return PartialView(_feeTrnRepo.GetReceiptDetails(fromDate, toDate));
        }

        public PartialViewResult _StudentDetails(string SearchType, string SearchText, string ClassID)
        {
            return PartialView(_feeTrnRepo.GetStudentDetails(SearchType, SearchText, ClassID, Session["SessionId"].ToString(), "Active"));
        }

        public PartialViewResult _ReceiptDetails_ByERPNo(string erpno)
        {
            StudentFeeStatusModel model = new StudentFeeStatusModel();
            model = _studentFeeDetailsRepo.GetFeeStatusDetailsList(erpno, Convert.ToInt32(Session["SessionId"]));
            model.StudentERPNo = erpno;
            //return PartialView(_studentFeeDetailsRepo.GetFeeStatusDetailsList(erpno, Convert.ToInt32(Session["SessionID"])));
            return PartialView(model);
        }

        public PartialViewResult _TransportDetails_ByERPNo(string erpno)
        {
            return PartialView(_studentFeeDetailsRepo.GetFeeStatusDetails_TransportList(erpno, Convert.ToInt32(Session["SessionId"])));
        }


        [EncryptedActionParameter]
        public ActionResult NewFeeReceipt(string erpNo = "")
        {
            FeeReceiptModel model = new FeeReceiptModel();
            model.BankList = _commonRepo.GetBankList();
            model.TransactionDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.ReceiptDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            ViewBag.erpNo = erpNo;
            return View(model);
        }
        [EncryptedActionParameter]
        public ActionResult NewStudentLedger(string erpNo)
        {
            Fee_StudentLedgerModel model = _feeTrnRepo.Get_Fee_StudentLedgerModel(erpNo, Session["SessionId"].ToString());
            return View(model);
        }
        public PartialViewResult _StudentLedgerInstallmentDetails(string ErpNo,string DueDate)
        {
            ReportHeaderEntities model1 = _commonRepo.ReportHeaderDetails(" ");
            if (System.IO.File.Exists(Server.MapPath("~/" + model1.LogoURL + "")))
                ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/" + model1.LogoURL + ""))));
            else
                ViewBag.ImageData = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Images/System/loginBgleft.jpg")))); //defaultLogo.png

            ViewBag.HeaderInfo = model1;
            Fee_StudentLedgerModel model = new Fee_StudentLedgerModel();
            model.InstallmentDetailsList = _feeTrnRepo.Get_Fee_InstallmentDetailsList(ErpNo, DueDate,Session["SessionId"].ToString());
            return PartialView(model);
        }


        public PartialViewResult _FeeInstallmentDetails(string ErpNo, string SessionId, string DueDate, string FeeMode, string TransactionDate)
        {
            FeeReceiptModel model = _feeTrnRepo.GetFeeInstallmentDetails(ErpNo, SessionId, DueDate, FeeMode, TransactionDate);
            model.HddnERPNo = ErpNo;
            return PartialView(model);
        }

        

        public JsonResult GetUnpaidInstallmentList(string ErpNo, string SessionId, string FeeMode)
        {
            List<SelectListItem> installmentList =  _feeTrnRepo.GetUnpaidInstallmentList(ErpNo, SessionId, FeeMode);
            if (installmentList != null && installmentList.Count > 0)
                installmentList = installmentList.Select(c => { c.Selected = c.Text == TextVal ? true : false; return c; }).ToList();

            return Json(installmentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FeeReceipt_CRUD(FeeReceiptModel model, StudentSearchModel studentDetails)
        {
            if(model.FeeModeId == "CE")
            {
                if (string.IsNullOrEmpty(model.SelectedBankId))
                    ModelState.AddModelError("SelectedBankId", "Bank is Req.!");

                if (string.IsNullOrEmpty(model.TransactionNo))
                    ModelState.AddModelError("TransactionNo", "Trans. No is Req.!");

                if (string.IsNullOrEmpty(model.TransactionDate))
                    ModelState.AddModelError("TransactionDate", "Trans. Date is Req.!");
            }

            if (ModelState.IsValid)
            {
                model.StudentDetails = studentDetails;
                string receiptNo = _feeTrnRepo.Fee_Transaction_CRUD(model,Session["UserId"].ToString());
                return Json(new { status = "success", Msg = "Your receipt no is " + receiptNo, ReceiptNo = receiptNo }, JsonRequestBehavior.AllowGet);
            }

            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FeeReceipt_CancelOrDelete(string ERPNo, string SessionId, string ReceiptNo, string Mode)
        {
            string msg = this._feeTrnRepo.FeeReceipt_CancelOrDelete(ERPNo, SessionId, ReceiptNo, Mode,Session["UserId"].ToString());
            return Json(new { status = "success", Msg = msg }, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult _EncodedViewRctLink(string ReceiptNo, string StudentId)
        {
            return PartialView(new Tuple<string, string>(ReceiptNo, StudentId));
        }


        public JsonResult Get_LastReceiptNo()
        {
            return Json(_feeTrnRepo.Get_LastReceiptNo(), JsonRequestBehavior.AllowGet);
        }
    }
}

