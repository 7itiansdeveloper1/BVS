using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_TransactionRepo
    {
        Fee_Tran_LandingModel GetCollectionDetails(string fromDate, string toDate, string userId,string sessionId);
        Fee_StudentLedgerModel Get_Fee_StudentLedgerModel(string ErpNo, string SessionId);
        List<Fee_InstallmentDetailsList> Get_Fee_InstallmentDetailsList(string erpNo, string dueDate, string sessionId);
        List<ReceiptDetailModel> GetReceiptDetails(string fromDate, string toDate);
        List<ReceiptDetailModel> GetReceiptDetails(string erpNo);
        List<StudentDetailsModel> GetStudentDetails(string SearchType, string SearchText, string ClassID, string SessionID, string status);
        List<StudentDetailsModel> GetStudentDetails(string SearchType, string SearchText, string ClassID, string SessionID);
        List<SelectListItem> GetUnpaidInstallmentList(string ErpNo, string SessionId, string FeeMode);
        FeeReceiptModel GetFeeInstallmentDetails(string ErpNo, string SessionId, string DueDate, string FeeMode, string TransactionDate);
        string Fee_Transaction_CRUD(FeeReceiptModel model,string userid);
        //string FeeReceipt_CancelOrDelete(string ERPNo, string SessionId, string ReceiptNo, string Mode);
        string FeeReceipt_CancelOrDelete(string ERPNo, string SessionId, string ReceiptNo, string Mode, string userId);
        string Get_LastReceiptNo();
    }
}
