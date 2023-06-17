using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_ReceiptCancellationRepo
    {
        List<ReceiptDetailModel> GetReceiptDetailList(string FromDate, string ToDate, string QueryFor, string FeeType, string ERPNo, string RecNo, string UserId, string sessionId);
        ReceiptDetailModel GetReceiptDetailById(string FromDate, string ToDate, string QueryFor, string FeeType, string ERPNo, string RecNo, string UserId, string sessionId);
        Fee_ReceiptCancellationModels GetDishonurReceiptDetails(string SessionId, string RecptNo, string TransReferenceNo, string ERPNo);
        Tuple<int, string> Fee_ReceiptCancellation_CRUD(Fee_ReceiptCancellationModels model);

        Tuple<int, string> Fee_Receipt_CRUD(ReceiptDetailModel model);
    }
}
