using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.HR_PayrollRepo.Repository
{
    public class HR_Payroll_LeaveRegisterRepo : IHR_Payroll_LeaveRegisterRepo
    {
        public HR_Payroll_LeaveRegisterModels GetFormLoadDetails()
        {
            HR_Payroll_LeaveRegisterModels formLoadDetails = new HR_Payroll_LeaveRegisterModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveRegister_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", "");
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                formLoadDetails.StaffList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("StaffID"),
                }).ToList();

                formLoadDetails.LeaveCodeList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("LvCode"),
                    Value = r.Field<string>("LvID"),
                }).ToList();
            }
            return formLoadDetails;
        }


        public int GetLeaveAnnualQuota(string LeaveID)
        {
            int annQuota = 0;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveRegister_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", "");
                cmd.Parameters.AddWithValue("@Mode", "GetLeaveQuota");
                cmd.Parameters.AddWithValue("@LeaveId", LeaveID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                int.TryParse(ds.Tables[0].Rows[0][0].ToString(), out annQuota);
                return annQuota;
            }
        }


        public HR_Payroll_LeaveRegisterModels GetLeaveDetails(string EmpID)
        {
            HR_Payroll_LeaveRegisterModels formLoadDetails = new HR_Payroll_LeaveRegisterModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveRegister_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", EmpID);
                cmd.Parameters.AddWithValue("@Mode", "GetOpenBalance");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                formLoadDetails.LeaveList = ds.Tables[1].AsEnumerable().Select(r => new LeaveDetailsModels
                {
                    days = r.Field<Int16>("days"),
                    EmpID = r.Field<string>("EmpID"),
                    FromDate = r.Field<string>("FromDate"),
                    LvCode = r.Field<string>("LvCode"),
                    LvID = r.Field<string>("LvID"),
                    ToDate = r.Field<string>("ToDate"),
                    TransID = r.Field<int>("TransID"),
                    TransType = r.Field<string>("TransType"),
                }).ToList();
            }
            return formLoadDetails;
        }

        public HR_Payroll_LeaveRegisterModels GetLocalLeaveDetails(string EmpID)
        {
            HR_Payroll_LeaveRegisterModels formLoadDetails = new HR_Payroll_LeaveRegisterModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveRegister_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpID", EmpID);
                cmd.Parameters.AddWithValue("@Mode", "GetOpenBalance");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                formLoadDetails.LocalLeaveList = ds.Tables[0].AsEnumerable().Select(r => new LocalLeaveModels
                {
                    OpenBal = r.Field<Int16>("OpenBal"),
                    OpenDate = r.Field<string>("OpenDate"),
                    LvCode = r.Field<string>("LvCode"),
                    LvID = r.Field<string>("LvID"),
                }).ToList();
            }
            return formLoadDetails;
        }


        public Tuple<int, string> HR_Payroll_LeaveRegister_CRUD(HR_Payroll_LeaveRegisterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveRegister_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpID", model.SelectedEmpID);
                cmd.Parameters.AddWithValue("@LvID", model.LeaveCode);
                cmd.Parameters.AddWithValue("@Days", model.BalanceDays);
                if(!string.IsNullOrEmpty(model.FromDate))
                cmd.Parameters.AddWithValue("@FDate", Convert.ToDateTime(model.FromDate).Date);

                if (!string.IsNullOrEmpty(model.ToDate))
                    cmd.Parameters.AddWithValue("@TDate", Convert.ToDateTime(model.ToDate).Date);

                cmd.Parameters.AddWithValue("@TransType", model.LeaveType);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

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
