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
    public class HR_Payroll_CTC_SetupRepo : IHR_Payroll_CTC_SetupRepo
    {
        public List<HR_Payroll_CTC_SetupModels> GetCTCList(string PayBandID)
        {
            List<HR_Payroll_CTC_SetupModels> ctcList = new List<HR_Payroll_CTC_SetupModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_PayBandCTC_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PayBandId", PayBandID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                ctcList = ds.Tables[0].AsEnumerable().Select(r => new HR_Payroll_CTC_SetupModels
                {
                    HeadId = r.Field<string>("HeadId"),
                    HeadName = r.Field<string>("HeadName"),
                    HeadType = r.Field<string>("HeadType"),
                    PayBandId = r.Field<string>("PayBandId"),
                    Value = r.Field<string>("Value"),
                }).ToList();
            }
            return ctcList;
        }

        public Tuple<int, string> HR_Payroll_CTC_CRUD(HR_Payroll_CTC_SetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Payroll_PayBandCTC_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PayBandId", model.PayBandId);
                cmd.Parameters.AddWithValue("@HeadId", model.HeadId);
                cmd.Parameters.AddWithValue("@HeadType", model.HeadType);
                cmd.Parameters.AddWithValue("@Value", model.Value);

                
                if(!string.IsNullOrEmpty(model.SlabList[0].Minimum) && !string.IsNullOrEmpty(model.SlabList[0].Maximum)
                   && !string.IsNullOrEmpty(model.SlabList[0].Result))
                cmd.Parameters.AddWithValue("@Slab1", model.SlabList[0].Minimum + "-" + model.SlabList[0].Maximum + "|" + model.SlabList[0].Result );

                if (!string.IsNullOrEmpty(model.SlabList[1].Minimum) && !string.IsNullOrEmpty(model.SlabList[1].Maximum)
                   && !string.IsNullOrEmpty(model.SlabList[1].Result))
                    cmd.Parameters.AddWithValue("@Slab2", model.SlabList[1].Minimum + "-" + model.SlabList[1].Maximum + "|" + model.SlabList[1].Result);

                if (!string.IsNullOrEmpty(model.SlabList[2].Minimum) && !string.IsNullOrEmpty(model.SlabList[2].Maximum)
                   && !string.IsNullOrEmpty(model.SlabList[2].Result))
                    cmd.Parameters.AddWithValue("@Slab3", model.SlabList[2].Minimum + "-" + model.SlabList[2].Maximum + "|" + model.SlabList[2].Result);

                if (!string.IsNullOrEmpty(model.SlabList[3].Minimum) && !string.IsNullOrEmpty(model.SlabList[3].Maximum)
                   && !string.IsNullOrEmpty(model.SlabList[3].Result))
                    cmd.Parameters.AddWithValue("@Slab4", model.SlabList[3].Minimum + "-" + model.SlabList[3].Maximum + "|" + model.SlabList[3].Result);

                if (!string.IsNullOrEmpty(model.SlabList[4].Minimum) && !string.IsNullOrEmpty(model.SlabList[4].Maximum)
                   && !string.IsNullOrEmpty(model.SlabList[4].Result))
                    cmd.Parameters.AddWithValue("@Slab5", model.SlabList[4].Minimum + "-" + model.SlabList[4].Maximum + "|" + model.SlabList[4].Result);

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
