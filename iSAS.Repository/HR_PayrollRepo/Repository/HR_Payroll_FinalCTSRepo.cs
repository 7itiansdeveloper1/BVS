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
    public class HR_Payroll_FinalCTSRepo : IHR_Payroll_FinalCTSRepo
    {
        public HR_Payroll_FinalCTSModels GetFinalCTSDetails(string PayBandID, string PayBandName)
        {
            HR_Payroll_FinalCTSModels ctsFinal = new HR_Payroll_FinalCTSModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_StaffCTS_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PayBandId", PayBandID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Columns.Count > 2)
                {
                    for (int i = 2; i < ds.Tables[0].Columns.Count; i++)
                    {
                        ctsFinal.HeadNameList.Add(ds.Tables[0].Columns[i].ColumnName);
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        HR_Payroll_FinalCTSStaffDetailModel newRow = new HR_Payroll_FinalCTSStaffDetailModel();
                        newRow.StaffId = ds.Tables[0].Rows[i][0].ToString();
                        newRow.StaffName = ds.Tables[0].Rows[i][1].ToString();
                        List<int> ctsDetails = new List<int>();
                        for (int j = 2; j < ds.Tables[0].Columns.Count; j++)
                        {
                            ctsDetails.Add(Convert.ToInt32(ds.Tables[0].Rows[i][j]));
                        }
                        newRow.PayBandCTSDetails.AddRange(ctsDetails);
                        ctsFinal.StaffDetails.Add(newRow);
                    }


                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        HR_Payroll_FinalCTSStaffDetailModel newRow = new HR_Payroll_FinalCTSStaffDetailModel();
                        newRow.StaffId = ds.Tables[1].Rows[i][0].ToString();
                        newRow.StaffName = ds.Tables[1].Rows[i][1].ToString();
                        List<int> ctsDetails = new List<int>();
                        for (int j = 2; j < ds.Tables[1].Columns.Count; j++)
                        {
                            ctsDetails.Add(Convert.ToInt32(ds.Tables[1].Rows[i][j]));
                        }
                        newRow.PayBandCTSDetails.AddRange(ctsDetails);
                        ctsFinal.FinalCTSList.Add(newRow);
                    }
                }

                ctsFinal.PayBandId = PayBandID;
                ctsFinal.PayBandName = PayBandName;
            }
            return ctsFinal;
        }

        public string HR_Payroll_FinalCTS_CRUD(HR_Payroll_FinalCTSModels model)
        {
            string message = "";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_StaffFinalCTS_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PayBandId", model.PayBandId);

                DataTable param = new DataTable();
                param.Columns.Add("StaffID");
                param.Columns.Add("HeadValue");

                List<HR_Payroll_FinalCTSStaffDetailModel> staffDetails = model.StaffDetails.Where(r => r.Selected).ToList();
                for (int x = 0; x < staffDetails.Count(); x++)
                {
                    DataRow row = param.NewRow();
                    row[0] = staffDetails[x].StaffId;

                    string headValue = "";
                    for (int k = 0; k < model.HeadNameList.Count; k++)
                    {
                        if (string.IsNullOrEmpty(headValue))
                            headValue = model.HeadNameList[k] + "_" + staffDetails[x].PayBandCTSDetails[k];

                        else
                            headValue += "," + model.HeadNameList[k] + "_" + staffDetails[x].PayBandCTSDetails[k];
                    }

                    row[1] = headValue;
                    param.Rows.Add(row);
                }

                cmd.Parameters.AddWithValue("@Dt", param);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                message = dt.Rows[0][1].ToString();
                return message;
            }
        }
    }
}
