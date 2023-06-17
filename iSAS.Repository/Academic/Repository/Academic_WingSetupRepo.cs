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
    public class Academic_WingSetupRepo : IAcademic_WingSetupRepo
    {
        public List<Academic_WingSetupModels> GetWingList(string SchoolName, int SchoolId, string WingId)
        {
            List<Academic_WingSetupModels> wingList = new List<Academic_WingSetupModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_WingSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetSchoolWingList");
                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
                cmd.Parameters.AddWithValue("@WingId", WingId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                wingList = ds.Tables[0].AsEnumerable().Select(r => new Academic_WingSetupModels
                {
                    Default = r.Field<bool>("Default"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    SchoolId = r.Field<int>("SchoolId"),
                    WingId = r.Field<string>("WingId"),
                    WingName = r.Field<string>("WingName"),

                    SchoolName = SchoolName,
                }).ToList();
            }
            return wingList;
        }

        public Academic_WingSetupModels GetWingDetailsById(string SchoolName, int SchoolId, string WingId)
        {
            return GetWingList(SchoolName, SchoolId, WingId).FirstOrDefault();
        }

        public Tuple<int, string> Academic_WingSetup_CRUD(Academic_WingSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_WingSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WingId", model.WingId);
                cmd.Parameters.AddWithValue("@WingName", model.WingName);
                cmd.Parameters.AddWithValue("@Default", model.Default);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@SchoolId", model.SchoolId);
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

        public Tuple<int, string> Academic_WingSetup_CRUD(string WingId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_WingSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WingId", WingId);
                cmd.Parameters.AddWithValue("@WingName", "");
                cmd.Parameters.AddWithValue("@Default","");
                cmd.Parameters.AddWithValue("@PrintOrder","");
                cmd.Parameters.AddWithValue("@SchoolId","");
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
