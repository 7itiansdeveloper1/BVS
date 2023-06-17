using CCA.Util;
using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace ISas.Web.Controllers.Fee_Module
{
    public class PaymentGatewayController : Controller
    {

        private IFee_PaymentGatewayMasterRepo _paymentGatewayMasterRepo;
        public PaymentGatewayController(IFee_PaymentGatewayMasterRepo paymentGatewayMasterRepo)
        {
            this._paymentGatewayMasterRepo = paymentGatewayMasterRepo;
        }
        [EncryptedActionParameter]
        public ActionResult MakeOnlinepayment(string paymentMode, string erpno,string admno,string name,string cls,string mob, string amount, string duedate)
        {
            onlinePayment _onlinePayment = new onlinePayment();
            DataTable dt = new DataTable();
            dt = _paymentGatewayMasterRepo.GetPaymentGateway("FEE");
            if (dt.Rows.Count > 0)
            {
                _onlinePayment.paymentGateWay.merchantId = dt.Rows[0]["merchantId"].ToString();
                _onlinePayment.paymentGateWay.transactionURL = dt.Rows[0]["transactionURL"].ToString();
                _onlinePayment.paymentGateWay.merchant_KEY = dt.Rows[0]["merchant_KEY"].ToString();
                _onlinePayment.paymentGateWay.aesEncryptRequestKey = dt.Rows[0]["aesEncryptRequestKey"].ToString();
                _onlinePayment.paymentGateWay.encodedURL = dt.Rows[0]["encodedURL"].ToString();
                _onlinePayment.paymentGateWay.cancelURL = dt.Rows[0]["cancelURL"].ToString();
                _onlinePayment.paymentGateWay.responseURL = dt.Rows[0]["responseURL"].ToString();
                _onlinePayment.paymentGateWay.aesEncryptResponseKey = dt.Rows[0]["aesEncryptResponseKey"].ToString();
            }
            if (String.IsNullOrEmpty(_onlinePayment.paymentGateWay.transactionURL))
            {
                return Json(new { status = "failed", Msg = "Payment gateway is not integrted, Please contact to system administrator. ", Color = "Warning" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Guid OrderID = Guid.NewGuid();
                string tnxId = "";
                tnxId = erpno + DateTime.Now.ToString("yyyymmddHHmmss");
                Tuple<int, string> res = _paymentGatewayMasterRepo.GatewayTransaction_CRUD(tnxId, erpno, Session["UserId"].ToString(), Convert.ToDecimal(amount), paymentMode, "Pending", duedate);
                if (res.Item1 == 1)
                {
                    CCACrypto ccaCrypto = new CCACrypto();
                    string encodedURL = _onlinePayment.paymentGateWay.encodedURL;
                    encodedURL = encodedURL.Replace("[merchant_id]", _onlinePayment.paymentGateWay.merchantId);
                    encodedURL = encodedURL.Replace("[order_id]", tnxId);
                    encodedURL = encodedURL.Replace("[amount]", amount);
                    encodedURL = encodedURL.Replace("[redirect_url]", _onlinePayment.paymentGateWay.responseURL);
                    encodedURL = encodedURL.Replace("[cancel_url]", _onlinePayment.paymentGateWay.cancelURL);
                    encodedURL = encodedURL.Replace("[udf1]", erpno);
                    encodedURL = encodedURL.Replace("[udf2]", admno);
                    encodedURL = encodedURL.Replace("[udf3]", name);
                    encodedURL = encodedURL.Replace("[udf4]", cls);
                    encodedURL = encodedURL.Replace("[udf5]", mob);
                    _onlinePayment.paymentGateWay.encryptedURL = ccaCrypto.Encrypt(encodedURL, _onlinePayment.paymentGateWay.aesEncryptRequestKey);
                }

            }
            return View(_onlinePayment);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Response_()
        {
            DataTable dt = new DataTable();
            dt = _paymentGatewayMasterRepo.GetPaymentGateway("FEE");
            string workingKey = "";
            if (dt.Rows.Count>0)
            workingKey = dt.Rows[0]["aesEncryptRequestKey"].ToString();//put in the 32bit alpha numeric key in the quotes provided here
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);

            paymentResponse_CCAvenue _onlinePayment = responseModel(encResponse);
            if (_onlinePayment.order_id != null)
            {
                Tuple<int, string> res = _paymentGatewayMasterRepo.GatewayTransaction_CRUD(_onlinePayment);
                if (_onlinePayment.order_status== "Success")
                {
                    return RedirectToAction("SuccessPayment", "PaymentGateway", new { Orderid = _onlinePayment.order_id});
                    //return RedirectToAction("SuccessPayment", "PaymentGateway", new
                    //{
                    //    transactionid = _onlinePayment.order_id,
                    //    paymentmode = _onlinePayment.payment_mode,
                    //    bankrefrenceno = _onlinePayment.bank_ref_no,
                    //    banktransid = _onlinePayment.tracking_id,
                    //    amount = _onlinePayment.amount,
                    //    status = _onlinePayment.order_status,
                    //    transdatetime = _onlinePayment.trans_date,
                    //    paymentmessage = _onlinePayment.status_message
                    //});
                }
                else
                {
                    //return RedirectToAction("Failed", "PaymentGateway", new
                    //{
                    //    transactionid = _onlinePayment.order_id,
                    //    paymentmode = _onlinePayment.payment_mode,
                    //    bankrefrenceno = _onlinePayment.bank_ref_no,
                    //    amount = _onlinePayment.amount,
                    //    status = _onlinePayment.order_status,
                    //    transdatetime = _onlinePayment.trans_date,
                    //    paymentmessage = _onlinePayment.failure_message
                    //});

                    return RedirectToAction("Failed", "PaymentGateway", new { orderid = _onlinePayment.order_id });
                }
            }
            else
            {
                return RedirectToAction("Failed", "PaymentGateway", new {orderid = _onlinePayment.order_id});
            }

        }
        //[EncryptedActionParameter]
        //public ActionResult SuccessPayment(string transactionid, string paymentmode, string bankrefrenceno, string banktransid, 
        //    string amount, string status, string transdatetime, string paymentmessage)
        //{
        //    paymentStatus _paymentStatus = new paymentStatus();
        //    _paymentStatus.transactionId = transactionid;
        //    _paymentStatus.paymentMode = paymentmode;
        //    _paymentStatus.bankRefrenceNo = bankrefrenceno;
        //    _paymentStatus.bankTransactionId = banktransid;
        //    _paymentStatus.amount = amount;
        //    _paymentStatus.status = status;
        //    _paymentStatus.transactionDateTime = transdatetime;
        //    _paymentStatus.paymentMessage = paymentmessage;
        //    //paytmEntities paytmEntities = _paymentGatewayMasterRepo.GatewayTransaction_Transaction(_paymentStatus, "SuccessPayment");
        //    return View(_paymentStatus);
        //}


        



        //public ActionResult Failed(string transactionid, string paymentmode, string bankrefrenceno, string banktransid, string amount, string status, string transdatetime, string paymentmessage)
        //{
        //    paymentStatus _paymentStatus = new paymentStatus();
        //    _paymentStatus.transactionId = transactionid;
        //    _paymentStatus.paymentMode = paymentmode;
        //    _paymentStatus.bankRefrenceNo = bankrefrenceno;
        //    _paymentStatus.bankTransactionId = banktransid;
        //    _paymentStatus.amount = amount;
        //    _paymentStatus.status = status;
        //    _paymentStatus.transactionDateTime = transdatetime;
        //    _paymentStatus.paymentMessage = paymentmessage;
        //    //paytmEntities paytmEntities = _paymentGatewayMasterRepo.GatewayTransaction_Transaction(Orderid, "Failed");
        //    return View(_paymentStatus);
        //}

        public ActionResult Cancel()
        {
            paymentStatus _paymentStatus = new paymentStatus();
            _paymentStatus.transactionId = "NA";
            _paymentStatus.paymentMode = "NA";
            _paymentStatus.bankRefrenceNo = "NA";
            _paymentStatus.bankTransactionId = "NA";
            _paymentStatus.amount = "0";
            _paymentStatus.status = "Cancelled";
            _paymentStatus.transactionDateTime = "NA";
            _paymentStatus.paymentMessage = "Transaction is cancelled...!";
            //paytmEntities paytmEntities = _paymentGatewayMasterRepo.GatewayTransaction_Transaction(Orderid, "Failed");
            return View(_paymentStatus);
        }

        [EncryptedActionParameter]
        public ActionResult SuccessPayment(string Orderid)
        {
            paymentStatus _paymentStatus = _paymentGatewayMasterRepo.GatewayTransaction_Transaction_V1(Orderid, "SuccessPayment");
            return View(_paymentStatus);
        }

        public ActionResult Failed(string Orderid)
        {
            paymentStatus _paymentStatus = _paymentGatewayMasterRepo.GatewayTransaction_Transaction_V1(Orderid, "Failed");
            return View(_paymentStatus);
        }

        
        public JsonResult StatusAPI(string Orderid,string trackingId)
        {
            onlinePayment _onlinePayment = new onlinePayment();
            paymentResponse_CCAvenue _onlinePaymentResponse;

            Orderid = "2019225620231130061141";
            trackingId = "112894948962";

            DataTable dt = _paymentGatewayMasterRepo.GetPaymentGateway("FEE");
            _onlinePayment.paymentGateWay.statusAPIURL = dt.Rows[0]["statusAPIURL"].ToString();
            _onlinePayment.paymentGateWay.aesEncryptRequestKey = dt.Rows[0]["aesEncryptRequestKey"].ToString();
            _onlinePayment.paymentGateWay.merchant_KEY = dt.Rows[0]["merchant_KEY"].ToString();
            string orderstatus = "";

            try
            {
                string accessCode = _onlinePayment.paymentGateWay.merchant_KEY;
                string workingKey = _onlinePayment.paymentGateWay.aesEncryptRequestKey;
                string orderStatusQueryJson = "{ \"reference_no\":\""+ trackingId + "\", \"order_no\":\""+ Orderid + "\" }"; 
                string encJson = "";
                string queryUrl = _onlinePayment.paymentGateWay.statusAPIURL;
                CCACrypto ccaCrypto = new CCACrypto();
                encJson = ccaCrypto.Encrypt(orderStatusQueryJson, workingKey);
                string authQueryUrlParam = "enc_request=" + encJson + "&access_code=" + accessCode + "&command=orderStatusTracker&request_type=JSON&response_type=JSON";
                String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);
                NameValueCollection param = getResponseMap(message);
                String status = "";
                String encResJson = "";
                if (param != null && param.Count == 2)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        if ("status".Equals(param.Keys[i]))
                        {
                            status = param[i];
                        }
                        if ("enc_response".Equals(param.Keys[i]))
                        {
                            encResJson = param[i];
                        }
                    }
                    if (!"".Equals(status) && status.Equals("0"))
                    {
                        String ResJson = ccaCrypto.Decrypt(encResJson, workingKey);
                        _onlinePaymentResponse = statusResponseModel_V1(ResJson);
                        Tuple<int, string> res = _paymentGatewayMasterRepo.GatewayTransaction_CRUD__STATUSAPI(_onlinePaymentResponse);
                        orderstatus = _onlinePaymentResponse.order_status;
                    }
                    else if (!"".Equals(status) && status.Equals("1"))
                    {
                        Console.WriteLine("failure response from ccAvenues: " + encResJson);
                        orderstatus = "Failed";
                    }


                    #region Testing Block
                    //if (!"".Equals(status) && status.Equals("0"))
                    //{
                    //    String ResJson = ccaCrypto.Decrypt(encResJson, workingKey);
                    //    Response.Write(ResJson);
                    //    orderstatus = ResJson;

                    //}
                    //else if (!"".Equals(status) && status.Equals("1"))
                    //{
                    //    Console.WriteLine("failure response from ccAvenues: " + encResJson);
                    //    orderstatus = encResJson;

                    //}
                    #endregion

                }

            }
            catch (Exception exp)
            {
                Response.Write("Exception " + exp);

            }

            return Json(orderstatus, JsonRequestBehavior.AllowGet);
        }

        private string postPaymentRequestToGateway(String queryUrl, String urlParam)
        {

            String message = "";
            try
            {
                StreamWriter myWriter = null;// it will open a http connection with provided url
                WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
                objRequest.Method = "POST";
                //objRequest.ContentLength = TranRequest.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(urlParam);//send data
                myWriter.Close();//closed the myWriter object
                ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls| SecurityProtocolType.Tls11| SecurityProtocolType.Tls12| SecurityProtocolType.Ssl3;
                // Getting Response
                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    message = sr.ReadToEnd();
                    //Response.Write(message);
                }
            }
            catch (Exception exception)
            {
                Console.Write("Exception occured while connection." + exception);
            }
            return message;

        }

        private NameValueCollection getResponseMap(String message)
        {
            NameValueCollection Params = new NameValueCollection();
            if (message != null || !"".Equals(message))
            {
                string[] segments = message.Split('&');
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        Params.Add(Key, Value);
                    }
                }
            }
            return Params;
        }

        private paymentResponse_CCAvenue responseModel(string encResponse)
        {
            paymentResponse_CCAvenue _responseModel = new paymentResponse_CCAvenue();
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }

            for (int i = 0; i < Params.Count; i++)
            {
                Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
            }
            _responseModel.order_id = Params["order_id"];
            _responseModel.tracking_id = Params["tracking_id"];
            _responseModel.bank_ref_no = Params["bank_ref_no"];
            _responseModel.order_status = Params["order_status"];
            _responseModel.failure_message = Params["failure_message"];
            _responseModel.payment_mode = Params["payment_mode"];
            _responseModel.card_name = Params["card_name"];
            _responseModel.status_code = Params["status_code"];
            _responseModel.status_message = Params["status_message"];
            _responseModel.currency = Params["currency"];
            _responseModel.billing_name = Params["billing_name"];
            _responseModel.billing_tel = Params["billing_tel"];
            _responseModel.billing_email = Params["billing_email"];
            _responseModel.merchant_param1 = Params["merchant_param1"];
            _responseModel.merchant_param2 = Params["merchant_param2"];
            _responseModel.merchant_param3 = Params["merchant_param3"];
            _responseModel.merchant_param4 = Params["merchant_param4"];
            _responseModel.merchant_param5 = Params["merchant_param5"];
            _responseModel.userId = Params["merchant_param1"]; ;
            _responseModel.bene_account = Params["bene_account"];
            _responseModel.trans_date = Params["trans_date"];
            _responseModel.bene_bank = Params["bene_bank"];
            _responseModel.amount = Params["amount"];
            return _responseModel;
        }

        private paymentResponse_CCAvenue statusResponseModel_V1(string jsonResponse)
        {

            dynamic data = JObject.Parse(jsonResponse);
            data = data.Order_Status_Result?.ToString();
            dynamic responseData = JObject.Parse(data);
            paymentResponse_CCAvenue _responseModel = new paymentResponse_CCAvenue();
            _responseModel.order_id = responseData.order_no;
            _responseModel.order_status = responseData.order_status;
            _responseModel.trans_date = responseData.order_status_date_time;
            return _responseModel;
        }
    }
}