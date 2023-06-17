using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_SchoolSetupRepo : IAcademic_SchoolSetupRepo
    {
        public List<Academic_SchoolSetupModels> GetSchoolList(int SchoolId)
        {
            List<Academic_SchoolSetupModels> schoolList = new List<Academic_SchoolSetupModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SchoolSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetSchoolList");
                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                schoolList = ds.Tables[0].AsEnumerable().Select(r => new Academic_SchoolSetupModels
                {
                    Add = r.Field<string>("Add"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                    AffiliationBoard = r.Field<string>("AffiliationBoard"),
                    AffiliationCode = r.Field<string>("AffiliationCode"),
                    AffiliationYear = r.Field<string>("AffiliationYear"),
                    Alias = r.Field<string>("Alias"),
                    City = r.Field<string>("City"),
                    ClientName = r.Field<string>("ClientName"),
                    Code = r.Field<string>("Code"),
                    Country = r.Field<string>("Country"),
                    Email = r.Field<string>("Email"),
                    Fax = r.Field<string>("Fax"),
                    Header1 = r.Field<string>("Header1"),
                    Header2 = r.Field<string>("Header2"),
                    Header3 = r.Field<string>("Header3"),
                    Logo = r.Field<string>("Logo"),
                    Pincode = r.Field<string>("Pincode"),
                    SchoolId = r.Field<int>("SchoolId"),
                    State = r.Field<string>("State"),
                    Tel = r.Field<string>("Tel"),
                    Web = r.Field<string>("Web"),

                }).ToList();
            }
            return schoolList;
        }
        public Academic_SchoolSetupModels GetSchoolDetailsById(int SchoolId)
        {
            return GetSchoolList(SchoolId).FirstOrDefault();
        }

        public Tuple<int, string> Academic_SchoolSetup_CRUD(Academic_SchoolSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SchoolSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", model.SchoolId);
                cmd.Parameters.AddWithValue("@ClientName", model.ClientName);
                cmd.Parameters.AddWithValue("@Alias", model.Alias);
                cmd.Parameters.AddWithValue("@Code", model.Code);
                cmd.Parameters.AddWithValue("@Add", model.Add);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                cmd.Parameters.AddWithValue("@State", model.State);
                cmd.Parameters.AddWithValue("@Country", model.Country);
                cmd.Parameters.AddWithValue("@Tel", model.Tel);
                cmd.Parameters.AddWithValue("@Fax", model.Fax);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Web", model.Web);
                cmd.Parameters.AddWithValue("@Header1", model.Header1);
                cmd.Parameters.AddWithValue("@Header2", model.Header2);
                cmd.Parameters.AddWithValue("@Header3", model.Header3);
                cmd.Parameters.AddWithValue("@AffiliationCode", model.AffiliationCode);
                cmd.Parameters.AddWithValue("@AffiliationBoard", model.AffiliationBoard);
                cmd.Parameters.AddWithValue("@AffiliationYear", model.AffiliationYear);
                cmd.Parameters.AddWithValue("@Logo", model.Logo);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Academic_SchoolSetup_CRUD(int SchoolId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_SchoolSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SchoolId", SchoolId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");
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
