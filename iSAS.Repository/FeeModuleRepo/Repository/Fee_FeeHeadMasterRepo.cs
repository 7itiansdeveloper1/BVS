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
    public class Fee_FeeHeadMasterRepo : IFee_FeeHeadMasterRepo
    {
        public List<Fee_FeeHeadMasterModels> GetFeeHeadMasterList(string HeadID)
        {
            List<Fee_FeeHeadMasterModels> feeHeadList = new List<Fee_FeeHeadMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeHeadMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HeadId", HeadID);
                cmd.Parameters.AddWithValue("@QueryFor", "GetHeadList");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                feeHeadList = ds.Tables[0].AsEnumerable().Select(r => new Fee_FeeHeadMasterModels
                {
                    Annual = r.Field<bool>("Annual"),
                    HeadAlias = r.Field<string>("HeadAlias"),
                    HeadID = r.Field<string>("HeadID"),
                    HeadName = r.Field<string>("HeadName"),
                    Installment = r.Field<bool>("Installment"),
                    IsActive = r.Field<bool>("IsActive"),
                    IsAdjustable = r.Field<bool>("IsAdjustable"),
                    IsAdvanceHead = r.Field<bool>("IsAdvanceHead"),
                    IsConcessionHead = r.Field<bool>("IsConcessionHead"),
                    IsDeletetable = r.Field<bool>("IsDeletetable"),
                    IsDiscountHead = r.Field<bool>("IsDiscountHead"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    IsFineEnableHead = r.Field<bool>("IsFineEnableHead"),
                    IsFineHead = r.Field<bool>("IsFineHead"),
                    IsMainHead = r.Field<bool>("IsMainHead"),
                    IsMiscHead = r.Field<bool>("IsMiscHead"),
                    IsNonRefundable = r.Field<bool>("IsNonRefundable"),
                    IsTransportHead = r.Field<bool>("IsTransportHead"),
                    NewAdm = r.Field<bool>("NewAdm"),
                    PrintOrder = r.Field<short>("PrintOrder"),
                    AdjustmentOrder = r.Field<short>("AdjustmentOrder"),
                    IsOpenBalanceHead = r.Field<bool>("IsOpenBalanceHead"),
                }).ToList();
            }
            return feeHeadList;
        }

        public Fee_FeeHeadMasterModels GetFeeHeadByHeadID(string HeadId)
        {
            return GetFeeHeadMasterList(HeadId).FirstOrDefault();
        }

        public Tuple<int, string> Fee_FeeHeadMaster_CRUD(Fee_FeeHeadMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeHeadMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeadId", model.HeadID);
                cmd.Parameters.AddWithValue("@HeadName", model.HeadName);
                cmd.Parameters.AddWithValue("@HeadAlias", model.HeadAlias);
                cmd.Parameters.AddWithValue("@NewAdm", model.NewAdm);
                cmd.Parameters.AddWithValue("@Installment", model.Installment);

                cmd.Parameters.AddWithValue("@Annual", model.Annual);
                cmd.Parameters.AddWithValue("@IsNonRefundable", model.IsNonRefundable);
                cmd.Parameters.AddWithValue("@IsAdjustable", model.IsAdjustable);
                cmd.Parameters.AddWithValue("@IsMainHead", model.IsMainHead);
                cmd.Parameters.AddWithValue("@IsFineEnableHead", model.IsFineEnableHead);

                cmd.Parameters.AddWithValue("@IsTransportHead", model.IsTransportHead);
                cmd.Parameters.AddWithValue("@IsAdvanceHead", model.IsAdvanceHead);
                cmd.Parameters.AddWithValue("@IsDiscountHead", model.IsDiscountHead);
                cmd.Parameters.AddWithValue("@IsConcessionHead", model.IsConcessionHead);
                cmd.Parameters.AddWithValue("@IsFineHead", model.IsFineHead);
                cmd.Parameters.AddWithValue("@IsMiscHead", model.IsMiscHead);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);

                cmd.Parameters.AddWithValue("@AdjustmentOrder", model.AdjustmentOrder);
                cmd.Parameters.AddWithValue("@IsOpenBalanceHead", model.IsOpenBalanceHead);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Fee_FeeHeadMaster_CRUD(string HeadId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeHeadMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeadId", HeadId);
                cmd.Parameters.AddWithValue("@HeadName", "");
                cmd.Parameters.AddWithValue("@HeadAlias", "");
                cmd.Parameters.AddWithValue("@NewAdm", "");
                cmd.Parameters.AddWithValue("@Installment", "");

                cmd.Parameters.AddWithValue("@Annual", "");
                cmd.Parameters.AddWithValue("@IsNonRefundable", "");
                cmd.Parameters.AddWithValue("@IsAdjustable", "");
                cmd.Parameters.AddWithValue("@IsMainHead", "");
                cmd.Parameters.AddWithValue("@IsFineEnableHead", "");

                cmd.Parameters.AddWithValue("@IsTransportHead", "");
                cmd.Parameters.AddWithValue("@IsAdvanceHead", "");
                cmd.Parameters.AddWithValue("@IsDiscountHead", "");
                cmd.Parameters.AddWithValue("@IsConcessionHead", "");
                cmd.Parameters.AddWithValue("@IsFineHead", "");
                cmd.Parameters.AddWithValue("@IsMiscHead", "");
                cmd.Parameters.AddWithValue("@PrintOrder", "");
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
        public Tuple<int, string> Fee_FeeHeadMaster_CRUD(string HeadId, int PrintOrder, string UserId) // Used For Update Print Order
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeHeadMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeadId", HeadId);
                cmd.Parameters.AddWithValue("@HeadName", "");
                cmd.Parameters.AddWithValue("@HeadAlias", "");
                cmd.Parameters.AddWithValue("@NewAdm", false);
                cmd.Parameters.AddWithValue("@Installment", false);

                cmd.Parameters.AddWithValue("@Annual", false);
                cmd.Parameters.AddWithValue("@IsNonRefundable", false);
                cmd.Parameters.AddWithValue("@IsAdjustable", false);
                cmd.Parameters.AddWithValue("@IsMainHead", false);
                cmd.Parameters.AddWithValue("@IsFineEnableHead", false);

                cmd.Parameters.AddWithValue("@IsTransportHead", false);
                cmd.Parameters.AddWithValue("@IsAdvanceHead", false);
                cmd.Parameters.AddWithValue("@IsDiscountHead", false);
                cmd.Parameters.AddWithValue("@IsConcessionHead", false);
                cmd.Parameters.AddWithValue("@IsFineHead", false);
                cmd.Parameters.AddWithValue("@IsMiscHead", false);
                cmd.Parameters.AddWithValue("@PrintOrder", PrintOrder);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", "UPDATE_PIRNTORDER");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
