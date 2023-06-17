using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_DueSetupRepo
    {
        Tuple<List<SelectListItem>, List<SelectListItem>> GetHeadWithInstallmentList(string StructId, string sessionId);
        List<Fee_DueSetupModels> GetClassDueList(string StructId, string StructName, string ClassId, string SectionId, string SessionId);
        List<Fee_DueSummery> GetClassDueSummeryList(string StructId, string StructName, string SessionId);
        Tuple<int, string> Fee_DueSetup_CRUD(Fee_DueSetupModels model, string sessionId);
        Tuple<int, string> Fee_DueSetup_CRUD(int RecordId);
        Tuple<int, string> Fee_DueSetup_CRUD(string StructId, string FromClassId, string FromSectionId, string ToClass, string UserId,string sessionId);
    }
}
