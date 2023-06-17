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
    public class Academic_QualificationMasterRepo : IAcademic_QualificationMasterRepo
    {
        public List<Academic_QualificationMasterModels> GetQualificationList()
        {
            List<Academic_QualificationMasterModels> qualificationList = new List<Academic_QualificationMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_QualificationMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetQualificationList");
                cmd.Parameters.AddWithValue("@QualifId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                qualificationList = ds.Tables[0].AsEnumerable().Select(r => new Academic_QualificationMasterModels
                {
                     FatherStrength = r.Field<int>("FatherStrength"),
                     MotherStrength = r.Field<int>("MotherStrength"),
                     QualifId = r.Field<string>("QualifId"),
                     QualifName = r.Field<string>("QualifName"),
                     StaffStrength = r.Field<int>("StaffStrength"),
                     IsDeletable = r.Field<bool>("IsDeletable"),
                     
                }).ToList();
            }
            return qualificationList;
        }

        public Tuple<int, string> Academic_QualificationMaster_CRUD(Academic_QualificationMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_QualificationMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QualifId", model.QualifId);
                cmd.Parameters.AddWithValue("@QualifName", model.QualifName);

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
        public Tuple<int, string> Academic_QualificationMaster_CRUD(string QualifId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_QualificationMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QualifId", QualifId);
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
