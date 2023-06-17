using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.HR_PayrollRepo.Repository
{
    public class HR_Payroll_AttendanceProcessRepo : IHR_Payroll_AttendanceProcessRepo
    {
        public HR_Payroll_AttendanceProcessModels GetAttenProcessFormLoadDetails(string SessionId)
        {
            HR_Payroll_AttendanceProcessModels formLoadDetails = new HR_Payroll_AttendanceProcessModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_AttendnaceProcess_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                formLoadDetails.MonthList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("MONTH"),
                    Value = r.Field<string>("MonthDate"),
                }).ToList();

                formLoadDetails.PayBandList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("PayBandName"),
                    Value = r.Field<string>("PayBandId"),
                }).ToList();
            }
            return formLoadDetails;
        }


        public HR_Payroll_AttendanceProcessModels GetMonthleyAttenList(string PayBandId, string MonthDate)
        {
            HR_Payroll_AttendanceProcessModels attenDetails = new HR_Payroll_AttendanceProcessModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_AttendnaceProcess_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PayBandId", PayBandId);
                cmd.Parameters.AddWithValue("@MonthDate", MonthDate);
                cmd.Parameters.AddWithValue("@Mode", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Columns.Count > 2)
                {
                    for (int i = 2; i < ds.Tables[0].Columns.Count; i++)
                    {
                        attenDetails.HeadNameList.Add(ds.Tables[0].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        HR_Payroll_MonthlyAttenDetailModels newRow = new HR_Payroll_MonthlyAttenDetailModels();
                        newRow.StaffID = ds.Tables[0].Rows[i][0].ToString();
                        newRow.StaffName = ds.Tables[0].Rows[i][1].ToString();
                        List<string> attenDetail = new List<string>();
                        for (int j = 2; j < ds.Tables[0].Columns.Count; j++)
                        {
                            attenDetail.Add(ds.Tables[0].Rows[i][j].ToString());
                        }
                        newRow.ValuesList.AddRange(attenDetail);
                        attenDetails.MontyelyAttenList.Add(newRow);
                    }
                }
            }
            return attenDetails;
        }
    }
}
