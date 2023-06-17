using ISas.Entities;
using ISas.Repository.Interface;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Implementation
{
    public class Exam_StudentProfileRepo : IExam_StudentProfileRepo
    {
        public List<SelectListItem> GetUserWiseClass(string UserId)
        {
            List<SelectListItem> classList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_GetUserWiseClass", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                classList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID"),
                }).ToList();
            }
            return classList;
        }

        public List<SelectListItem> GetUserWiseSection(string UserId, string ClassId)
        {
            List<SelectListItem> sectionList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_GetUserWiseClassSection", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Cls", ClassId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                sectionList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SecName"),
                    Value = r.Field<string>("SecID"),
                }).ToList();
            }
            return sectionList;
        }


        public List<StudentDetailModel> Profile_Attendnace_Cascading(string userid, string sessionid, string classid, string sectionid, string examid, string mode)
        {
            List<StudentDetailModel> studentDetailList = new List<StudentDetailModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Profile_Attendance_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@Mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                studentDetailList = ds.Tables[0].AsEnumerable().Select(r => new StudentDetailModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Attendance = r.Field<decimal>("Attendance"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Height = r.Field<string>("Height"),
                    OpenDay = r.Field<int>("OpenDay"),
                    RollNo = r.Field<int>("RollNo"),
                    Sno = r.Field<long>("Sno"),
                    StudentName = r.Field<string>("StudentName"),
                    Weight = r.Field<string>("Weight"),
                }).ToList();
            }
            return studentDetailList;
        }


        public string Profile_Attendnace_CRUD(string userid, string sessionid, string classid, string sectionid, string examid, DataTable dtVal)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Profile_Attendnace_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@DataTable", dtVal);

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
