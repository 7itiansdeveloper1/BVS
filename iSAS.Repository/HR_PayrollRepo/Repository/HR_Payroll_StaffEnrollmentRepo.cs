using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.HR_PayrollRepo.Repository
{
    public class HR_Payroll_StaffEnrollmentRepo : IHR_Payroll_StaffEnrollmentRepo
    {
        public HR_Payroll_StaffEnrollmentModels GetStaffEnrollmentDetails(string PayBandID, string PayBandName)
        {
            HR_Payroll_StaffEnrollmentModels staffErnolmentDetails = new HR_Payroll_StaffEnrollmentModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_StaffEnrollment_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PayBandId", PayBandID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                staffErnolmentDetails.NonEnrolledStaffList = ds.Tables[0].AsEnumerable().Select(r => new HR_Payroll_StaffDetailsModels
                {
                    DeptName = r.Field<string>("DeptName"),
                    StaffID = r.Field<string>("StaffID"),
                    StaffName = r.Field<string>("StaffName")
                }).ToList();

                staffErnolmentDetails.EnrolledStaffList = ds.Tables[1].AsEnumerable().Select(r => new HR_Payroll_StaffDetailsModels
                {
                    DeptName = r.Field<string>("DeptName"),
                    StaffID = r.Field<string>("StaffID"),
                    StaffName = r.Field<string>("StaffName"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                }).ToList();

                staffErnolmentDetails.PayBandId = PayBandID;
                staffErnolmentDetails.PayBandName = PayBandName;
            }
            return staffErnolmentDetails;
        }
        public Tuple<int, string> HR_Payroll_StaffEnrollment_CRUD(HR_Payroll_StaffEnrollmentModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_StaffEnrollment_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PayBandId", model.PayBandId);
                cmd.Parameters.AddWithValue("@StaffId", string.Join(",", model.NonEnrolledStaffList.Where(r => r.Selected).Select(r => r.StaffID).ToList()));

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
        public Tuple<int, string> HR_Payroll_StaffEnrollment_CRUD(string PayBandID, string StaffID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_StaffEnrollment_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PayBandId", PayBandID);
                cmd.Parameters.AddWithValue("@StaffId", StaffID);

                cmd.Parameters.AddWithValue("@UserId", "");
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

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
