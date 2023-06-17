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
    public class HR_Payroll_PayBandMasterRepo : IHR_Payroll_PayBandMasterRepo
    {
        public List<HR_Payroll_PayBandMasterModels> GetPayBandList(string PayBandID)
        {
            List<HR_Payroll_PayBandMasterModels> payBandList = new List<HR_Payroll_PayBandMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_PayBandMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PayBandId", PayBandID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                payBandList = ds.Tables[0].AsEnumerable().Select(r => new HR_Payroll_PayBandMasterModels
                {
                    Active = r.Field<bool>("Active"),
                    Discrption = r.Field<string>("Discrption"),
                    PayBandId = r.Field<string>("PayBandId"),
                    PayBandName = r.Field<string>("PayBandName"),
                    PrintOrder = r.Field<Int16>("PrintOrder"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                }).ToList();
            }
            return payBandList;
        }

        public HR_Payroll_PayBandMasterModels GetPayBandByID(string PayBandID)
        {
            return GetPayBandList(PayBandID).FirstOrDefault();
        }

        public Tuple<int, string> HR_Payroll_PayBandMaster_CRUD(HR_Payroll_PayBandMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_PayBandMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PayBandId", model.PayBandId);
                cmd.Parameters.AddWithValue("@PayBandName", model.PayBandName);
                cmd.Parameters.AddWithValue("@Discrption", model.Discrption);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Active", model.Active);

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

        public Tuple<int, string> HR_Payroll_PayBandMaster_CRUD(string PayBandID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_PayBandMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PayBandId", PayBandID);
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
