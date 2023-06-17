using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_DefaulterDetailsRepo : IFee_DefaulterDetailsRepo
    {
        public Fee_FilterDefaulterDetailModel GetDefaulterDetails(string SessionId, string DueDate, string ClassId, string FeeCategoryId, string DefaulterType, string ReportType) //string SectionId, 
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_FilterDefaulterDetailModel model = new Fee_FilterDefaulterDetailModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DefaulterReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@InstallmentId", DueDate);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                //cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@FeeStructure", FeeCategoryId);
                cmd.Parameters.AddWithValue("@DefaulterType ", DefaulterType);
                cmd.Parameters.AddWithValue("@ReportType", ReportType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.ReportHeaders.Header1 = ds.Tables[0].Rows[0][0].ToString();
                    model.ReportHeaders.Header2 = ds.Tables[0].Rows[0][1].ToString();
                    model.ReportHeaders.Header3 = ds.Tables[0].Rows[0][2].ToString();
                    model.ReportHeaders.Header4 = ds.Tables[0].Rows[0][3].ToString();
                    model.ReportHeaders.LogoURL = ds.Tables[0].Rows[0][4].ToString();
                    model.ReportHeaders.ReportName = ds.Tables[1].Rows[0][0].ToString();
                }

                model.DefaulterList = ds.Tables[2].AsEnumerable().Select(r => new Fee_DefaulterDetailsModels
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Balance = r.Field<int>("Balance"),
                    Class = r.Field<string>("Class"),
                    Duration = r.Field<string>("Duration"),
                    ERPNo = r.Field<string>("ERPNo"),
                    SMSNo = r.Field<string>("SMSNo"),
                    Student = r.Field<string>("Student"),
                    FatherName = r.Field<string>("FatherName"),
                }).ToList();

                if (ds.Tables[3] != null)
                {
                    for (int i = 0; i < ds.Tables[3].Columns.Count; i++)
                    {
                        model.HeaderNameList1.Add(ds.Tables[3].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        List<string> rowVal = new List<string>();
                        for (int j = 0; j < ds.Tables[3].Columns.Count; j++)
                        {
                            rowVal.Add(ds.Tables[3].Rows[i][j].ToString());
                        }
                        model.ValueList1.Add(rowVal);
                    }
                }


                if (ds.Tables[4] != null)
                {
                    for (int i = 0; i < ds.Tables[4].Columns.Count; i++)
                    {
                        model.HeaderNameList2.Add(ds.Tables[4].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    {
                        List<string> rowVal = new List<string>();
                        for (int j = 0; j < ds.Tables[4].Columns.Count; j++)
                        {
                            rowVal.Add(ds.Tables[4].Rows[i][j].ToString());
                        }
                        model.ValueList2.Add(rowVal);
                    }
                }
                return model;
            }
        }
        public Fee_FilterDefaulterDetailModel GetDefaulterDetails1(string SessionId, string DueDate, string ClassId, string FeeCategoryId, string DefaulterType, bool IncludeInactive, bool IncludeNonAdmitted, bool IncludePaid) //string SectionId, 
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_FilterDefaulterDetailModel model = new Fee_FilterDefaulterDetailModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_DefaulterReport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@InstallmentId", DueDate);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                //cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@FeeStructure", FeeCategoryId);
                cmd.Parameters.AddWithValue("@DefaulterType ", DefaulterType);
                cmd.Parameters.AddWithValue("@IncludeInactive", IncludeInactive);
                cmd.Parameters.AddWithValue("@IncludeNonAdmitted ", IncludeNonAdmitted);
                cmd.Parameters.AddWithValue("@IncludePaid", IncludePaid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.ReportHeaders.Header1 = ds.Tables[0].Rows[0][0].ToString();
                    model.ReportHeaders.Header2 = ds.Tables[0].Rows[0][1].ToString();
                    model.ReportHeaders.Header3 = ds.Tables[0].Rows[0][2].ToString();
                    model.ReportHeaders.Header4 = ds.Tables[0].Rows[0][3].ToString();
                    model.ReportHeaders.LogoURL = ds.Tables[0].Rows[0][4].ToString();
                    model.ReportHeaders.ReportName = ds.Tables[1].Rows[0][0].ToString();
                }

                model.DefaulterList = ds.Tables[2].AsEnumerable().Select(r => new Fee_DefaulterDetailsModels
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    Balance = r.Field<int>("Balance"),
                    Class = r.Field<string>("Class"),
                    Duration = r.Field<string>("Duration"),
                    ERPNo = r.Field<string>("ERPNo"),
                    SMSNo = r.Field<string>("SMSNo"),
                    Student = r.Field<string>("Student"),
                    FatherName = r.Field<string>("FatherName"),
                }).ToList();

                if (ds.Tables[3] != null)
                {
                    for (int i = 0; i < ds.Tables[3].Columns.Count; i++)
                    {
                        model.HeaderNameList1.Add(ds.Tables[3].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        List<string> rowVal = new List<string>();
                        for (int j = 0; j < ds.Tables[3].Columns.Count; j++)
                        {
                            rowVal.Add(ds.Tables[3].Rows[i][j].ToString());
                        }
                        model.ValueList1.Add(rowVal);
                    }
                }


                if (ds.Tables[4] != null)
                {
                    for (int i = 0; i < ds.Tables[4].Columns.Count; i++)
                    {
                        model.HeaderNameList2.Add(ds.Tables[4].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                    {
                        List<string> rowVal = new List<string>();
                        for (int j = 0; j < ds.Tables[4].Columns.Count; j++)
                        {
                            rowVal.Add(ds.Tables[4].Rows[i][j].ToString());
                        }
                        model.ValueList2.Add(rowVal);
                    }
                }
                return model;
            }
        }

        public DataSet GetDefaulterLetterDetails(Fee_FilterDefaulterDetailModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Report_DefaulterLetter", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", model.SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", string.Join(",", model.DefaulterList.Where(r => r.Selected).Select(r=> r.ERPNo).ToList()));
                cmd.Parameters.AddWithValue("@InstallmentId", model.InstallmentId);
                cmd.Parameters.AddWithValue("@FeeStructure", model.FeeCategoryId);
                cmd.Parameters.AddWithValue("@DefaulterType", model.DefaulterType);
                cmd.Parameters.AddWithValue("@LetterDate ", model.Date);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds;
            }
        }

        public string GetSMSTextForFee()
        {
            string smsText = "";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_Defaulter_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                    smsText = ds.Tables[0].Rows[0][1].ToString();
            }
            return smsText;
        }
    }
}
