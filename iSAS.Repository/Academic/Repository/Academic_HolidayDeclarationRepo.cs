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
    public class Academic_HolidayDeclarationRepo : IAcademic_HolidayDeclarationRepo
    {
        public List<Academic_HolidayDeclarationModel> GetHolidayDeclarationList(string HolidayId)
        {
            List<Academic_HolidayDeclarationModel> declarationList = new List<Academic_HolidayDeclarationModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayDeclaration_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetHolidayDetails");
                cmd.Parameters.AddWithValue("@HolidayId", HolidayId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                declarationList = ds.Tables[0].AsEnumerable().Select(r => new Academic_HolidayDeclarationModel
                {
                    HolidayId = r.Field<string>("HolidayId"),
                    Fdate = r.Field<string>("Fdate"),
                    TDate = r.Field<string>("TDate"),
                    NoofDays = r.Field<int>("NoofDays"),
                    HolidayName = r.Field<string>("HolidayName"),
                }).ToList();
            }
            return declarationList;
        }

        public Academic_HolidayDeclarationModel GetHolidayDeclarationById(string HolidayId)
        {
            return GetHolidayDeclarationList(HolidayId).FirstOrDefault();
        }

        public string Academic_HolidayDeclaration_CRUD(Academic_HolidayDeclarationModel model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayDeclaration_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HolidayId", model.HolidayId);

                if (!string.IsNullOrEmpty(model.Fdate))
                    cmd.Parameters.AddWithValue("@FDate", Convert.ToDateTime(model.Fdate).Date);

                if (!string.IsNullOrEmpty(model.Fdate))
                    cmd.Parameters.AddWithValue("@TDate", Convert.ToDateTime(model.TDate).Date);

                cmd.Parameters.AddWithValue("@NoofDays", model.NoofDays);

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
        public string Academic_HolidayDeclaration_CRUD(string HolidayId)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Academic_HolidayDeclaration_CRUD", con);
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
