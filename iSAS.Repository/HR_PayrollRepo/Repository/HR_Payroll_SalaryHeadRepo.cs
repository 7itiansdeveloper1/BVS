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
    public class HR_Payroll_SalaryHeadRepo : IHR_Payroll_SalaryHeadRepo
    {
        public List<HR_Payroll_SalaryHeadModels> GetSalaryHeadList(string HeadID)
        {
            List<HR_Payroll_SalaryHeadModels> salaryHeadList = new List<HR_Payroll_SalaryHeadModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_HeadMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@HeadId", HeadID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                salaryHeadList = ds.Tables[0].AsEnumerable().Select(r => new HR_Payroll_SalaryHeadModels
                {
                    Daywise = r.Field<bool>("Daywise"),
                    Discrption = r.Field<string>("Discrption"),
                    ExpansetoComp = r.Field<bool>("ExpansetoComp"),
                    HeadID = r.Field<string>("HeadID"),
                    HeadName = r.Field<string>("HeadName"),
                    HeadType = r.Field<string>("HeadType"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                }).ToList();
            }
            return salaryHeadList;
        }

        public HR_Payroll_SalaryHeadModels GetSalaryHeadByID(string HeadID)
        {
            return GetSalaryHeadList(HeadID).FirstOrDefault();
        }

        public Tuple<int, string> HR_Payroll_SalaryHead_CRUD(HR_Payroll_SalaryHeadModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_HeadMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeadId", model.HeadID);
                cmd.Parameters.AddWithValue("@HeadName", model.HeadName);
                cmd.Parameters.AddWithValue("@Desc", model.Discrption);
                cmd.Parameters.AddWithValue("@HeadType", model.HeadType);
                cmd.Parameters.AddWithValue("@ExptoComp", model.ExpansetoComp);
                cmd.Parameters.AddWithValue("@DayWise", model.Daywise);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);

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

        public Tuple<int, string> HR_Payroll_SalaryHead_CRUD(string HeadID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_HeadMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HeadId", HeadID);
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
