using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.Library.Repository
{
    public class Library_SetupRepo : ILibrary_SetupRepo
    {
        public List<Library_SetupModels> GetLibraryList(string LibraryId)
        {
            List<Library_SetupModels> libList = new List<Library_SetupModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_LibraryMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetLibraryList");
                cmd.Parameters.AddWithValue("@LibraryId", LibraryId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                libList = ds.Tables[0].AsEnumerable().Select(r => new Library_SetupModels
                {
                    BookStrength = r.Field<int>("BookStrength"),
                    FirstAccNo = r.Field<string>("FirstAccNo"),
                    LastAccNo = r.Field<string>("LastAccNo"),
                    LibCat = r.Field<string>("LibCat"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    AccLen = r.Field<int>("AccLen"),
                    LibId = r.Field<string>("LibId"),
                    LibName = r.Field<string>("LibName"),
                    LibPrefix = r.Field<string>("LibPrefix"),
                    StartAccNo = r.Field<string>("StartAccNo"),
                    UsePrefix = r.Field<bool>("UsePrefix"),

                }).ToList();
            }
            return libList;
        }
        public Library_SetupModels GetLibraryById(string LibraryId)
        {
            return GetLibraryList(LibraryId).FirstOrDefault();
        }

        public Tuple<int, string> Library_Setup_CRUD(Library_SetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_LibraryMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LibId", model.LibId);
                cmd.Parameters.AddWithValue("@LibName", model.LibName);
                cmd.Parameters.AddWithValue("@LibPrefix", model.LibPrefix);
                cmd.Parameters.AddWithValue("@StartAccNo", model.StartAccNo);
                cmd.Parameters.AddWithValue("@LibCat", model.LibCat);
                cmd.Parameters.AddWithValue("@AccLen", model.AccLen);
                cmd.Parameters.AddWithValue("@UsePrefix", model.UsePrefix);
                //cmd.Parameters.AddWithValue("@FirstAccNo", model.FirstAccNo);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Library_Setup_CRUD(string LibraryId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_LibraryMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LibId", LibraryId);
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
