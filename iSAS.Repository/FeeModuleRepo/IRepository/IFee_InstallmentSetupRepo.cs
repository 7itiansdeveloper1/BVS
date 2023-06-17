using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_InstallmentSetupRepo
    {
        List<Fee_InstallmentSetupModels> GetInstallmetsByStructId(string StructId, string InstallId, string sessId);
        Tuple<int, string> Fee_InstallmentSetup_CRUD(Fee_InstallmentSetupModels model);
        Tuple<int, string> Fee_InstallmentSetup_CRUD(string InstallId, string UserId);
        List<SelectListItem> InstallmetDropdwon_ByStrectureId(string StrectureId);
    }
}