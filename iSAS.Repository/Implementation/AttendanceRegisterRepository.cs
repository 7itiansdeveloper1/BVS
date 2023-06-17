using System.Collections.Generic;
using ISas.Repository.Interface;
using ISas.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System;

namespace ISas.Repository.Implementation
{
    public class AttendanceRegisterRepository : IAttendanceRegister
    {
        public List<StudentPTMAttendance> _studentptmattendnaceList = new List<StudentPTMAttendance>();
        public string _classTeacherName = "";
        public IEnumerable<AttendanceRegisterMonthList> GetAttendanceRegisterMonthList(string userid, string sessionId, string classid, string sectionid)
        {

            List<AttendanceRegisterMonthList> attendanceregistermonthlist = new List<AttendanceRegisterMonthList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_AttendanceRegister_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetAttendanceRegisterDate");
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    AttendanceRegisterMonthList attendanceregistermonth = new AttendanceRegisterMonthList();
                    attendanceregistermonth.MonthName = dt.Rows[x][0].ToString();
                    attendanceregistermonth.MonthDate = dt.Rows[x][1].ToString();
                    attendanceregistermonthlist.Add(attendanceregistermonth);
                }

                con.Close();
            }
            return attendanceregistermonthlist;
        }


        public AttendanceRegisterViewModel GetClassAttendanceRegister(string sessionid, string classid, string sectionid, string monthname)
        {
            //DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            AttendanceRegisterViewModel attRegModel = new AttendanceRegisterViewModel();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_AttendanceRegister", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@MonthName", monthname);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            //return ds;
            attRegModel.AttendanceDataTable = ds.Tables[0];
            attRegModel.DefaulterPercentage =  Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return attRegModel;
        }

        public string GetClassTeacher()
        {
            return _classTeacherName;
        }
        public List<StudentAttendanceDetailsModel> StudentAttenDetails(string sessionid, string classid, string sectionid, string monthname)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_AttendanceRegister_Part1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@MonthName", monthname);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new StudentAttendanceDetailsModel
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    AttendancePercentage = r.Field<decimal>("AttendancePercentage"),
                    CurrentMonthAttendance = r.Field<decimal>("CurrentMonthAttendance"),
                    CurrentMonthOpenDay = r.Field<int>("CurrentMonthOpenDay"),
                    ERPNo = r.Field<string>("ERPNo"),
                    PreviousMonthOpenDay = r.Field<int>("PreviousMonthOpenDay"),
                    PrviousMonthAttendance = r.Field<decimal>("PrviousMonthAttendance"),
                    RollNo = r.Field<int>("RollNo"),
                    sno = r.Field<int>("sno"),
                    Student = r.Field<string>("Student"),
                    TotalNoofOpenDay = r.Field<int>("TotalNoofOpenDay"),
                    TotalAttendance = r.Field<decimal>("TotalAttendance"),
                    ClassName = r.Field<string>("ClassName"),
                }).ToList();
            }
        }
    }
}
