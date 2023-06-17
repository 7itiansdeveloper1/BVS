using System;
using System.Collections.Generic;
using ISas.Repository.Interface;
using ISas.Entities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ISas.Repository.Implementation
{
    public class Exam_ReportCardRepository : IExam_ReportCard
    {
        public IEnumerable<ClassStudentList> GetClassStudentList(string sessionid,string classid, string sectionid,string userid)
        {
            List<ClassStudentList> classstudentlist = new List<ClassStudentList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_ReportCard_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetClassStudentList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ClassStudentList classstudent = new ClassStudentList();
                    classstudent.ERPNo = dt.Rows[x][0].ToString();
                    classstudent.AdmNo = dt.Rows[x][1].ToString();
                    classstudent.RollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
                    classstudent.Student = dt.Rows[x][3].ToString();
                    classstudentlist.Add(classstudent);
                }
                con.Close();
            }
            return classstudentlist;
        }
        public IEnumerable<ClassStudentList> GetClassRetestStudentList(string sessionid, string classid, string sectionid, string userid)
        {
            List<ClassStudentList> classstudentlist = new List<ClassStudentList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {

                SqlCommand cmd = new SqlCommand("sp_Exam_RetestReportCard_Cascading", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@Mode", "GetClassStudentList");
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                int count = dt.Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    ClassStudentList classstudent = new ClassStudentList();
                    classstudent.ERPNo = dt.Rows[x][0].ToString();
                    classstudent.AdmNo = dt.Rows[x][1].ToString();
                    classstudent.RollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
                    classstudent.Student = dt.Rows[x][3].ToString();
                    classstudentlist.Add(classstudent);
                }
                con.Close();
            }
            return classstudentlist;
        }

        public DataSet GradeCard_ReportTemplate(string classId, string examId, string section, string erpNo, string session)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet dt = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_GradeCardTemplate", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@ExamId", examId);

                cmd.Parameters.AddWithValue("@SectionId", section);
                cmd.Parameters.AddWithValue("@ERPNo", erpNo);
                cmd.Parameters.AddWithValue("@SessionId", session);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                return dt;
            }
        }

        public DataTable GradeCard_ReportTemplate1(string classId, string examId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_GradeCardTemplate1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classId);
                cmd.Parameters.AddWithValue("@ExamId", examId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                return dt;
            }
        }

        public DataSet GradeCard_GradeCard6to8(string classid, string sectionid, string sessionid, string erpno, string examid)
        {

            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_GradeCard6to8", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }
        public DataSet GradeCard_GradeCard9to10(string classid, string sectionid, string sessionid, string erpno, string examid)
        {

            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_GradeCard9to10", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }
        public DataSet MarkRegister_MarkRegister6to8(string classid, string sectionid, string sessionid, string erpno, string examid)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister6to8", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }


        public DataSet MarkRegister_MarkRegister11to12(string classid, string sectionid, string sessionid, string erpno, string examid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegisterto11to12", con);
                //SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister6to8", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
            
        }
        public DataSet MarkRegister_MarkRegister9to10(string classid, string sectionid, string sessionid, string erpno, string examid)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister9to10", con);
                //SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister6to8", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }
        public DataSet MarkRegister_MarkRegister1to5(string classid, string sectionid, string sessionid, string erpno, string examid)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister1to5", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }


        //public DataSet Exam_Processing_Class(string classid, string sectionid, string sessionid, string erpno, string examid)
        //{
        //    DataSet ds = new DataSet();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister_Process", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@ClassId", classid);
        //        cmd.Parameters.AddWithValue("@SectionId", sectionid);
        //        cmd.Parameters.AddWithValue("@SessionId", sessionid);
        //        cmd.Parameters.AddWithValue("@ExamId", examid);
        //        cmd.CommandTimeout = 0;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //        con.Close();
        //    }
        //    return ds;
        //}


        public Tuple<int, string> Exam_Processing_Multi(string classid, string sectionid, string sessionid,string examid, string erpnos)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                //SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister_Process", con);
                SqlCommand cmd = new SqlCommand("sp_Exam_ReportCard1to2_Compile_Class", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ERPNos", erpnos);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Exam_Processing_Single(string classid, string sectionid, string sessionid, string examid,string erpno)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_MarkRegister_SingleStudent1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassId", classid);
                cmd.Parameters.AddWithValue("@SectionId", sectionid);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
            }
        }


    }
}

