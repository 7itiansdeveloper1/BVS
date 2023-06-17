using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ISas.Entities.Exam_Entities;
using ISas.Repository.ExamRepository.IRepository;

namespace ISas.Repository.ExamRepository.Repository
{
    public class Exam_ReportsRepo: IExam_ReportsRepo
    {
        public List<SelectListItem> Exam_Report_Transaction_GetReportList(string sessionId, string userId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Exam_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ReportId", "");
                cmd.Parameters.AddWithValue("@ClassSectionId", "");
                cmd.Parameters.AddWithValue("@SubjectId", "");
                cmd.Parameters.AddWithValue("@Mode", "GetReportList");
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

            }
            return dt.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Field<string>("ReportName"),
                Value = r.Field<string>("ReportId"),
            }).ToList();
        }
        public List<SelectListItem> Exam_Report_Transaction_GetClassList(string sessionId, string reportId, string userId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Exam_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ReportId", reportId);
                cmd.Parameters.AddWithValue("@ClassSectionId", "");
                cmd.Parameters.AddWithValue("@SubjectId", "");
                cmd.Parameters.AddWithValue("@Mode", "GetClassList");
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

            }
            return dt.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Field<string>("ClassName"),
                Value = r.Field<string>("ClassSectionId"),
            }).ToList();
        }
        public List<SelectListItem> Exam_Report_Transaction_GetSubjectList(string sessionId, string reportId, string classSectionId,  string userId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Exam_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ReportId", reportId);
                cmd.Parameters.AddWithValue("@ClassSectionId", classSectionId);
                cmd.Parameters.AddWithValue("@SubjectId", "");
                cmd.Parameters.AddWithValue("@Mode", "GetSubjectsList");
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();

            }
            return dt.AsEnumerable().Select(r => new SelectListItem
            {
                Text = r.Field<string>("SubjectName"),
                Value = r.Field<string>("SubjectId"),
            }).ToList();
        }
        public Exam_ReportModels GetReportData(string sessionId, string reportId, string classSectionId, string subjectId, string userId)
        {
            Exam_ReportModels model = new Exam_ReportModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Exam_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ReportId", reportId);
                cmd.Parameters.AddWithValue("@ClassSectionId", classSectionId);
                cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                cmd.Parameters.AddWithValue("@Mode", "GetReportData");
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.ReportHeader.Header1 = ds.Tables[0].Rows[0][0].ToString();
                model.ReportHeader.Header2 = ds.Tables[0].Rows[0][1].ToString();
                model.ReportHeader.Header3 = ds.Tables[0].Rows[0][2].ToString();
                model.ReportHeader.Header4 = ds.Tables[0].Rows[0][3].ToString();
                model.ReportHeader.LogoURL = ds.Tables[0].Rows[0][4].ToString();
                model.ReportHeader.ReportName = ds.Tables[1].Rows[0][0].ToString();

                for (int i = 0; i < ds.Tables[2].Columns.Count; i++)
                {
                    model.HeaderNameList.Add(ds.Tables[2].Columns[i].ColumnName);
                }

                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    List<string> rowVal = new List<string>();
                    for (int j = 0; j < ds.Tables[2].Columns.Count; j++)
                    {
                        rowVal.Add(ds.Tables[2].Rows[i][j].ToString());
                    }
                    model.ValueList.Add(rowVal);
                }
            }
            return model;
        }
    }
}
