using ISas.Entities.Student_Entities;
using ISas.Repository.StudentRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.StudentRepository.Repository
{
    public class Student_IdentityCardRepo : IStudent_IdentityCardRepo
    {
        public List<IdentityCard_StudentDetailsModel> Get_IdentityCard_StudentDetails(string ClassSecId, string ReportName, string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Student_ICardReports", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassSectionId", ClassSecId);
                cmd.Parameters.AddWithValue("@ReportName", ReportName);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[2].AsEnumerable().Select(r => new IdentityCard_StudentDetailsModel
                {
                    BG = r.Field<string>("BG"),
                    DOA = r.Field<string>("DOA"),
                    AdmNo = r.Field<string>("AdmNo"),
                    ERPNo = r.Field<string>("ERPNo"),
                    Student = r.Field<string>("Student"),
                    Class = r.Field<string>("Class"),
                    Gender = r.Field<string>("Gender"),
                    DOB = r.Field<string>("DOB"),
                    Father = r.Field<string>("Father"),
                    Mother = r.Field<string>("Mother"),
                    Address = r.Field<string>("Address"),
                    F = r.Field<string>("F"),
                    FMobile = r.Field<string>("FMobileNo"),
                    M = r.Field<string>("M"),
                    MMobile = r.Field<string>("MMobileNo"),
                    S = r.Field<string>("S"),
                    Route = r.Field<string>("Route"),
                    Session = r.Field<string>("Session"),
                    sno = r.Field<long>("sno"),
                    RollNo = r.Field<string>("RollNo"),
                    Stop = r.Field<string>("Stop"),
                }).ToList();
            }
        }
    }
}
