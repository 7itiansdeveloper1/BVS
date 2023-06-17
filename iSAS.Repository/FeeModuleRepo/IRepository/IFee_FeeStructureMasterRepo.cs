using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public  interface IFee_FeeStructureMasterRepo
    {
        List<Fee_FeeStructureMasterModels> GetFeeStructureList(string StructId, string sessionId);
        Fee_FeeStructureMasterModels GetFeeStructureByStructId(string StructId, string sessionId);
        Tuple<int, string> Fee_FeeStructureMaster_CRUD(Fee_FeeStructureMasterModels model);
        Tuple<int, string> Fee_FeeStructureMaster_CRUD(string StructId, string UserId);
        List<SelectListItem> Fee_StrectureDropDown_ByClassSectionId(string ClassSectionId);
    }
}
