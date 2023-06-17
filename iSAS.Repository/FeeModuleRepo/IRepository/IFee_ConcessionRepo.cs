using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_ConcessionRepo
    {
        List<SelectListItem> GetConcessionCategoryList(string ConcessionCategroy = "Fee", string QueryFor = "ConcessionList");

        int GetConcessionCategoryPercent(string ConcessionCategoryId);

        List<SelectListItem> GetHeadList(string ConcessionCategroy, string ERPNo, string SessionId, string QueryFor = "StudentConcessionHeadList");
        List<SelectListItem> GetInstallmentList(string ERPNo, string SessionId, string QueryFor = "StudentConcessionHeadList");

        List<ConsessioinDetailsModel> GetConcessionDetailsList(string ConcessionCategroy, string ERPNo,  string QueryFor, string FeeHeadId, string SessionId, string InstallmentId);

        Tuple<int, string> Fee_Concession_CRUD(Fee_ConcessionModels model, DataTable paramdt,string userId);
        Tuple<int, string> Fee_Concession_CRUD(string SessionId, string ERPNo, string DueDate, string FeeHeadId, string CreditNoteId, string ConcessionFor, string userId);
    }
}
