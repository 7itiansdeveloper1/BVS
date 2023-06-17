using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface ICommon_NECNRepo
    {
        IEnumerable<Common_NECN_LandingModel> LandingPageDetails(string UploadType);
        Common_NECN_SelectionGroup GetSelectionGroupDetails(string UploadType, string UserId);
        Common_NECN_MainModel GetDetailsById(string UploadType, string UploadID, string UserId);
        Tuple<int, string> Common_NECN_CRUD(Common_NECN_MainModel model);
        int DeleteUploadedDocument(string DocId, string UploadID);
        Common_NECN_DisplayListModel GetNewAndOld_NECNList(string UserId, string UserRole, string UploadType);
    }
}
