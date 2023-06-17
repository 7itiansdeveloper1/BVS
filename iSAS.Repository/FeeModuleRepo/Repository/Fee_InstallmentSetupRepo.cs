using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_InstallmentSetupRepo : IFee_InstallmentSetupRepo
    {
        public List<Fee_InstallmentSetupModels> GetInstallmetsByStructId(string StructId, string InstallId, string sessId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_InstallmentMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@QueryFor", "GetStructureList");
                cmd.Parameters.AddWithValue("@InstallId", InstallId);
                cmd.Parameters.AddWithValue("@SessionId", sessId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Fee_InstallmentSetupModels
                {
                    Active = r.Field<bool>("Active"),
                    InstallAlias = r.Field<string>("InstallAlias"),
                    InstallDueDate = r.Field<string>("InstallDueDate"),
                    InstallFirstDate = r.Field<string>("InstallFirstDate"),
                    InstallId = r.Field<string>("InstallId"),
                    InstallLastDate = r.Field<string>("InstallLastDate"),
                    InstallName = r.Field<string>("InstallName"),
                    IsDeletetable = r.Field<bool>("IsDeletetable"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    StructId = r.Field<string>("StructId"),
                    StrectureName = r.Field<string>("StrectureName"),
                    FineStartDate = r.Field<string>("FineStartDate"),
                }).ToList();
            }
        }
        public Tuple<int, string> Fee_InstallmentSetup_CRUD(Fee_InstallmentSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_InstallmentMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstallId", model.InstallId);
                cmd.Parameters.AddWithValue("@InstallName", model.InstallName);
                cmd.Parameters.AddWithValue("@InstallAlias", model.InstallAlias);

                if (!string.IsNullOrEmpty(model.InstallDueDate))
                    cmd.Parameters.AddWithValue("@InstallDueDate", Convert.ToDateTime(model.InstallDueDate).Date);

                if (!string.IsNullOrEmpty(model.FineStartDate))
                    cmd.Parameters.AddWithValue("@FineStartDate", Convert.ToDateTime(model.FineStartDate).Date);

                cmd.Parameters.AddWithValue("@StructId", model.StructId);

                if (!string.IsNullOrEmpty(model.InstallFirstDate))
                    cmd.Parameters.AddWithValue("@FirstDate", Convert.ToDateTime(model.InstallFirstDate).Date);

                if (!string.IsNullOrEmpty(model.InstallLastDate))
                    cmd.Parameters.AddWithValue("@LastDate", Convert.ToDateTime(model.InstallLastDate).Date);

                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@IsActive", model.Active);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);
                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Fee_InstallmentSetup_CRUD(string InstallId, string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_InstallmentMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InstallId", InstallId);
                cmd.Parameters.AddWithValue("@InstallName", "");
                cmd.Parameters.AddWithValue("@InstallAlias", "");
                cmd.Parameters.AddWithValue("@InstallDueDate", "");
                cmd.Parameters.AddWithValue("@StructId", "");
                cmd.Parameters.AddWithValue("@FirstDate", "");
                cmd.Parameters.AddWithValue("@LastDate", "");
                cmd.Parameters.AddWithValue("@PrintOrder", "");
                cmd.Parameters.AddWithValue("@IsActive", "");
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public List<SelectListItem> InstallmetDropdwon_ByStrectureId(string StrectureId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FeeBill_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FeeStructId", StrectureId);
                cmd.Parameters.AddWithValue("@Mode", "GetInstallmentList");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("InstallName"),
                    Value = r.Field<string>("InstallDueDate"),
                }).ToList();
            }
        }
    }
}
