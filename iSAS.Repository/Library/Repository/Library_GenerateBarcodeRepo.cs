using ISas.Repository.Library.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Library.Repository
{
    public class Library_GenerateBarcodeRepo : ILibrary_GenerateBarcodeRepo
    {
        public List<string> GetAccNoList(string fromaccno, string toaccno)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<string> accNoList = new List<string>();

                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_CreateBarCode_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FromAccNo", fromaccno);
                cmd.Parameters.AddWithValue("@ToAccNo", toaccno);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    accNoList.Add(ds.Tables[0].Rows[i][0].ToString());

                return accNoList;
            }

        }
    }
}
