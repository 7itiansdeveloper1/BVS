using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ISas.Entities.DashboardEntities;
using System.Configuration;
using ISas.Repository.DashboardRepository.IRepository;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class OnlineClass_StudentRepo: IOnlineClass_StudentRepo
    {
        public OnlineClassesModel GetStudentOnlineClassList(string userid)
        {
            OnlineClassesModel model = new OnlineClassesModel();
            using ( SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_OnlineClasses_Student_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userid);
                cmd.Parameters.AddWithValue("@classDate",DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@mode","FormLoad");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model._onlineclassesStudentList = ds.Tables[0].AsEnumerable().Select(r => new onlineclassStudent
                    {
                        id=r.Field<Guid>("id"),
                        onlineClassName = r.Field<string>("onlineClassName"),
                        ClasName = r.Field<string>("ClasName"),
                        subjectName = r.Field<string>("subjectName"),
                        OnlineClassStartTime = r.Field<string>("OnlineClassStartTime"),
                        IsclassAvaialbe = r.Field<bool>("IsclassAvaialbe"),
                        zoomURL = r.Field<string>("zoomURL"),
                    }
                    ).ToList();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    model.secretCode = ds.Tables[1].Rows[0][0].ToString();
                }
            }
            return model;
        }

      public  Tuple<bool,int,string> onlineLogPunch_CRUD(string erpno,string classid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_onlineClassesLog_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@classId", classid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                
                da.Fill(dt);
                
            }
            return new Tuple<bool, int, string>(Convert.ToBoolean(dt.Rows[0][0]), Convert.ToInt32(dt.Rows[0][1]), dt.Rows[0][2].ToString());
        }
    }
}
