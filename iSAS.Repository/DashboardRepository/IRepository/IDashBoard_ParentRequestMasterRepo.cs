using ISas.Entities.DashboardEntities;
using System.Collections.Generic;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IDashBoard_ParentRequestMasterRepo
    {
        List<DashBoard_ParentRequestMasterModel> GetParentRequestMasterList(string UserID, string Category);

        string ParentRequestMaster_CRUD(DashBoard_ParentRequestMasterModel model);

        List<RequestCommunicationDetailsModel> GetCommunicationDetails(string RequestID, string UserID);
    }
}
