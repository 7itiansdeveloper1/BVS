using System;
using System.Collections.Generic;
using ISas.Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ISas.Repository.Interface;
using System.Web.Mvc;
using System.Linq;

namespace ISas.Repository.Implementation
{
    public class StudentClassRepositroy : IStudentClass
    {
        public IEnumerable<StudentClass> GetAllClasses(string userid)
        {
            List<StudentClass> classes = new List<StudentClass>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_GetAllClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    StudentClass studentclass = new StudentClass();
                    studentclass.ClassId = dr.GetValue(0).ToString();
                    studentclass.ClassName = dr.GetValue(1).ToString();
                    studentclass.PrintOrder = Convert.ToInt32(dr.GetValue(2).ToString());
                    classes.Add(studentclass);
                }
                con.Close();
            }
            return classes;
        }

        public IEnumerable<StudentClass> GetAllClasses()
        {
            List<StudentClass> classes = new List<StudentClass>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select ClassID,ClassName,PrintOrder from dbo.[GetClass]() where Active = 1 order by PrintOrder", con);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@UserId", userid);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    StudentClass studentclass = new StudentClass();
                    studentclass.ClassId = dr.GetValue(0).ToString();
                    studentclass.ClassName = dr.GetValue(1).ToString();
                    studentclass.PrintOrder = Convert.ToInt32(dr.GetValue(2).ToString());
                    classes.Add(studentclass);
                }
                con.Close();
            }
            return classes;
        }

        //public IEnumerable<Student> GetStudentListForTC(string sessionid, string classid, string sectionid, string userid)
        //{
        //    List<Student> studentlistfortc = new List<Student>();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {

        //        SqlCommand cmd = new SqlCommand("sp_Exam_ReportCard_Cascading", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@SessionId", sessionid);
        //        cmd.Parameters.AddWithValue("@ClassId", classid);
        //        cmd.Parameters.AddWithValue("@SectionId", sectionid);
        //        cmd.Parameters.AddWithValue("@Mode", "GetClassStudentListwithTC");
        //        cmd.Parameters.AddWithValue("@UserId", userid);
        //        con.Open();
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        int count = dt.Rows.Count;
        //        for (int x = 0; x < count; x++)
        //        {
        //            Student classstudent = new Student();
        //            classstudent.StudentERPNo = dt.Rows[x][0].ToString();
        //            classstudent.StudentAdmNo = dt.Rows[x][1].ToString();
        //            classstudent.StudentRollNo = Convert.ToInt32(dt.Rows[x][2].ToString());
        //            classstudent.StudentName = dt.Rows[x][3].ToString();
        //            classstudent.FatherName = dt.Rows[x][4].ToString();
        //            studentlistfortc.Add(classstudent);
        //        }
        //        con.Close();
        //    }
        //    return studentlistfortc;
        //}

        //public string StudentTC_CRUD(Student_TC model)
        //{
        //    string message = "";
        //    DataTable dt1 = new DataTable();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Student_TC_CRUD", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        //model.FormID = "TESTFORMID";
        //        cmd.Parameters.AddWithValue("@TC_Id", model.TCNo);
        //        cmd.Parameters.AddWithValue("@SessionId", model.Session);
        //        cmd.Parameters.AddWithValue("@ERPNO", model.ERPNo);
        //        cmd.Parameters.AddWithValue("@TC_AppliedDate", Convert.ToDateTime(model.AppliedDate).Date);
        //        cmd.Parameters.AddWithValue("@TC_CreationDate", Convert.ToDateTime(model.CreationDate).Date);
        //        cmd.Parameters.AddWithValue("@TC_IssueDate", Convert.ToDateTime(model.IssueDate).Date);

        //        cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
        //        cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
        //        cmd.Parameters.AddWithValue("@Refund_Amount", model.RefundFee);

        //        cmd.Parameters.AddWithValue("@TC_Description", model.Description);
        //        cmd.Parameters.AddWithValue("@ExtraCircullarActivity", model.ExtraCurricularActivities);
        //        cmd.Parameters.AddWithValue("@BelongToSCST", model.BelogtoSCST);
        //        cmd.Parameters.AddWithValue("@FailedInSameClass", model.FailedinSameClass);
        //        cmd.Parameters.AddWithValue("@ClassInFigure", model.ClassInFigure);
        //        cmd.Parameters.AddWithValue("@MonthForDues", model.MonthForDues);
        //        cmd.Parameters.AddWithValue("@TotalWorkingDay", model.TotalWorkingDays);
        //        cmd.Parameters.AddWithValue("@WeitherNCCCadit", model.WeitherNCCCedete);

        //        cmd.Parameters.AddWithValue("@TC_Reason", model.ReasonForTC);
        //        cmd.Parameters.AddWithValue("@HigherClassId", model.HigherClass);
        //        cmd.Parameters.AddWithValue("@LastExamResult", model.LastExamResult);
        //        cmd.Parameters.AddWithValue("@QualifiedforPromotion", model.QualifiedForPromotion);
        //        cmd.Parameters.AddWithValue("@ClassInWord", model.ClassInWords);
        //        cmd.Parameters.AddWithValue("@NatureofConcession", model.NatureOFConcession);
        //        cmd.Parameters.AddWithValue("@WorkingDaysPresent", model.WorkingDaysPresents);
        //        cmd.Parameters.AddWithValue("@ExtraCircullarAchievement", model.ExtraCurricularAcheivementLevel);
        //        cmd.Parameters.AddWithValue("@IsQualifiedforHighClass", model.IsQualifiedForHigherClass);
        //        cmd.Parameters.AddWithValue("@Mode ", "SAVE");

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt1);
        //        con.Close();
        //    }
        //    message = dt1.Rows[0][1].ToString();
        //    return message;
        //}

        //public IEnumerable<Student_TC> Student_TC_FormLoad(string TCID)
        //{
        //    List<Student_TC> studnettcList = new List<Student_TC>();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        SqlCommand cmd = new SqlCommand("exec sp_Student_TC_FormLoad '" + TCID + "'", con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        int count = ds.Tables[0].Rows.Count;

        //        for (int x = 0; x < count; x++)
        //        {
        //            Student_TC tcDetails = new Student_TC();

        //            tcDetails.Session = ds.Tables[0].Rows[x][0].ToString();
        //            tcDetails.TCNo = ds.Tables[0].Rows[x][1].ToString();
        //            tcDetails.ERPNo = ds.Tables[0].Rows[x][2].ToString();
        //            tcDetails.AppliedDate = ds.Tables[0].Rows[x][3].ToString();
        //            tcDetails.CreationDate = ds.Tables[0].Rows[x][4].ToString();
        //            tcDetails.IssueDate = ds.Tables[0].Rows[x][5].ToString();
        //            tcDetails.ClassId = ds.Tables[0].Rows[x][6].ToString();
        //            tcDetails.Class = ds.Tables[0].Rows[x][7].ToString();
        //            tcDetails.SectionId = ds.Tables[0].Rows[x][8].ToString();
        //            tcDetails.Section = ds.Tables[0].Rows[x][9].ToString();
        //            //tcDetails.RefundFee = ds.Tables[0].Rows[x][10].ToString();
        //            tcDetails.Description = ds.Tables[0].Rows[x][11].ToString();
        //            tcDetails.ExtraCurricularActivities = ds.Tables[0].Rows[x][12].ToString();
        //            tcDetails.BelogtoSCST = ds.Tables[0].Rows[x][13].ToString();
        //            tcDetails.FailedinSameClass = ds.Tables[0].Rows[x][14].ToString();
        //            //tcDetails.ClassInFigure = ds.Tables[0].Rows[x][15].ToString();
        //            tcDetails.MonthForDues = ds.Tables[0].Rows[x][16].ToString();
        //            tcDetails.TotalWorkingDays = ds.Tables[0].Rows[x][17].ToString();
        //            tcDetails.WeitherNCCCedete = ds.Tables[0].Rows[x][18].ToString();
        //            tcDetails.ReasonForTC = ds.Tables[0].Rows[x][19].ToString();
        //            tcDetails.HigherClassinFigure  = ds.Tables[0].Rows[x][20].ToString();
        //            //tcDetails.LastExamResult = ds.Tables[0].Rows[x][21].ToString();
        //            //tcDetails.QualifiedForPromotion = ds.Tables[0].Rows[x][22].ToString();
        //            //tcDetails.ClassInWords = ds.Tables[0].Rows[x][23].ToString();
        //            tcDetails.NatureOFConcession = ds.Tables[0].Rows[x][24].ToString();
        //            tcDetails.WorkingDaysPresents = ds.Tables[0].Rows[x][25].ToString();
        //            tcDetails.ExtraCurricularActivities = ds.Tables[0].Rows[x][26].ToString();
        //            tcDetails.IsQualifiedForHigherClass = ds.Tables[0].Rows[x][27].ToString();
        //            tcDetails.IsTCCancelled = Convert.ToBoolean(ds.Tables[0].Rows[x][28]);
        //            tcDetails.FatherName = ds.Tables[0].Rows[x][29].ToString();
        //            tcDetails.AdmNo = ds.Tables[0].Rows[x][30].ToString();
        //            tcDetails.Student = ds.Tables[0].Rows[x][31].ToString();
        //            studnettcList.Add(tcDetails);
        //        }
        //        con.Close();
        //    }
        //    return studnettcList;
        //}


        //public IEnumerable<Student_TC> Student_NEWTC_FormLoad()
        //{
        //    List<Student_TC> studnettcList = new List<Student_TC>();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        SqlCommand cmd = new SqlCommand("exec sp_Student_NEWTC_FormLoad" , con);
        //        cmd.CommandType = CommandType.Text;
        //        con.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        int count = ds.Tables[0].Rows.Count;
        //        for (int x = 0; x < count; x++)
        //        {
        //            Student_TC tcDetails = new Student_TC();

        //            tcDetails.LastTCNo = ds.Tables[0].Rows[x][0].ToString();
        //            tcDetails.TCCount = ds.Tables[0].Rows[x][1].ToString();
        //            studnettcList.Add(tcDetails);
        //        }
        //        con.Close();
        //    }
        //    return studnettcList;
        //}

    }
}
