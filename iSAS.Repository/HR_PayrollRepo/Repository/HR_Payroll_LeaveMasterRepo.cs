using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.HR_PayrollRepo.Repository
{
    public class HR_Payroll_LeaveMasterRepo : IHR_Payroll_LeaveMasterRepo
    {
        public List<HR_Payroll_LeaveMasterModels> GetLeaveMasterList(string LvID)
        {
            List<HR_Payroll_LeaveMasterModels> leaveMstList = new List<HR_Payroll_LeaveMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LvID", LvID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                leaveMstList = ds.Tables[0].AsEnumerable().Select(r => new HR_Payroll_LeaveMasterModels
                {
                    AnnQuota = r.Field<Int16>("AnnQuota"),
                    Balance = r.Field<bool>("Balance"),
                    CF = r.Field<bool>("CF"),
                    Encash = r.Field<bool>("Encash"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    IsEditable = r.Field<bool>("IsEditable"),
                    LeaveType = r.Field<string>("LeaveType"),
                    LvCode = r.Field<string>("LvCode"),
                    LvID = r.Field<string>("LvID"),
                    MaxAllow = r.Field<Int16>("MaxAllow"),
                    Paid = r.Field<bool>("Paid"),
                    Running = r.Field<bool>("Running"),
                }).ToList();
            }
            return leaveMstList;
        }

        public HR_Payroll_LeaveMasterModels GetLeaveMasterByID(string LvID)
        {
            return GetLeaveMasterList(LvID).FirstOrDefault();
        }

        public Tuple<int, string> HR_Payroll_LeaveMaster_CRUD(HR_Payroll_LeaveMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LvID", model.LvID);
                cmd.Parameters.AddWithValue("@LvCode", model.LvCode);
                cmd.Parameters.AddWithValue("@LeaveType", model.LeaveType);
                cmd.Parameters.AddWithValue("@CF", model.CF);
                cmd.Parameters.AddWithValue("@AnnQuota", model.AnnQuota);
                cmd.Parameters.AddWithValue("@MaxAllow", model.MaxAllow);
                cmd.Parameters.AddWithValue("@Paid", model.Paid);
                cmd.Parameters.AddWithValue("@Balance", model.Balance);
                cmd.Parameters.AddWithValue("@Running", model.Running);
                cmd.Parameters.AddWithValue("@Encash", model.Encash);

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

        public Tuple<int, string> HR_Payroll_LeaveMaster_CRUD(string LvID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_HR_LeaveMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LvID", LvID);
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
