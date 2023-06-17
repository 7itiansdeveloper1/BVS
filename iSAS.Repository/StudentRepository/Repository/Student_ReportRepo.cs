using ISas.Entities;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.StudentRegistrationRepository.Repository
{
    public class Student_ReportRepo : IStudent_ReportRepo
    {
        public Student_ReportModel GetStudentReport_FormLoad(string ModuleName, string ReportName,string UserId)
        {
            Student_ReportModel model = new Student_ReportModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModuleName", ModuleName);
                cmd.Parameters.AddWithValue("@ReportType", ReportName);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ClassSectionList = ds.Tables[0].AsEnumerable().Select(r => new ClassSectionModel
                {
                    ClassSectionId = r.Field<string>("ClassSectionId"),
                    ClassSectionName = r.Field<string>("ClassNameWithSection"),
                    WingId = r.Field<string>("WingId"),
                    ClassName = r.Field<string>("ClassName"),
                }).ToList();

                model.ReportNameList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("ReportName"),
                    Text = r.Field<string>("ReportDisplayName"),
                }).ToList();

                model.WingList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("WingID"),
                    Text = r.Field<string>("WingName"),
                }).ToList();

            }
            return model;
        }



        public List<SelectListItem> GetReportNameList(string ReportType,string userid)
        {
            List<SelectListItem> reportNameList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Report_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ModuleName", "Students");
                cmd.Parameters.AddWithValue("@ReportType", ReportType);
                cmd.Parameters.AddWithValue("@UserId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();


                reportNameList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("ReportName"),
                    Text = r.Field<string>("ReportDisplayName"),
                }).ToList();
            }
            return reportNameList;
        }


        public Student_ReportModel GetStudentDetailReport(string ClassSectionId, string Reporttype, bool InActive, string OrderBy, string ReportName, string WingId,string sessionId)
        {
            Student_ReportModel model = new Student_ReportModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_DetailReports", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (string.IsNullOrEmpty(WingId))
                    WingId = null;

                if (string.IsNullOrEmpty(ClassSectionId))
                    ClassSectionId = null;

                cmd.Parameters.AddWithValue("@WingId", WingId);
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSectionId);
                cmd.Parameters.AddWithValue("@Reporttype", Reporttype);
                cmd.Parameters.AddWithValue("@InActive", InActive);
                cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
                cmd.Parameters.AddWithValue("@ReportName", ReportName);
                cmd.Parameters.AddWithValue("@SessionID", sessionId);
                cmd.CommandTimeout = 0;
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
               // model.SelectedReportName = ds.Tables[1].Rows[0][0].ToString();
            }
            return model;
        }


        public DataSet GetStudentDetailReport_Crystal(string reportValue, string sessionId, string userId,string filter1Value)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ReportEngine", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportName", reportValue);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@ParameterValue", filter1Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }
    }
}
