using ISas.Entities.RegistrationEntities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ISas.Repository.Implementation
{
    public class CountryRepository : ICountryRepository
    {
        public IEnumerable<School_CountryMaster> GetAllCountryList()
        {
            List<School_CountryMaster> countryList = new List<School_CountryMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from GetCountry()", con);
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
                    School_CountryMaster country = new School_CountryMaster();
                    country.CountryID = ds.Tables[0].Rows[x][0].ToString();
                    country.CountryName = ds.Tables[0].Rows[x][1].ToString();
                    country.Default = Convert.ToBoolean(ds.Tables[0].Rows[x][2]);
                    country.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3].ToString());
                    countryList.Add(country);
                }
                con.Close();
            }
            return countryList;
        }
    }


}
