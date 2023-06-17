using System;
using System.Collections.Generic;
using ISas.Repository.Interface;
using ISas.Entities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ISas.Repository.Implementation
{
    public class Exam_AchievementRepository : IExam_AchievementRepository
    {

        public IEnumerable<ExamNameList> GetExamNameList(string userId)
        {
            List<ExamNameList> examnamelist = new List<ExamNameList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_Achievement_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Mode", "GetExamList");
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ExamNameList examname = new ExamNameList();
                    examname.ExamId = dt.Rows[x][0].ToString();
                    examname.ExamName = dt.Rows[x][1].ToString();
                    examname.PrintOrder = Convert.ToInt32(dt.Rows[x][2]);
                    examnamelist.Add(examname);
                }
                con.Close();
            }
            return examnamelist;
        }
        public IEnumerable<StudentAchievementList> GetStudentAchievementList(string sessionId, string classId, string sectionId, string examId)
        {
            List<StudentAchievementList> studentachievementlist = new List<StudentAchievementList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_Achievement_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@ExamId", examId);
                cmd.Parameters.AddWithValue("@Mode", "GetStudentList");
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentAchievementList studentachievement = new StudentAchievementList();
                    studentachievement.Student = new Student();
                    studentachievement.Student.StudentERPNo = dt.Rows[x][0].ToString();
                    studentachievement.Student.StudentAdmNo =  dt.Rows[x][1].ToString();
                    studentachievement.Student.StudentRollNo = Convert.ToInt32(dt.Rows[x][2]);
                    studentachievement.Student.StudentName = dt.Rows[x][3].ToString();
                    studentachievement.Achievement = dt.Rows[x][4].ToString();
                    studentachievementlist.Add(studentachievement);
                }
                con.Close();
            }
            return studentachievementlist;
        }
      
        public Tuple<int, string> Exam_Achievement_CRUD(DataTable dt, string sessionid, string examid, string userid)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Achievements_CRUD1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Exam_Remark_CRUD_Type", dt);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@Mode", "SAVE");
                cmd.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
    }
}

