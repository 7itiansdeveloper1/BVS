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
    public class Fee_ConcessionPolicyRepo : IFee_ConcessionPolicyRepo
    {
        public List<SelectListItem> GetUserRoleList()
        {
            List<SelectListItem> userRoleList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionPolicy_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetUserRoleList");
                cmd.Parameters.AddWithValue("@ConcId", "");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                userRoleList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("URoleName"),
                    Value = Convert.ToString(r.Field<int>("URoleId")),
                }).ToList();
            }
            return userRoleList;
        }

        public List<Fee_ConcessionPolicyModels> GetConcessionPolicyList(string ConcId)
        {
            List<Fee_ConcessionPolicyModels> concessionPolicyList = new List<Fee_ConcessionPolicyModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionPolicy_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "ConcessionPolicyList");
                cmd.Parameters.AddWithValue("@ConcId", ConcId);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                concessionPolicyList = ds.Tables[0].AsEnumerable().Select(r => new Fee_ConcessionPolicyModels
                {
                    Active = r.Field<bool>("Active"),
                    ConcCategory = r.Field<string>("ConcCategory"),
                    ConcId = r.Field<string>("ConcId"),
                    ConcName = r.Field<string>("ConcName"),
                    DefaultPer = r.Field<int>("DefaultPer"),
                    IsDefault = r.Field<bool>("IsDefault"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    WorkProcess = r.Field<bool>("WorkProcess"),
                    WorkProcessBy = r.Field<int>("WorkProcessBy"),
                }).ToList();
            }
            return concessionPolicyList;
        }

        public Fee_ConcessionPolicyModels GetConcessionPolicyById(string ConcId)
        {
            return GetConcessionPolicyList(ConcId).FirstOrDefault();
        }

        public string Fee_ConcessionPolicy_CRUD(Fee_ConcessionPolicyModels model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionPolicy_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Conc_Id", model.ConcId);
                cmd.Parameters.AddWithValue("@Conc_Name", model.ConcName);
                cmd.Parameters.AddWithValue("@Conc_Category", model.ConcCategory);
                cmd.Parameters.AddWithValue("@IsDefault", model.IsDefault);
                cmd.Parameters.AddWithValue("@DefaultPer", model.DefaultPer);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@IsActive", model.Active);
                cmd.Parameters.AddWithValue("@WorkProcess", model.WorkProcess);
                cmd.Parameters.AddWithValue("@WorkProcessBy", model.WorkProcessBy);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][1].ToString();
                return message;
            }
        }
        public string Fee_ConcessionPolicy_CRUD(string ConcId)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_ConcessionPolicy_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Conc_Id", ConcId);
                cmd.Parameters.AddWithValue("@Conc_Name", "");
                cmd.Parameters.AddWithValue("@Conc_Category","");
                cmd.Parameters.AddWithValue("@IsDefault", "");
                cmd.Parameters.AddWithValue("@DefaultPer","");
                cmd.Parameters.AddWithValue("@PrintOrder", "");
                cmd.Parameters.AddWithValue("@IsActive", "");
                cmd.Parameters.AddWithValue("@WorkProcess", "");
                cmd.Parameters.AddWithValue("@WorkProcessBy","");

                cmd.Parameters.AddWithValue("@UserId", "");
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][1].ToString();
                return message;
            }
        }
    }
}
