using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Repository.Interface;
using ISas.Entities;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ISas.Repository.Implementation
{
    public class Exam_ResultRepo : IExam_Result
    {

        public IEnumerable<ExamNameList> GetExamList(string SessionId, string UserId)
        {
            List<ExamNameList> examnamelist = new List<ExamNameList>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Exam_Result_FormLoad", con);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public StudentGradeCardViewModel GetStudentGardeCardView(string examid, string erpno,string sessionid)
        {
            StudentGradeCardViewModel model = new StudentGradeCardViewModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Exam_GradeCardTemplate_DashboardView", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamId", examid);
                cmd.Parameters.AddWithValue("@ERPNo", erpno);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    model.HeaderNameList.Add(ds.Tables[0].Columns[i].ColumnName);
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    List<string> rowVal = new List<string>();
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        rowVal.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                    model.ValueList.Add(rowVal);
                }
            }
            return model;
        }

    }
}
