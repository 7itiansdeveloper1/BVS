using ISas.Entities.RegistrationEntities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ISas.Repository.Implementation
{
    public class SchoolAdmissionCategoryRepo : ISchoolAdmissionCategoryRepo
    {
        public IEnumerable<School_AdmissionCategoryMaster> GetAllAdmissionCategoryList()
        {
            List<School_AdmissionCategoryMaster> admCategoryList = new List<School_AdmissionCategoryMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from fun_GetSchoolAdmissionCategory()", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //SqlDataReader dr = cmd.ExecuteReader();
                //ds.Tables[0].Rows
                int count = ds.Tables[0].Rows.Count;
                for (int x = 0; x < count; x++)
                {
                    School_AdmissionCategoryMaster admMaster = new School_AdmissionCategoryMaster();
                    admMaster.AdmCategoryId = ds.Tables[0].Rows[x][0].ToString();
                    admMaster.AdmCategoryName = ds.Tables[0].Rows[x][1].ToString();
                    admMaster.IsDefault = Convert.ToBoolean(ds.Tables[0].Rows[x][2]);
                    admMaster.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3].ToString());
                    admCategoryList.Add(admMaster);
                }
                con.Close();
            }
            return admCategoryList;
        }
    }
}
