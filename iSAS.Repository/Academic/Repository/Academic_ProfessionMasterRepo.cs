using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_ProfessionMasterRepo : IAcademic_ProfessionMasterRepo
    {
        public List<Academic_ProfessionMasterModels> GetProfessionList()
        {
            List<Academic_ProfessionMasterModels> porfList = new List<Academic_ProfessionMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ProfessionMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetProfessionList");
                cmd.Parameters.AddWithValue("@ProfId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                porfList = ds.Tables[0].AsEnumerable().Select(r => new Academic_ProfessionMasterModels
                {
                    FatherStrength = r.Field<int>("FatherStrength"),
                    MotherStrength = r.Field<int>("MotherStrength"),
                    ProfId = r.Field<string>("ProfId"),
                    ProfName = r.Field<string>("ProfName"),
                    IsDeletable = r.Field<bool>("IsDeletable"),

                }).ToList();
            }
            return porfList;
        }

        public Tuple<int, string> Academic_ProfessionMaster_CRUD(Academic_ProfessionMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ProfessionMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProfID", model.ProfId);
                cmd.Parameters.AddWithValue("@ProfName", model.ProfName);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Academic_ProfessionMaster_CRUD(string ProfId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ProfessionMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProfID", ProfId);
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
