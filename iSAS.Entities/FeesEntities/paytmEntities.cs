using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.FeesEntities
{
   public class paytmEntities
    {
        public string tnxId { get; set; }
        public string orderId { get; set; }
        public string customerId { get; set; }
        public string trackingId { get; set; }
        public string bankRefNo { get; set; }
        public string paymentStatus { get; set; }
        public string statusMsg { get; set; }
        public string failureMsg { get; set; }
        public string cardName { get; set; }
        public string statusCode { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }
        public string paymentMode { get; set; }
        public string billingId { get; set; }
        public string billingName { get; set; }
        public string billingEmail { get; set; }
        public string billingTel { get; set; }
        public string userId { get; set; }
        public DateTime paymentSuccessOn { get; set; }
        public bool isManualResponse { get; set; }
        public string tnxRemark { get; set; }
        public string sRemark { get; set; }
        public string transRefNo { get; set; }
        public string tnxBank { get; set; }
    }
    public class Responsedata
    {
        public string CHECKSUMHASH { get; set; }
        public string TXNID { get; set; }
        public string ORDERID { get; set; }
        public string BANKTXNID { get; set; }
        public string TXNAMOUNT { get; set; }
        public string CURRENCY { get; set; }
        public string STATUS { get; set; }
        public string RESPCODE { get; set; }
        public string RESPMSG { get; set; }
        public string TXNDATE { get; set; }
        public string GATEWAYNAME { get; set; }
        public string BANKNAME { get; set; }
        public string PAYMENTMODE { get; set; }
        public string MID { get; set; }
    }
    public class StatusResponsedata
    {
        public string TXNID { get; set; }
        public string ORDERID { get; set; }
        public string BANKTXNID { get; set; }
        public string TXNAMOUNT { get; set; }
        public string CURRENCY { get; set; }
        public string STATUS { get; set; }
        public string TXNTYPE { get; set; }
        public string RESPCODE { get; set; }
        public string RESPMSG { get; set; }
        public string TXNDATE { get; set; }
        public string GATEWAYNAME { get; set; }
        public string BANKNAME { get; set; }
        public string PAYMENTMODE { get; set; }
        public string MID { get; set; }
        public string REFUNDAMT { get; set; }
        public string MERC_UNQ_REF { get; set; }
    }
}

