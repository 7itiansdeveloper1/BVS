using ISas.Entities;
using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_FeeBillReportRepo : IFee_FeeBillReportRepo
    {
        public Fee_FeeBillReportHtmlModel Fee_BillReportDetails(Fee_FeeBillReportModels param)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Fee_FeeBillReportHtmlModel model = new Fee_FeeBillReportHtmlModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_Report_FeeBill", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo",param.Selected_ERPNos);
                cmd.Parameters.AddWithValue("@SessionId", param.SessionId);

                if (!string.IsNullOrEmpty(param.InstallmentId))
                    cmd.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(param.InstallmentId).Date);
                else
                    cmd.Parameters.AddWithValue("@DueDate", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.ReportHeader.Header1 = ds.Tables[0].Rows[0][0].ToString();
                    model.ReportHeader.Header2 = ds.Tables[0].Rows[0][1].ToString();
                    model.ReportHeader.Header3 = ds.Tables[0].Rows[0][2].ToString();
                    model.ReportHeader.Header4 = ds.Tables[0].Rows[0][3].ToString();
                    model.ReportHeader.LogoURL = ds.Tables[0].Rows[0][4].ToString();
                }

                if (ds.Tables[1].Rows.Count > 0)
                    model.ReportHeader.ReportName = ds.Tables[1].Rows[0][0].ToString();

                model.StudentList = ds.Tables[2].AsEnumerable().Select(r => new Basic_StudentInfoModel
                {
                    ERP = r.Field<string>("ERP"),
                    DOA = r.Field<string>("DOA"),
                    AdmNo = r.Field<string>("AdmNo"),
                    RollNo = r.Field<int>("RollNo"),
                    Student = r.Field<string>("Student"),
                    Class = r.Field<string>("Class"),
                    Gender = r.Field<string>("Gender"),
                    DOB = r.Field<string>("DOB"),
                    Father = r.Field<string>("Father"),
                    Mother = r.Field<string>("Mother"),
                    Address = r.Field<string>("Address"),
                    FMobileNo = r.Field<string>("FMobileNo"),
                    MMobileNo = r.Field<string>("MMobileNo"),
                    SMSNo = r.Field<string>("SMSNo"),
                    AlternateNumber = r.Field<string>("Alternate Number"),
                }).ToList();

                model.HeadDetails = ds.Tables[3].AsEnumerable().Select(r => new HeadDetailsModel
                {
                    HeadName = r.Field<string>("HeadName"),
                    Due = r.Field<int>("Due"),
                    Paid = r.Field<int>("Paid"),
                    ReceiptNo = r.Field<string>("E6"),
                    InvoiceNo = r.Field<string>("E5"),
                    Period = r.Field<string>("E4"),
                    ERPNo = r.Field<string>("ERPNo"),
                }).ToList();

                return model;
            }
        }
    }
}
