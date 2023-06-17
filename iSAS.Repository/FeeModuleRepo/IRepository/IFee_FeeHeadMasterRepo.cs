using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_FeeHeadMasterRepo
    {
        List<Fee_FeeHeadMasterModels> GetFeeHeadMasterList(string HeadID);
        Fee_FeeHeadMasterModels GetFeeHeadByHeadID(string HeadId);
        Tuple<int, string> Fee_FeeHeadMaster_CRUD(Fee_FeeHeadMasterModels model);
        Tuple<int, string> Fee_FeeHeadMaster_CRUD(string HeadId);

        /// <summary>
        /// Used For Update Print Order
        /// </summary>
        /// <param name="HeadId"></param>
        /// <param name="PrintOrder"></param>
        /// <returns></returns>
        Tuple<int, string> Fee_FeeHeadMaster_CRUD(string HeadId, int PrintOrder, string UserId);
    }
}
