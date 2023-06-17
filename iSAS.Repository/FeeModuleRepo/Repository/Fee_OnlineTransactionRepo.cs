using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_OnlineTransactionRepo: IFee_OnlineTransactionRepo
    {
        public Fee_OnlineTransactionModel OnlineSettlement_Transaction(string fromdate, string todate,string paymentstatus, string sessionid)
        {
            DataTable dt = new DataTable();
            Fee_OnlineTransactionModel model = new Fee_OnlineTransactionModel();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_OnlineTransaction_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fromDate", fromdate==null? DateTime.Now.Date : Convert.ToDateTime(fromdate).Date);
                cmd.Parameters.AddWithValue("@toDate", todate == null ? DateTime.Now.Date : Convert.ToDateTime(todate).Date);
                cmd.Parameters.AddWithValue("@paymentStatus", paymentstatus);
                cmd.Parameters.AddWithValue("@sessionId", sessionid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            if (dt.Rows.Count > 0)
            {
                model.onlineTransctionsList = dt.AsEnumerable().Select(r => new onlineTransctions
                {
                    isSelected = r.Field<bool>("isSelected"),
                    isEditable= r.Field<bool>("isEditable"),
                    isReadyForReceipt = r.Field<bool>("isReadyForReceipt"),
                    tnxId = r.Field<string>("tnxId"),
                    ERPNo = r.Field<string>("ERPNo"),
                    bankRefNo = r.Field<string>("bankRefNo"),
                    trackingId = r.Field<string>("trackingId"),
                    statusMessage = r.Field<string>("statusMessage"),
                    amount = r.Field<int>("amount"),
                    paymentMode = r.Field<string>("paymentMode"),
                    paymentSuccessOn = r.Field<string>("paymentSuccessOn"),
                    tnxBank = r.Field<string>("tnxBank"),
                    DueDate = r.Field<string>("DueDate"),
                    InstallName= r.Field<string>("InstallName"),
                    Student = r.Field<string>("Student"),
                    Father = r.Field<string>("Father"),
                    SMSNo = r.Field<string>("SMSNo"),
                    className = r.Field<string>("className"),
                    Recpt = r.Field<string>("Recpt"),
                }).ToList();
            }

            return model;
        }
        public Tuple<int, string> OnlineTransaction_CRUD(string transactionids, string sessionid, string recptdate, string userid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_OnlineTransaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TransactionIds", transactionids);
                cmd.Parameters.AddWithValue("@sessionId", sessionid);
                cmd.Parameters.AddWithValue("@ReceiptDate", Convert.ToDateTime(recptdate).Date);
                cmd.Parameters.AddWithValue("@userId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
