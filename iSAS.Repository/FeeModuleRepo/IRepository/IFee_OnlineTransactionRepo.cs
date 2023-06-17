using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_OnlineTransactionRepo
    {
        Fee_OnlineTransactionModel OnlineSettlement_Transaction(string fromdate, string todate, string paymentstatus, string sessionid);
        Tuple<int, string> OnlineTransaction_CRUD(string transactionids, string sessionid, string recptdate, string userid);
    }
}
