using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.FeesEntities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ISas.Repository.FeeModuleRepo.IRepository;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_PaymentGatewayMasterRepo: IFee_PaymentGatewayMasterRepo
    {
        public DataTable GetPaymentGateway(string module)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from fn_PaymentGateway('" + module + "')", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@moduleName", module);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
        public Fee_PaymentGatewayMasterModel PaymentGatewayMasterTranasction(string moduleName,string mode)
        {

            Fee_PaymentGatewayMasterModel model = new Fee_PaymentGatewayMasterModel();
            using ( SqlConnection con=new SqlConnection( ConfigurationManager.ConnectionStrings["iSASDB"].ToString()   ))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_PaymentGatewayMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@moduleName", moduleName);
                cmd.Parameters.AddWithValue("@Mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count>0)
                {
                    model.gatewayId = Convert.ToInt32(dt.Rows[0]["gatewayId"]);
                    model.gatewayName = dt.Rows[0]["gatewayName"].ToString();
                    model.merchantId = dt.Rows[0]["merchantId"].ToString();
                    model.merchant_KEY = dt.Rows[0]["merchant_KEY"].ToString();
                    model.responseURL = dt.Rows[0]["responseURL"].ToString();
                    model.transactionURL = dt.Rows[0]["transactionURL"].ToString();
                    model.statusAPIURL = dt.Rows[0]["statusAPIURL"].ToString();
                    model.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                    model.moduleName = dt.Rows[0]["moduleName"].ToString();
                }

            }

            return model;
        }
        public Tuple<int, string> GatewayTransaction_CRUD(string tnxId, string customerId, string userId, decimal amount, string paymentMode, string paymentStatus, string duedate)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GatewayTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tnxId", tnxId);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@paymentMode", paymentMode);
                cmd.Parameters.AddWithValue("@paymentStatus", paymentStatus);
                cmd.Parameters.AddWithValue("@dueDate", duedate);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> GatewayTransaction_CRUD(paytmEntities paytmEntities)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GatewayTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tnxId", paytmEntities.tnxId);
                cmd.Parameters.AddWithValue("@customerId", paytmEntities.customerId);
                cmd.Parameters.AddWithValue("@trackingId", paytmEntities.trackingId);
                cmd.Parameters.AddWithValue("@bankRefNo", paytmEntities.bankRefNo);
                cmd.Parameters.AddWithValue("@paymentStatus", paytmEntities.paymentStatus);
                cmd.Parameters.AddWithValue("@statusMsg", paytmEntities.statusMsg);
                cmd.Parameters.AddWithValue("@failureMsg", paytmEntities.failureMsg);
                cmd.Parameters.AddWithValue("@cardName", paytmEntities.cardName);
                cmd.Parameters.AddWithValue("@statusCode", paytmEntities.statusCode);
                cmd.Parameters.AddWithValue("@currency", paytmEntities.currency);
                cmd.Parameters.AddWithValue("@amount", paytmEntities.amount);
                cmd.Parameters.AddWithValue("@paymentMode", paytmEntities.paymentMode);
                cmd.Parameters.AddWithValue("@billingId", paytmEntities.billingId);
                cmd.Parameters.AddWithValue("@billingName", paytmEntities.billingName);
                cmd.Parameters.AddWithValue("@billingEmail", paytmEntities.billingEmail);
                cmd.Parameters.AddWithValue("@billingTel", paytmEntities.billingTel);
                cmd.Parameters.AddWithValue("@userId", paytmEntities.userId);
                cmd.Parameters.AddWithValue("@paymentSuccessOn", paytmEntities.paymentSuccessOn);
                cmd.Parameters.AddWithValue("@tnxRemark", paytmEntities.tnxRemark);
                cmd.Parameters.AddWithValue("@transRefNo", paytmEntities.transRefNo);
                cmd.Parameters.AddWithValue("@tnxBank", paytmEntities.tnxBank);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> GatewayTransaction_CRUD(paymentResponse_CCAvenue ccAvenueEntities)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GatewayTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tnxId", ccAvenueEntities.order_id);
                cmd.Parameters.AddWithValue("@customerId", ccAvenueEntities.merchant_param1);
                cmd.Parameters.AddWithValue("@trackingId", ccAvenueEntities.tracking_id);
                cmd.Parameters.AddWithValue("@bankRefNo", ccAvenueEntities.bank_ref_no);
                cmd.Parameters.AddWithValue("@paymentStatus", ccAvenueEntities.order_status);
                cmd.Parameters.AddWithValue("@statusMsg", ccAvenueEntities.status_message);
                cmd.Parameters.AddWithValue("@failureMsg", ccAvenueEntities.failure_message);
                cmd.Parameters.AddWithValue("@cardName", ccAvenueEntities.card_name);
                cmd.Parameters.AddWithValue("@statusCode", ccAvenueEntities.status_code);
                cmd.Parameters.AddWithValue("@currency", ccAvenueEntities.currency);
                cmd.Parameters.AddWithValue("@amount", ccAvenueEntities.amount);
                cmd.Parameters.AddWithValue("@paymentMode", ccAvenueEntities.payment_mode);
                cmd.Parameters.AddWithValue("@billingId", ccAvenueEntities.merchant_param1);
                cmd.Parameters.AddWithValue("@billingName", ccAvenueEntities.billing_name);
                cmd.Parameters.AddWithValue("@billingEmail", ccAvenueEntities.billing_email);
                cmd.Parameters.AddWithValue("@billingTel", ccAvenueEntities.billing_tel);
                cmd.Parameters.AddWithValue("@userId", ccAvenueEntities.userId);
                cmd.Parameters.AddWithValue("@paymentSuccessOn", Convert.ToDateTime(ccAvenueEntities.trans_date));
                cmd.Parameters.AddWithValue("@tnxRemark",  ccAvenueEntities.bank_ref_no);
                cmd.Parameters.AddWithValue("@transRefNo", ccAvenueEntities.bene_account);
                cmd.Parameters.AddWithValue("@tnxBank", ccAvenueEntities.bene_bank);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> GatewayTransaction_CRUD__STATUSAPI(paymentResponse_CCAvenue ccAvenueEntities)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GatewayTransaction_CRUD_STATUSAPI", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tnxId", ccAvenueEntities.order_id);
                cmd.Parameters.AddWithValue("@order_status", ccAvenueEntities.order_status);
                cmd.Parameters.AddWithValue("@order_bank_response", ccAvenueEntities.status_message);
                cmd.Parameters.AddWithValue("@order_status_date_time", ccAvenueEntities.trans_date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public paytmEntities GatewayTransaction_Transaction(string tnxId,string mode)
        {

            paytmEntities model = new paytmEntities();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GatewayTransaction_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tnxId", tnxId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    model.trackingId = dt.Rows[0]["trackingId"].ToString();
                    model.tnxId = dt.Rows[0]["tnxId"].ToString();
                    if (mode== "SuccessPayment")
                    model.paymentSuccessOn = Convert.ToDateTime(dt.Rows[0]["paymentSuccessOn"]);
                }
            }

            return model;
        }

        public paymentStatus GatewayTransaction_Transaction_V1(string tnxId, string mode)
        {
            paymentStatus _paymentStatus = new paymentStatus();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GatewayTransaction_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tnxId", tnxId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    _paymentStatus.transactionId = dt.Rows[0]["tnxId"].ToString();
                    _paymentStatus.paymentMode = dt.Rows[0]["paymentMode"].ToString();
                    _paymentStatus.bankRefrenceNo = dt.Rows[0]["bankRefNo"].ToString();
                    _paymentStatus.bankTransactionId = dt.Rows[0]["trackingId"].ToString();
                    _paymentStatus.amount = dt.Rows[0]["amount"].ToString();
                    _paymentStatus.status = dt.Rows[0]["paymentStatus"].ToString();
                    _paymentStatus.transactionDateTime = Convert.ToDateTime(dt.Rows[0]["paymentSuccessOn"]).ToString();
                    _paymentStatus.paymentMessage = dt.Rows[0]["paymentMessage"].ToString();
                }
            }

            return _paymentStatus;
        }


    }

}
