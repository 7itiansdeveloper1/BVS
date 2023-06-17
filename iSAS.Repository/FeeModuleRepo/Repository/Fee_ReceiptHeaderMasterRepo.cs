using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_ReceiptHeaderMasterRepo : IFee_ReceiptHeaderMasterRepo
    {
        public List<Fee_ReceiptHeaderMasterModels> GetFee_ReceiptHeaderMasterList(string HeaderId)
        {
            List<Fee_ReceiptHeaderMasterModels> headerList = new List<Fee_ReceiptHeaderMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ReceiptHeaderMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HeaderId", HeaderId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                headerList = ds.Tables[0].AsEnumerable().Select(r => new Fee_ReceiptHeaderMasterModels
                {
                    Header1 = r.Field<string>("Header1"),
                    Header2 = r.Field<string>("Header2"),
                    Header3 = r.Field<string>("Header3"),
                    Header4 = r.Field<string>("Header4"),
                    HeaderFor = r.Field<string>("HeaderFor"),
                    HeaderId = r.Field<string>("HeaderId"),
                    Logo = r.Field<string>("Logo"),
                    Prefix = r.Field<string>("Prefix"),
                    ReceiptStartNo = r.Field<int>("ReceiptStartNo"),
                    UsePrefix = r.Field<bool>("UsePrefix"),
                    IsEnable = r.Field<bool>("IsEnable"),
                }).ToList();
            }
            return headerList;
        }
        public Fee_ReceiptHeaderMasterModels Fee_ReceiptHeaderMasterById(string HeaderId)
        {
            return GetFee_ReceiptHeaderMasterList(HeaderId).FirstOrDefault();
        }
        public Tuple<int, string> Fee_ReceiptHeaderMaster_CRUD(Fee_ReceiptHeaderMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ReceiptHeaderMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeaderId", model.HeaderId);
                cmd.Parameters.AddWithValue("@Header1", model.Header1);
                cmd.Parameters.AddWithValue("@Header2", model.Header2);
                cmd.Parameters.AddWithValue("@Header3", model.Header3);
                cmd.Parameters.AddWithValue("@Header4", model.Header4);

                cmd.Parameters.AddWithValue("@Logo", model.Logo);
                cmd.Parameters.AddWithValue("@UsePrefix", model.UsePrefix);
                cmd.Parameters.AddWithValue("@Prefix", model.Prefix);
                cmd.Parameters.AddWithValue("@ReceiptStartNo", model.ReceiptStartNo);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);
                cmd.Parameters.AddWithValue("@HeaderFor", model.HeaderFor);
                cmd.Parameters.AddWithValue("@IsEnable", model.IsEnable);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Fee_ReceiptHeaderMaster_CRUD(string HeaderId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ReceiptHeaderMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeaderId", HeaderId);

                cmd.Parameters.AddWithValue("@UserId", "");
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }



        #region Wing Header Setup
        public List<Fee_WingHeaderSetupModel> WingHeaderSetupDetailsList(string WingId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeReceipt_ChildForm_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WingId", WingId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Fee_WingHeaderSetupModel
                {
                    Header1 = r.Field<string>("Header1"),
                    Header2 = r.Field<string>("Header2"),
                    Header3 = r.Field<string>("Header3"),
                    Header4 = r.Field<string>("Header4"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    Logo = r.Field<string>("Logo"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    WingID = r.Field<string>("WingID"),
                    WingName = r.Field<string>("WingName"),
                }).ToList();
            }
        }
        public Fee_WingHeaderSetupModel WingHeaderSetupDetails(string WingId)
        {
            return WingHeaderSetupDetailsList(WingId).FirstOrDefault();
        }
        public Tuple<int, string> Fee_WingHeaderSetup_CRUD(Fee_WingHeaderSetupModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeReceipt_ChildForm_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODE", model.CRUDMode);
                cmd.Parameters.AddWithValue("@WingID", model.WingID);
                cmd.Parameters.AddWithValue("@Header1", model.Header1);
                cmd.Parameters.AddWithValue("@Header2", model.Header2);
                cmd.Parameters.AddWithValue("@Header3", model.Header3);
                cmd.Parameters.AddWithValue("@Header4", model.Header4);
                cmd.Parameters.AddWithValue("@Logo", model.Logo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Fee_WingHeaderSetup_CRUD(string WingId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeReceipt_ChildForm_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MODE", "DELETE");
                cmd.Parameters.AddWithValue("@WingID", WingId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion
    }
}
