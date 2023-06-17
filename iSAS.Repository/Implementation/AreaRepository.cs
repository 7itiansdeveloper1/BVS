using ISas.Entities.RegistrationEntities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ISas.Repository.Implementation
{
    public class AreaRepository : IAreaRepository
    {
        public IEnumerable<School_AreaMaster> GetAllAreaList()
        {
            List<School_AreaMaster> areaList = new List<School_AreaMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from GetArea()", con);
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
                    School_AreaMaster area = new School_AreaMaster();
                    area.AreaID = ds.Tables[0].Rows[x][0].ToString();
                    area.AreaName = ds.Tables[0].Rows[x][1].ToString();
                    area.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][2].ToString());
                    areaList.Add(area);
                }
                con.Close();
            }
            return areaList;
        }
    }
}
