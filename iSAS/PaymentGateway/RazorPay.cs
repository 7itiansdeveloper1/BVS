using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Razorpay;

namespace ISas.Web.PaymentGateway
{
    public class RazorPay
    {
        
        public static razorPayEntinties MakePaymentByRazor(string tnxId, string amount,string keyid,string keysecret)
        {
            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(keyid, keysecret);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", Convert.ToInt32(amount) * 100);  // Amount will in paise
            options.Add("receipt", tnxId);
            options.Add("currency", "INR");
            //options.Add("payment_capture", "1"); // 1 - automatic  , 2 - manual //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();

            razorPayEntinties razorpayentinties = new razorPayEntinties
            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = keyid,
                amount = Convert.ToInt32(amount) * 100,
                currency = "INR",
                billingName = "Mahesh Kumar",
                billingEmail= "7itians@gmail.com",
                billingTel= "9891659669",
                billingAddress = "B-1268, New Ashok Nagar",
                tnxRemark = "Testing description"
            };
            return razorpayentinties;
        }
        public class razorPayEntinties
        {
            public string orderId { get; set; }
            public string razorpayKey { get; set; }
            public string currency { get; set; }
            public int amount { get; set; }
            public string billingId { get; set; }
            public string billingName { get; set; }
            public string billingEmail { get; set; }
            public string billingTel { get; set; }
            public string billingAddress { get; set; }
            public string tnxRemark { get; set; }
        }
    }

    

}