using ISas.Entities.RegistrationEntities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ISas.Repository.Implementation
{
    public class StateRepository : IStateRepository
    {
        public IEnumerable<School_StateMaster> GetStateListByCountryID(string CountryID)
        {
            List<School_StateMaster> stateList = new List<School_StateMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from GetStateByCountryID('" + CountryID + "')", con);
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
                    School_StateMaster state = new School_StateMaster();
                    state.StateID = ds.Tables[0].Rows[x][0].ToString();
                    state.StateName = ds.Tables[0].Rows[x][1].ToString();
                    state.Default = Convert.ToBoolean(ds.Tables[0].Rows[x][2]);
                    state.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][3].ToString());
                    stateList.Add(state);
                }
                con.Close();
            }
            return stateList;
        }
    }
}
