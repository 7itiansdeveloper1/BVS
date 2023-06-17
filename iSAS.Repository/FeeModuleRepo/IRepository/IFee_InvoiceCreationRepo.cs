using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_InvoiceCreationRepo
    {
        List<SelectListItem> GetFeeCategoryList();
        List<InvoiceDetails> GetInvoiceDetailsList(string ClassId, string SectionId, string SessionId);
        List<FeeStructureDetails> GetFeeStrectureDetailsList(string ClassId, string SectionId, string StructId, string sessionId);
        Tuple<int, string> Fee_InvoiceCreation_CRUD(string SessionId, string ERPNo, string StructId, string UserId);


        List<CancelInvoice_InvoiceDetailsModel> CancelInvoice_InvoiceList(string ERPNo,string sessionId);
        List<SelectListItem> ConcessionCategoryList(string ERPNo);
        Tuple<int, string> CancelInvoice_CRUD(string ERPNo, string InvoiceNo, string CancelCategory, string UserId);
    }
}
