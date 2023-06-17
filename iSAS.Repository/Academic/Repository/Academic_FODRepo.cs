using System;
using System.Collections.Generic;
using System.Linq;
using ISas.Entities.Academic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ISas.Repository.Academic.IRepository;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_FODRepo: IAcademic_FODRepo
    {
        public List<Academic_FOD> GetFOD_Transaction()
        {
            List<Academic_FOD> fodList = new List<Academic_FOD>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_FoodOfTheDay_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FODId", null);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                fodList = ds.Tables[0].AsEnumerable().Select(r => new Academic_FOD
                {
                    FODId = r.Field<int>("FODId"),
                    MessageText = r.Field<string>("MessageText"),
                    MonthDay = r.Field<int>("MonthDay"),
                    IsActive = r.Field<bool>("IsActive"),
                }).ToList();
            }
            return fodList;
        }
        public Tuple<int, string> GetFOD_CRUD(Academic_FOD model,string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_FoodOfTheDay_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FODId", model.FODId);
                cmd.Parameters.AddWithValue("@MessageText", model.MessageText);
                cmd.Parameters.AddWithValue("@MonthDay", model.MonthDay);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Academic_FOD GetFOD_Transaction(int fodId)
        {
            Academic_FOD  ObjFOD = new Academic_FOD ();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_FoodOfTheDay_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FODId", fodId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                ObjFOD.FODId = fodId;
                ObjFOD.MessageText = dt.Rows[0]["MessageText"].ToString();
                ObjFOD.MonthDay = Convert.ToInt32(dt.Rows[0]["MonthDay"]);
                ObjFOD.IsActive = Convert.ToBoolean( dt.Rows[0]["IsActive"]);
            }
            return ObjFOD;
        }
    }
}
