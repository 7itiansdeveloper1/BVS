using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_PTMOpendDayRepo : IAcademic_PTMOpendDayRepo
    {
        public List<Academic_PTMOpendDayEntities> GetOpenDayList(string SessionId, string ClassId, string UserId, string Category)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_PTMOpendDay_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Function", "GetClassPTMOpendDayList");
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Category", Category);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Academic_PTMOpendDayEntities
                {
                    AttDate = r.Field<string>("AttDate"),
                    Category = r.Field<string>("Category"),
                    Class = r.Field<string>("Class"),
                    ClassId = r.Field<string>("ClassId"),
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    SessionId = r.Field<string>("SessionId"),
                }).ToList();
            }
        }
        public Tuple<int, string> Academic_PTMOpendDay_CRUD(Academic_PTMOpendDayEntities model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_PTMOpendDay_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);

                if (!string.IsNullOrEmpty(model.AttDate))
                    cmd.Parameters.AddWithValue("@AttDate", Convert.ToDateTime(model.AttDate).Date);
                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@Category", model.Category);
                cmd.Parameters.AddWithValue("@IsActive", true);
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
        public Tuple<int, string> Academic_PTMOpendDay_CopyToClass(string SessionId, string UserId, string Category, string FromClassId, string ToClass)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendnace_CopyPTMDates", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@FromClassId", FromClassId);
                cmd.Parameters.AddWithValue("@PtmCategory", Category);
                cmd.Parameters.AddWithValue("@ToClass", ToClass);
                cmd.Parameters.AddWithValue("@UserId", UserId);

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
