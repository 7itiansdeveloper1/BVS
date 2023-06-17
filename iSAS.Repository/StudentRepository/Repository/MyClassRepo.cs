using ISas.Entities.Student_Entities;
using ISas.Repository.StudentRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.StudentRepository.Repository
{
    public class MyClassRepo: IMyClassRepo
    {
        public MyClassModels MyClass_Transaction(string userid,string sessionid,string status)
        {
            MyClassModels model = new MyClassModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_MyClass_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserReferenceNo", userid);
                cmd.Parameters.AddWithValue("@SessionId", sessionid);
                cmd.Parameters.AddWithValue("@Status", status);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                
                model.myClassList = ds.Tables[0].AsEnumerable().Select(r => new MyClass
                {
                    ERP = r.Field<string>("ERP"),
                    AdmNo = r.Field<string>("AdmNo"),
                    DOA = r.Field<string>("DOA"),
                    DOB = r.Field<string>("DOB"),
                    RollNo = r.Field<int>("RollNo"),
                    Student = r.Field<string>("Student"),
                    Gender = r.Field<string>("Gender"),
                    Father = r.Field<string>("Father"),
                    Mother = r.Field<string>("Mother"),
                    ContactNo = r.Field<string>("ContactNo"),
                }).ToList();
                }
            }
            return model;
        }

        public Tuple<int, string> UserCreation_CRUD(string userReferenceNo, string mode)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UserCreation_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserReferenceNo", userReferenceNo);
                cmd.Parameters.AddWithValue("@Mode", mode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
