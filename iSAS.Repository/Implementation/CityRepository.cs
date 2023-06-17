using ISas.Entities.RegistrationEntities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ISas.Repository.Implementation
{
    public class CityRepository : ICityRepository
    {
        public IEnumerable<School_CityMaster> GetCityListByStateID(string StateID)
        {
            List<School_CityMaster> cityList = new List<School_CityMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from GetCityByStateID('" + StateID + "')", con);
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
                    School_CityMaster city = new School_CityMaster();
                    city.CityID = ds.Tables[0].Rows[x][0].ToString();
                    city.CityName = ds.Tables[0].Rows[x][1].ToString();
                    city.Default = Convert.ToBoolean(ds.Tables[0].Rows[x][2]);
                    city.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3].ToString());
                    cityList.Add(city);
                }
                con.Close();
            }
            return cityList;
        }
    }
}
