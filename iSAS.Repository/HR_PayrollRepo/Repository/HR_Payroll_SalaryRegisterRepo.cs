using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.HR_PayrollRepo.Repository
{
    public class HR_Payroll_SalaryRegisterRepo : IHR_Payroll_SalaryRegisterRepo
    {
        public HR_Payroll_SalaryRegisterModels GetSalaryRegisterFormLoadDetails(string SessionId)
        {
            HR_Payroll_SalaryRegisterModels formLoadDetails = new HR_Payroll_SalaryRegisterModels();
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

        public HR_Payroll_SalaryRegisterModels GetSalaryRegisterList(string PayBandId, string MonthDate)
        {
            HR_Payroll_SalaryRegisterModels salaryRegDetails = new HR_Payroll_SalaryRegisterModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_SalaryRegister", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PayBandId", PayBandId);
                cmd.Parameters.AddWithValue("@MonthDate", MonthDate);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                salaryRegDetails.PayBandEmpList = ds.Tables[0].AsEnumerable().Select(r => new HR_Payroll_PayBandEmpModels
                {
                     IsSalaryCalculate = r.Field<bool>("IsSalaryCalculate"),
                     StaffId = r.Field<string>("StaffId"),
                     StaffName = r.Field<string>("StaffName"),
                }).ToList();

                if (ds.Tables[1] != null && ds.Tables[1].Columns.Count > 2)
                {
                    for (int i = 2; i < ds.Tables[1].Columns.Count; i++)
                    {
                        salaryRegDetails.HeadNameList.Add(ds.Tables[1].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        HR_Payroll_MonthlyAttenDetailModels newRow = new HR_Payroll_MonthlyAttenDetailModels();
                        newRow.StaffID = ds.Tables[1].Rows[i][0].ToString();
                        newRow.StaffName = ds.Tables[1].Rows[i][1].ToString();
                        List<string> attenDetail = new List<string>();
                        for (int j = 2; j < ds.Tables[1].Columns.Count; j++)
                        {
                            attenDetail.Add(ds.Tables[1].Rows[i][j].ToString());
                        }
                        newRow.ValuesList.AddRange(attenDetail);
                        salaryRegDetails.SalaryRegisterList.Add(newRow);
                    }
                }
            }
            return salaryRegDetails;
        }


        public Tuple<int, string> HR_Payroll_SalaryRegister_CRUD(HR_Payroll_SalaryRegisterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_SalaryMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MonthDate", model.AttenMonthID);
                cmd.Parameters.AddWithValue("@StaffId", string.Join(",", model.PayBandEmpList.Where(r => r.Selected).Select(r => r.StaffId).ToList()));

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
