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
    public class Academic_HouseMasterRepo : IAcademic_HouseMasterRepo
    {
        public List<Academic_HouseMasterModels> GetHouseList()
        {
            List<Academic_HouseMasterModels> houseMstList = new List<Academic_HouseMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HouseMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetHouseList");
                cmd.Parameters.AddWithValue("@HouseId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                houseMstList = ds.Tables[0].AsEnumerable().Select(r => new Academic_HouseMasterModels
                {
                    Active = r.Field<bool>("Active"),
                    HouseColor = r.Field<string>("HouseColor"),
                    HouseId = r.Field<string>("HouseId"),
                    HouseInchargeName = r.Field<string>("HouseInchargeName"),
                    HouseName = r.Field<string>("HouseName"),
                    HouseStrength = r.Field<int>("HouseStrength"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                }).ToList();
            }
            return houseMstList;
        }

        public Tuple<int, string> Academic_HouseMaster_CRUD(Academic_HouseMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HouseMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HouseId", model.HouseId);
                cmd.Parameters.AddWithValue("@HouseName", model.HouseName);
                cmd.Parameters.AddWithValue("@HouseColor", model.HouseColor);
                cmd.Parameters.AddWithValue("@HouseInchargeName", model.HouseInchargeName);
                cmd.Parameters.AddWithValue("@PrintORder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Active", model.Active);

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
        public Tuple<int, string> Academic_HouseMaster_CRUD(string HouseId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_HouseMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HouseId", HouseId);
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
