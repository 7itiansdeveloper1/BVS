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
    public class Fee_FinePolicyRepo : IFee_FinePolicyRepo
    {
        public List<Fee_FinePolicyModels> GetFinePolicyList(string StructId, string StructName, int PolicyId)
        {
            List<Fee_FinePolicyModels> policyList = new List<Fee_FinePolicyModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FinePolicy_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@QueryFor", "GetFinePolicy");
                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@PolicyId", PolicyId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                policyList = ds.Tables[0].AsEnumerable().Select(r => new Fee_FinePolicyModels
                {
                    Amount = r.Field<int>("Amount"),
                    Frequency = r.Field<string>("Frequency"),
                    PolicyId = r.Field<int>("PolicyId"),
                    TillDay = r.Field<string>("TillDay"),
                    TillMonth = r.Field<string>("TillMonth"),
                    IsDeletetable = r.Field<bool>("IsDeletetable"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    StructId = StructId,
                    StructName = StructName,
                    FixAmount = r.Field<int>("FixAmount"),
                }).ToList();
            }
            return policyList;
        }
        public Fee_FinePolicyModels GetFinePolicyById(string StructId, string StructName, int PolicyId)
        {
            return GetFinePolicyList(StructId, StructName, PolicyId).FirstOrDefault();
        }

        public Tuple<int, string> Fee_FinePolicy_CRUD(Fee_FinePolicyModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FinePolicyMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PolicyId", model.PolicyId);
                cmd.Parameters.AddWithValue("@StructId", model.StructId);
                cmd.Parameters.AddWithValue("@Amount", model.Amount);
                cmd.Parameters.AddWithValue("@Frequency", model.Frequency);
                cmd.Parameters.AddWithValue("@TillDay", model.TillDay);
                cmd.Parameters.AddWithValue("@TillMonth", model.TillMonth);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@FixAmount", model.FixAmount);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Fee_FinePolicy_CRUD(int PolicyId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_FinePolicyMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PolicyId", PolicyId);
                cmd.Parameters.AddWithValue("@StructId", "");
                cmd.Parameters.AddWithValue("@Amount", "");
                cmd.Parameters.AddWithValue("@Frequency", "");
                cmd.Parameters.AddWithValue("@TillDay", "");
                cmd.Parameters.AddWithValue("@TillMonth", "");

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
    }
}
