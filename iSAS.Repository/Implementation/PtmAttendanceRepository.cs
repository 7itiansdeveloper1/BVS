using System;
using System.Collections.Generic;
using ISas.Repository.Interface;
using ISas.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ISas.Repository.Implementation
{
    public class PtmAttendanceRepository:IPtmAttendance
    {
        public List<StudentPTMAttendance> _studentptmattendnaceList = new List<StudentPTMAttendance>();
        public string _classTeacherName = "";
        public IEnumerable<PTMDatesList> GetPTMDatesList(string classid, string userid,string sessionId,string category)
        {

            List<PTMDatesList> ptmdatelist = new List<PTMDatesList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_PTMAttendance_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetPTMDateList");
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@Category", category);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    PTMDatesList ptmdate = new PTMDatesList();
                    ptmdate.PTMDate = dt.Rows[x][0].ToString();
                    ptmdatelist.Add(ptmdate);
                }

                con.Close();
            }
            return ptmdatelist;

        }
        public IEnumerable<CategoryList> GetPTMCategoryList()
        {
            List<CategoryList> ptmcategorylist = new List<CategoryList>();
            CategoryList ptmcategory = new CategoryList();
            ptmcategory.Category = "PTM";
            ptmcategorylist.Add(ptmcategory);
            CategoryList ptmcategory1 = new CategoryList();
            ptmcategory1.Category = "ORIENTATION";
            ptmcategorylist.Add(ptmcategory1);
            return ptmcategorylist;
        }
        
        public IEnumerable<StudentPTMAttendance> GetPTMClassStudent(string sessionid, string classid, string sectionid, string userid, string ptmdate)
        {
            //List<StudentPTMAttendance> studentptmattendnaceList = new List<StudentPTMAttendance>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_PTMAttendance_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetPTMStudentList");
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@AttDate", ptmdate);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentPTMAttendance studentptmattendance = new StudentPTMAttendance();
                    studentptmattendance.Student = new Student();
                    studentptmattendance.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                    studentptmattendance.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                    studentptmattendance.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                    studentptmattendance.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();
                    studentptmattendance.Student.FatherName = ds.Tables[0].Rows[x][4].ToString();
                    studentptmattendance.Student.SmsMobileNo = ds.Tables[0].Rows[x][5].ToString();
                    studentptmattendance.StudentPTMAttendanceDetail = new StudentPTMAttendnaceDetails();
                    studentptmattendance.StudentPTMAttendanceDetail.Student = ds.Tables[0].Rows[x][6].ToString();
                    studentptmattendance.StudentPTMAttendanceDetail.Father = ds.Tables[0].Rows[x][7].ToString();
                    studentptmattendance.StudentPTMAttendanceDetail.Mother = ds.Tables[0].Rows[x][8].ToString();
                    _studentptmattendnaceList.Add(studentptmattendance);
                }
                if (ds.Tables[1].Rows.Count > 0)
                    _classTeacherName = ds.Tables[1].Rows[0][0].ToString();
                con.Close();
            }
            return _studentptmattendnaceList;
        }
        public Tuple<int, string> SaveClassPTMAttendance(DataTable dt, string userid, string attdate,string sessionid)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_PTMAttendance_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PTMAttendanceDT", dt);
                cmd.Parameters.AddWithValue("@AttDate", attdate);
                cmd.Parameters.AddWithValue("@LoginUserID", userid);
                cmd.Parameters.AddWithValue("@Sess", sessionid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
        public string GetClassTeacher()
        {
            return _classTeacherName;
        }

    }
}
