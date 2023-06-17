using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_HolidayMasterRepo : IAcademic_HolidayMasterRepo
    {
        public List<Academic_HolidayMasterModel> GetHolidayMasterList()
        {
            List<Academic_HolidayMasterModel> holidayList = new List<Academic_HolidayMasterModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetHolidayList");
                cmd.Parameters.AddWithValue("@HolidayId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                holidayList = ds.Tables[0].AsEnumerable().Select(r => new Academic_HolidayMasterModel
                {
                    HolidayCount = r.Field<int>("HolidayCount"),
                    HolidayForStaff = r.Field<bool>("HolidayForStaff"),
                    HolidayForStudent = r.Field<bool>("HolidayForStudent"),
                    HolidayId = r.Field<string>("HolidayId"),
                    HolidayName = r.Field<string>("HolidayName"),
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                }).ToList();
            }
            return holidayList;
        }

        public string Academic_HolidayMaster_CRUD(Academic_HolidayMasterModel model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HolidayId", model.HolidayId);
                cmd.Parameters.AddWithValue("@HolidayName", model.HolidayName);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@HolidayForStudent", model.HolidayForStudent);
                cmd.Parameters.AddWithValue("@HolidayForStaff", model.HolidayForStaff);

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
        public string Academic_HolidayMaster_CRUD(string HolidayId)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HolidayId", HolidayId);
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
