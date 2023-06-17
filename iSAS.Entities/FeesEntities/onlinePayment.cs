using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.FeesEntities
{
    public class onlinePayment
    {
        public onlinePayment()
        {
            paymentGateWay = new paymentGateWay();
            paymentResponse = new paymentResponse();
            paymentStatus = new paymentStatus();
            paymentResponseCCAvenue = new paymentResponse_CCAvenue();
        }
        public paymentGateWay paymentGateWay { get; set; }
        public paymentResponse paymentResponse { get; set; }
        public paymentResponse_CCAvenue paymentResponseCCAvenue { get; set; }
        public paymentStatus paymentStatus { get; set; }
    }

    public class paymentResponse_CCAvenue
    {
        public string order_id { get; set; }
        public string tracking_id { get; set; }
        public string trans_date { get; set; }
        public string bank_ref_no { get; set; }
        public string order_status { get; set; }
        public string failure_message { get; set; }
        public string payment_mode { get; set; }
        public string card_name { get; set; }
        public string status_code { get; set; }
        public string status_message { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string billing_name { get; set; }
        public string billing_tel { get; set; }
        public string billing_email { get; set; }
        public string merchant_param1 { get; set; }
        public string merchant_param2 { get; set; }
        public string merchant_param3 { get; set; }
        public string merchant_param4 { get; set; }
        public string merchant_param5 { get; set; }
        public string userId { get; set; }
        public string bene_bank { get; set; }
        public string bene_account { get; set; }
        public string duedate { get; set; }

    }

    public class paymentGateWay
    {
        public string gatewayId { get; set; }
        public string gatewayName { get; set; }
        public string merchantId { get; set; }
        public string merchant_KEY { get; set; }
        public string responseURL { get; set; }
        public string cancelURL { get; set; }
        public string transactionURL { get; set; }
        public string encodedURL { get; set; }
        public string encryptedURL { get; set; }
        public string statusAPIURL { get; set; }
        public string bankId { get; set; }
        public string loginId { get; set; }
        public string loginPassword { get; set; }
        public string requestHashKey { get; set; }
        public string responseHashKey { get; set; }
        public string customerCode { get; set; }
        public string clientCode { get; set; }
        public string clientProductId { get; set; }
        public string requestIVSet { get; set; }
        public string responseIVSet { get; set; }
        public string aesEncryptRequestKey { get; set; }
        public string aesEncryptResponseKey { get; set; }



    

    }

    public class paymentResponse
    {
        public string mmp_txn { get; set; }
        public string mer_txn { get; set; }
        public string amt { get; set; }
        public string prod { get; set; }
        public string date { get; set; }
        public string bank_txn { get; set; }
        public string f_code { get; set; }
        public string clientcode { get; set; }
        public string bank_name { get; set; }
        public string discriminator { get; set; }
        public string desc { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string udf6 { get; set; }
        public string udf9 { get; set; }
        public string udf10 { get; set; }
        public string udf11 { get; set; }
        public string ipg_txn_id { get; set; }
        public string surcharge { get; set; }
        public string CardNumber { get; set; }
        public string signature { get; set; }
    }

    public class paymentStatus
    {
        public string transactionId { get; set; }
        public string amount { get; set; }
        public string bankRefrenceNo { get; set; }
        public string bankTransactionId { get; set; }
        public string paymentMode { get; set; }
        public string status { get; set; }
        public string transactionDateTime { get; set; }
        public string paymentMessage { get; set; }
    }
}
