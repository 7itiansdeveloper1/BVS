using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Library.Repository
{
    public class Library_ReportRepo: ILibrary_ReportRepo
    {
        public Library_ReportModel Library_Transaction_GetReportList()
        {
            Library_ReportModel model = new Library_ReportModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_LibReport_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetReportList");
                cmd.Parameters.AddWithValue("@reportId", null);
                cmd.Parameters.AddWithValue("@fromDate", null);
                cmd.Parameters.AddWithValue("@toDate", null);
                cmd.Parameters.AddWithValue("@sessionId", null);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                
                model.ReportNameList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Value = r.Field<string>("reportId"),
                    Text = r.Field<string>("reportDisplayName"),
                }).ToList();
                model.fromDate= DateTime.Now.ToShortDateString().Replace("-", "/");
                model.toDate = DateTime.Now.ToShortDateString().Replace("-", "/");

            }
            return model;
        }


        public Library_ReportModel Library_Transaction_GetReport(int reportId,string fromDate, string toDate, string sessionId)
        {
            Library_ReportModel model = new Library_ReportModel();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_LibReport_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetReport");
                cmd.Parameters.AddWithValue("@reportId", reportId);
                cmd.Parameters.AddWithValue("@fromDate", Convert.ToDateTime(fromDate).Date);
                cmd.Parameters.AddWithValue("@toDate", Convert.ToDateTime(toDate).Date);
                cmd.Parameters.AddWithValue("@sessionId", sessionId);
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


        public DataSet GetStudentDetailReport_Crystal(string reportValue, string sessionId, string userId, string filter1Value)
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
