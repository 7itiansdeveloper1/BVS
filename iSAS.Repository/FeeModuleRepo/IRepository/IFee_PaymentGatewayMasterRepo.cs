using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.FeesEntities;
namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_PaymentGatewayMasterRepo
    {
        DataTable GetPaymentGateway(string module);
        Fee_PaymentGatewayMasterModel PaymentGatewayMasterTranasction(string moduleName, string mode);
        Tuple<int, string> GatewayTransaction_CRUD(string tnxId, string customerId, string userId, decimal amount, string paymentMode, string paymentStatus, string duedate);
        Tuple<int, string> GatewayTransaction_CRUD(paytmEntities paytmEntities);
        Tuple<int, string> GatewayTransaction_CRUD(paymentResponse_CCAvenue ccAvenueEntities);
        //paytmEntities GatewayTransaction_Transaction(string tnxId);
        paytmEntities GatewayTransaction_Transaction(string tnxId, string mode);
        paymentStatus GatewayTransaction_Transaction_V1(string tnxId, string mode);
        Tuple<int, string> GatewayTransaction_CRUD__STATUSAPI(paymentResponse_CCAvenue ccAvenueEntities);
    }
}
