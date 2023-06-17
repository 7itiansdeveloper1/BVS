using ISas.Entities.RegistrationEntities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ISas.Repository.Implementation
{
    public class School_DocumentMasterRepo : ISchool_DocumentMasterRepo
    {
        public IEnumerable<School_DocumentMaster> GetAllStudentDocumentList()
        {
            List<School_DocumentMaster> studnetDocumentList = new List<School_DocumentMaster>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("select * from GetStudentDocument()", con);
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
                    School_DocumentMaster studnetDoc = new School_DocumentMaster();
                    studnetDoc.DocNo = ds.Tables[0].Rows[x][0].ToString();
                    studnetDoc.DocName = ds.Tables[0].Rows[x][1].ToString();
                    studnetDoc.PrintOrder = Convert.ToInt32(ds.Tables[0].Rows[x][2].ToString());
                    studnetDocumentList.Add(studnetDoc);
                }
                con.Close();
            }
            return studnetDocumentList;
        }
    }
}
