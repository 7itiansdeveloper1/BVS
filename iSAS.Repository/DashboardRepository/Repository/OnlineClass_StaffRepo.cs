using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Web.Mvc;
using ISas.Repository.DashboardRepository.IRepository;

namespace ISas.Repository.DashboardRepository.Repository
{
    public class OnlineClass_StaffRepo: IOnlineClass_StaffRepo
    {
        public OnlineClass_Staff OnlineClasses_Transaction_FormLoad(string userid)
        {
            OnlineClass_Staff onlineClass_Staff = new OnlineClass_Staff();
            DataSet ds = new DataSet();
            using ( SqlConnection con=new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()) )
            {
                
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_OnlineClasses_Transaction",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "FormLoad");
                cmd.Parameters.AddWithValue("@userId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if(ds.Tables[0].Rows.Count>0)
                {
                    onlineClass_Staff.classList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text=r.Field<string>("ClassName"),
                        Value = r.Field<string>("ClassSectionId")
                    }).ToList();

                    
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    onlineClass_Staff.subjectList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("subjectName"),
                        Value = r.Field<Guid>("subjectId").ToString()
                    }).ToList();

                }
            }
            onlineclass onlineclass = new onlineclass();
            onlineclass.onlineClassDate= DateTime.Now.ToShortDateString().Replace("-", "/");
            onlineclass.zoomURL = ds.Tables[2].Rows[0]["zoomURL"].ToString();
            onlineClass_Staff.object_onlineclass = onlineclass;
            return onlineClass_Staff;

        }

        public List<onlineclass> OnlineClasses_Transaction_ClassList(string userid, string classdate)
        {
            List<onlineclass> onlineclassesList = new List<onlineclass>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_OnlineClasses_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userid);
                cmd.Parameters.AddWithValue("@classDate", Convert.ToDateTime( classdate).Date);
                cmd.Parameters.AddWithValue("@mode", "GetyOnlineClassList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                foreach(DataRow row in ds.Tables[0].Rows )
                {
                    onlineclass onlineclass = new onlineclass();
                    onlineclass.id = new Guid( row["id"].ToString());
                    onlineclass.onlineClassName = row["onlineClassName"].ToString();
                    onlineclass.classId = row["ClasName"].ToString();
                    onlineclass.subjectId = row["subjectName"].ToString();
                    onlineclass.onlineClassStartTime = row["onlineClassStartTime"].ToString();
                    onlineclass.IsDeleteable = Convert.ToBoolean(row["IsDeleteable"]);
                    onlineclassesList.Add(onlineclass);
                }
            }
            return onlineclassesList;
        }

        public Tuple<int,string> OnlineClasses_CRUD(onlineclass model)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_OnlineClasses_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",model.id);
                cmd.Parameters.AddWithValue("@onlineClassName", model.onlineClassName);
                cmd.Parameters.AddWithValue("@onlineClassDate", Convert.ToDateTime(model.onlineClassDate).Date);
                cmd.Parameters.AddWithValue("@classId", model.classId.ToString().Substring(0,10)) ;
                cmd.Parameters.AddWithValue("@sectionId", model.classId.ToString().Substring(10, 5));
                cmd.Parameters.AddWithValue("@subjectId", model.subjectId);
                cmd.Parameters.AddWithValue("@teacherId", model.teacherId);
                cmd.Parameters.AddWithValue("@onlineClassStartTime", Convert.ToDateTime(model.onlineClassDate+" "+model.onlineClassStartTime));
                cmd.Parameters.AddWithValue("@onlineClassEndTime", Convert.ToDateTime(model.onlineClassDate + " " + model.onlineClassEndTime));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                
            }
            return new Tuple<int,string>(Convert.ToInt32( ds.Tables[0].Rows[0][0]),ds.Tables[0].Rows[0][1].ToString());

        }
        public Tuple<int, string> ZoomURL_CRUD(string  userid,string zoomurl)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_ZoomURL_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@zoomURL", zoomurl);
                cmd.Parameters.AddWithValue("@userId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

            }
            return new Tuple<int, string>(Convert.ToInt32(ds.Tables[0].Rows[0][0]), ds.Tables[0].Rows[0][1].ToString());

        }
    }
}
