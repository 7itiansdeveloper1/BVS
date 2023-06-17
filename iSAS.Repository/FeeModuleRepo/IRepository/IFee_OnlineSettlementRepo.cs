using System;
using ISas.Entities.FeesEntities;
using System.Data;
    

namespace ISas.Repository.FeeModuleRepo.IRepository
{
   public interface IFee_OnlineSettlementRepo
    {
        Fee_OnlineSettlementEntities OnlineSettlement_Transaction(string fromdate, string todate, string sessionid);
        Tuple<int, string> OnlineSettlement_CRUD(DataTable sdt, string sessionid);
    }
}
