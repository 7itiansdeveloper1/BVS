using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ISas.Entities;
using System.Configuration;
using System.Data;
using ISas.Repository.Interface;


namespace ISas.Repository.Implementation
{
    public class StudentAttendanceRepository : IStudentAttendance
    {
        //private string ClassTeacherName = "";
        public Tuple<IEnumerable<StudentAttendance>, string , bool> GetClassStudent(string studentsession, string studentclass, string studentsection, string userid, DateTime attdate)
        {
            List<StudentAttendance> studentattendnaceList = new List<StudentAttendance>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_DailyAttendance_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetStudents");
                cmd.Parameters.AddWithValue("@SessionId", studentsession);
                cmd.Parameters.AddWithValue("@ClassId", studentclass);
                cmd.Parameters.AddWithValue("@SectionId", studentsection);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@AttDate", attdate);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentAttendance studentattendance = new StudentAttendance();
                    studentattendance.Student = new Student();
                    studentattendance.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                    studentattendance.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                    studentattendance.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                    studentattendance.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();
                    studentattendance.Student.FatherName = ds.Tables[0].Rows[x][4].ToString();
                    studentattendance.Student.SmsMobileNo = ds.Tables[0].Rows[x][5].ToString();
                    //studentattendance.MorningAttendance = AttendanceType.P;
                    //studentattendance.AfterAttendance  = AttendanceType.P;
                    studentattendance.StudentAttendanceDetails = new StudentAttendnaceDetails();
                    studentattendance.StudentAttendanceDetails.MorningAttendnace = ds.Tables[0].Rows[x][6].ToString();
                    studentattendance.StudentAttendanceDetails.AfternoonAttendnace = ds.Tables[0].Rows[x][7].ToString();
                    studentattendance.StudentAttendanceDetails.LeaveType = ds.Tables[0].Rows[x][8].ToString();
                    //studentattendance.LeaveList = LeaveType.NA;
                    studentattendnaceList.Add(studentattendance);
                }
                con.Close();
                return new Tuple<IEnumerable<StudentAttendance>, string, bool>(studentattendnaceList, ds.Tables[1].Rows[0][0].ToString(), Convert.ToBoolean(ds.Tables[1].Rows[0][1]));
            }
        }

        public Tuple<int, string> SaveClassAttendance(DataTable dt, string userid, DateTime attdate,string sessionid,bool sendmessage)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_DailyAttendance_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DailyAttendanceDT", dt);
                cmd.Parameters.AddWithValue("@AttDate", attdate);
                cmd.Parameters.AddWithValue("@LoginUserID", userid);
                cmd.Parameters.AddWithValue("@Sess", sessionid);
                cmd.Parameters.AddWithValue("@SendMessage", sendmessage);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }

        public Tuple<int, string> DeleteClassAttendance(string classId, DateTime attdate, string sessionid, string sectionId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_DELETE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@attDate", attdate);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@sessionId", sessionid);
                cmd.Parameters.AddWithValue("@sectionId", sectionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
        }


        //public string GetClassTeacher(string studentsession, string studentclass, string studentsection, string userid, DateTime attdate)
        //{
        //    string ClassTeacherName = "";
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {

        //        SqlCommand cmd = new SqlCommand("sp_StudentAttendance_DailyAttendance_Cascading", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@Function", "GetStudents");
        //        cmd.Parameters.AddWithValue("@SessionId", studentsession);
        //        cmd.Parameters.AddWithValue("@ClassId", studentclass);
        //        cmd.Parameters.AddWithValue("@SectionId", studentsection);
        //        cmd.Parameters.AddWithValue("@UserId", userid);
        //        cmd.Parameters.AddWithValue("@AttDate", attdate);
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        con.Close();
        //        da.Fill(ds);
        //        if (ds.Tables[1].Rows.Count > 0)
        //            ClassTeacherName = ds.Tables[1].Rows[0][0].ToString();
        //    }
        //    return ClassTeacherName;
        //}

        public IEnumerable<DailyAttenanceSummary> GetDailyAttendanceSummary(string studentsession, DateTime attdate)
        {
            List<DailyAttenanceSummary> dailyattenancesummarylist = new List<DailyAttenanceSummary>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_LandingPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AttDate", attdate);
                cmd.Parameters.AddWithValue("@SessionId", studentsession);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    DailyAttenanceSummary dailyattenancesummary = new DailyAttenanceSummary();
                    dailyattenancesummary.ClassName = dt.Rows[x][0].ToString();
                    dailyattenancesummary.ClassTeacherName = dt.Rows[x][1].ToString();
                    dailyattenancesummary.AttendanceMarkedBy = dt.Rows[x][2].ToString();
                    dailyattenancesummary.AttendanceTime = dt.Rows[x][3].ToString();
                    dailyattenancesummary.ClassTotalStrength = Convert.ToInt32(dt.Rows[x][4].ToString());
                    dailyattenancesummary.ClassPresentStudentCount = Convert.ToInt32(dt.Rows[x][5].ToString());
                    dailyattenancesummary.ClassAbsentStudentCount = Convert.ToInt32(dt.Rows[x][6].ToString());
                    dailyattenancesummary.ClassLeaveStudentCount = Convert.ToInt32(dt.Rows[x][7].ToString());
                    dailyattenancesummary.ClassPrintOrder = Convert.ToInt32(dt.Rows[x][8].ToString());
                    dailyattenancesummary.SectionPrintOrder = Convert.ToInt32(dt.Rows[x][9].ToString());
                    dailyattenancesummarylist.Add(dailyattenancesummary);
                }
                con.Close();
            }
            return dailyattenancesummarylist;
        }
        public IEnumerable<ClassPhotoList> GetClassPhotoList(string studentsession, string studentclass, string studentsection, string userid)
        {

            List<ClassPhotoList> classPhotoList = new List<ClassPhotoList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_StudentAttendance_DailyAttendance_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Function", "GetStudentListWithPhoto");
                cmd.Parameters.AddWithValue("@SessionId", studentsession);
                cmd.Parameters.AddWithValue("@ClassId", studentclass);
                cmd.Parameters.AddWithValue("@SectionId", studentsection);
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ClassPhotoList classphoto = new ClassPhotoList();
                    classphoto.Student = new Student();
                    classphoto.Student.StudentERPNo = ds.Tables[0].Rows[x][0].ToString();
                    classphoto.Student.StudentAdmNo = ds.Tables[0].Rows[x][1].ToString();
                    classphoto.Student.StudentRollNo = Convert.ToInt32(ds.Tables[0].Rows[x][2]);
                    classphoto.Student.StudentName = ds.Tables[0].Rows[x][3].ToString();
                    classphoto.Student.FatherName = ds.Tables[0].Rows[x][4].ToString();
                    classphoto.Student.MotherName = ds.Tables[0].Rows[x][5].ToString();
                    classphoto.Student.GuardianName = ds.Tables[0].Rows[x][6].ToString();
                    classphoto.Student.SmsMobileNo = ds.Tables[0].Rows[x][7].ToString();
                    classphoto.Student.StuentPhotoURL = ds.Tables[0].Rows[x][8].ToString();
                    classphoto.Student.FatherPhotoURL = ds.Tables[0].Rows[x][9].ToString();
                    classphoto.Student.MotherPhotoURL = ds.Tables[0].Rows[x][10].ToString();
                    classphoto.Student.GardianPhotoURL = ds.Tables[0].Rows[x][11].ToString();
                    classphoto.Student.GFPhotoURL = ds.Tables[0].Rows[x][12].ToString();
                    classphoto.Student.GMPhotoURL = ds.Tables[0].Rows[x][13].ToString();
                    classphoto.Student.PPhotoURL = ds.Tables[0].Rows[x][14].ToString();
                    classPhotoList.Add(classphoto);
                }

                con.Close();
            }
            return classPhotoList;

        }
        public Tuple<int, string> UploadStudentPhoto(string refno, string imageurl, string reftype)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_PhotoUpload_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RefNo", refno);
                cmd.Parameters.AddWithValue("@ImageURL", imageurl);
                cmd.Parameters.AddWithValue("@RefType", reftype);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
    }

}
