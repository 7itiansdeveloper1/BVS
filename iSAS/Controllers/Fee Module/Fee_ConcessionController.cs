using ISas.Entities;
using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_ConcessionController : Controller
    {
        private ICommonRepo _commonRepo;
        private IFee_ConcessionRepo _feeConcessionRepo;
        public Fee_ConcessionController(ICommonRepo commonRepo, IFee_ConcessionRepo feeConcessionRepo)
        {
            _commonRepo = commonRepo;
            _feeConcessionRepo = feeConcessionRepo;
        }

        // GET: Fee_Concession
        [CustomAuthorizeFilter("NEW")]
        [EncryptedActionParameter]
        public ActionResult Fee_ConcessionAllotment(string erpNo = null)
        {
            Fee_ConcessionModels model = new Fee_ConcessionModels();
            model.CreditNoteDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.BankList = _commonRepo.GetBankList();
            model.ConcessionCategoryList = _feeConcessionRepo.GetConcessionCategoryList();
            model.ConcessionFor = "CN";
            ViewBag.erpNo = erpNo;
            return View(model);
        }

        public JsonResult GetHeadList(string ERPNo, string SessionId)
        {
            return Json(_feeConcessionRepo.GetHeadList("Fee", ERPNo, SessionId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInstallmentList(string ERPNo, string SessionId)
        {
            return Json(_feeConcessionRepo.GetInstallmentList(ERPNo, SessionId, "StudentInstallmentList"), JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult _CreditNote_ConsessionDetails(string ERPNo, string FeeHeadId, string SessionId, string InstallmentId, string ConcessionFor)
        {
            Fee_ConcessionModels model = new Fee_ConcessionModels();
            if (ConcessionFor == "CN")
                model.ConsessionList = _feeConcessionRepo.GetConcessionDetailsList("Fee", ERPNo, "StudentFeeHeadDueList", FeeHeadId, SessionId, InstallmentId);
            else if (ConcessionFor == "DS")
                model.ConsessionList = _feeConcessionRepo.GetConcessionDetailsList("Fee", ERPNo, "StudentInstallmentDueList", FeeHeadId, SessionId, InstallmentId);

            return PartialView(model);
        }

        //[HttpPost]
        [CustomAuthorizeFilter("SAVE_WITH_UPDATE")]
        public JsonResult Fee_Concession_CRUD(Fee_ConcessionModels model, StudentSearchModel studentDetails)
        {
            if (ModelState.IsValid)
            {
                model.CRUDFor = "SAVE";
                model.StudentDetails = studentDetails;
                if (model.IsSingleSave)
                {
                    DataTable paramDt = new DataTable();
                    paramDt.Columns.Add("CreditNoteRefNo");
                    paramDt.Columns.Add("DueDate");
                    paramDt.Columns.Add("ConcessionAmount");
                    paramDt.Columns.Add("FeeHeadId");
                    DataRow row = paramDt.NewRow();
                    row[0] = model.Selected_CreditNoteId;
                    row[1] = model.Selected_DueDate;
                    row[2] = model.Selected_ConcessionAmount;
                    row[3] = model.Selected_FeeHeadId;
                    paramDt.Rows.Add(row);
                    Tuple<int, string> res = _feeConcessionRepo.Fee_Concession_CRUD(model, paramDt, Session["UserId"].ToString());
                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        DataTable paramDt = new DataTable();
                        paramDt.Columns.Add("CreditNoteRefNo");
                        paramDt.Columns.Add("DueDate");
                        paramDt.Columns.Add("ConcessionAmount");
                        paramDt.Columns.Add("FeeHeadId");
                        int count = 0;
                        string concessionfor = model.ConcessionFor;
                        foreach (var item in model.ConsessionList.Where(r => r.IsEditable))
                        {
                            if (item.IsSelected)
                            {
                                DataRow row = paramDt.NewRow();
                                row[0] = item.CreditNoteRefNo;
                                row[1] = item.DisplayDueDate;
                                if (concessionfor == "CN")
                                    row[2] = item.ConcAmount.ToString();
                                else
                                    row[2] = item.DiscAmount.ToString();
                                row[3] = item.HeadId.ToString();
                                paramDt.Rows.Add(row);
                            }



                            count++;
                        }

                        Tuple<int, string> res = _feeConcessionRepo.Fee_Concession_CRUD(model, paramDt, Session["UserId"].ToString());


                        //return Json(new { status = "success", Msg = "Applied Successfully", Color = "Success" }, JsonRequestBehavior.AllowGet);
                        return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        return Json(new { status = "success", Msg = ex.Message, Color = "Warning" }, JsonRequestBehavior.AllowGet);
                    }
                }
                // return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("CANCEL")]
        public JsonResult Fee_Concession_Cancel(string SessionId, string ERPNo, string DueDate, string FeeHeadId, string CreditNoteId, string ConcessionFor)
        {
            Tuple<int, string> res = _feeConcessionRepo.Fee_Concession_CRUD(SessionId, ERPNo, DueDate, FeeHeadId, CreditNoteId, ConcessionFor, Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetConcessionCategoryPercent(string ConcessionCategoryId)
        {
            return Json(_feeConcessionRepo.GetConcessionCategoryPercent(ConcessionCategoryId), JsonRequestBehavior.AllowGet);
        }
    }
}