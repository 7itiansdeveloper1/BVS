using System;
using System.Collections.Generic;
using ISas.Repository.Interface;
using ISas.Entities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;

namespace ISas.Repository.ImplementationGetExamNameList

{
    public class Exam_RemarkRepository : IExam_RemarkRepository
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
        public IEnumerable<StudentRemarkList> GetStudentRemarkList(string sessionId, string classId, string sectionId, string examId, string mode)
        {
            List<StudentRemarkList> studentremarklist = new List<StudentRemarkList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_Remark_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@ExamId", examId);
                cmd.Parameters.AddWithValue("@Mode", mode);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    StudentRemarkList studentremark = new StudentRemarkList();
                    studentremark.Student = new Student();
                    studentremark.Student.StudentERPNo = dt.Rows[x][0].ToString();
                    studentremark.Student.StudentAdmNo = dt.Rows[x][1].ToString();
                    studentremark.Student.StudentRollNo = Convert.ToInt32(dt.Rows[x][2]);
                    studentremark.Student.StudentName = dt.Rows[x][3].ToString();
                    studentremark.RemarkId = dt.Rows[x][4].ToString();

                    if (!string.IsNullOrEmpty(dt.Rows[x][5].ToString()))
                        studentremark.PreviousSelectedRemarksIds = dt.Rows[x][5].ToString().Replace(" ", "").Split(',').ToList();
                    studentremarklist.Add(studentremark);
                }
                con.Close();
            }
            return studentremarklist;
        }

        public IEnumerable<RemarkList> GetRemarkList(string sessionId, string classId, string sectionId, string examId)
        {
            List<RemarkList> _remarkList = new List<RemarkList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_Remark_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@SectionId", sectionId);
                cmd.Parameters.AddWithValue("@ExamId", examId);
                cmd.Parameters.AddWithValue("@Mode", "GetRemarkList");
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    RemarkList _remark = new RemarkList();
                    _remark.RemarkId = ds.Tables[0].Rows[x][0].ToString();
                    _remark.RemarkText = ds.Tables[0].Rows[x][1].ToString();
                    _remarkList.Add(_remark);
                }
                con.Close();
            }
            return _remarkList;
        }
        //public IEnumerable<StudentsMarkList> GetStudentMarkList(string examid, string userid, string classid, string sectionid, string subjectid,string assessmentid)
        //{
        //    List<StudentsMarkList> studentmarklist = new List<StudentsMarkList>();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {

        //        SqlCommand cmd = new SqlCommand("sp_Exam_MarksEntry6to8_Cascading", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ExamId", examid);
        //        cmd.Parameters.AddWithValue("@ClassId", classid);
        //        cmd.Parameters.AddWithValue("@SectionId", sectionid);
        //        cmd.Parameters.AddWithValue("@SubjectId", subjectid);
        //        cmd.Parameters.AddWithValue("@AssessmentId", assessmentid);
        //        cmd.Parameters.AddWithValue("@Mode", "GetStudentMarkList");
        //        cmd.Parameters.AddWithValue("@UserId", userid);
        //        con.Open();
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        int count = dt.Rows.Count;
        //        for (int x = 0; x < count; x++)
        //        {
        //            StudentsMarkList studentmark = new StudentsMarkList();
        //            studentmark.Student = new Student();
        //            studentmark.Student.StudentERPNo = dt.Rows[x][0].ToString();
        //            studentmark.Student.StudentAdmNo = dt.Rows[x][1].ToString();
        //            studentmark.Student.StudentRollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
        //            studentmark.Student.StudentName = dt.Rows[x][3].ToString();
        //            studentmark.StudentsMarkFromDBList = new StudentsMarkFromDBList();

        //            studentmark.StudentsMarkFromDBList.IsStudentAbsent = Convert.ToBoolean( dt.Rows[x][4].ToString());
        //            studentmark.StudentsMarkFromDBList.IsStudentonML = Convert.ToBoolean(dt.Rows[x][5].ToString());
        //            studentmark.StudentsMarkFromDBList.IsStudentExempt = Convert.ToBoolean(dt.Rows[x][6].ToString());
        //            studentmark.StudentsMarkFromDBList.StudentMark = dt.Rows[x][7].ToString();
        //            studentmark.StudentsMarkFromDBList.StudentGrade = dt.Rows[x][8].ToString();
        //            studentmarklist.Add(studentmark);
        //        }
        //        con.Close();
        //    }
        //    return studentmarklist;
        //}
        public Tuple<int, string> Exam_Remark_CRUD(DataTable dt, string sessionid, string examid, string userid)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_Remark_CRUD1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Exam_Remark_CRUD_Type", dt);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
    }
}

