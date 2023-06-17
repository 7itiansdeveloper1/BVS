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
    public class Library_AuthorMasterRepo : ILibrary_AuthorMasterRepo
    {
        public List<Library_AuthorMasterModels> GetAuthorList(string AuthorId, string AuthorType)
        {
            List<Library_AuthorMasterModels> authorList = new List<Library_AuthorMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_AuthorMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetAuthorList");
                cmd.Parameters.AddWithValue("@AuthorId", AuthorId);
                cmd.Parameters.AddWithValue("@AuthorType", AuthorType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                authorList = ds.Tables[0].AsEnumerable().Select(r => new Library_AuthorMasterModels
                {
                    AuthorType = r.Field<string>("AuthorType"),
                    AuthName = r.Field<string>("AuthName"),
                    AuthId = r.Field<string>("AuthId"),
                    Description = r.Field<string>("Description"),
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    TitleStrength = r.Field<int>("TitleStrength"),
                }).ToList();
            }
            return authorList;
        }
        public Library_AuthorMasterModels GetAuthorById(string AuthorId, string AuthorType)
        {
            return GetAuthorList(AuthorId, AuthorType).FirstOrDefault();
        }

        public Tuple<int, string> Library_AuthorMaster_CRUD(Library_AuthorMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_AuthorMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AuthId", model.AuthId);
                cmd.Parameters.AddWithValue("@AuthName", model.AuthName);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@AuthorType", model.AuthorType);

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Library_AuthorMaster_CRUD(string AuthorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_AuthorMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AuthId", AuthorId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Library_Author_IndividualReportModel Get_IndividualReport(string AuthorType, string AuthorId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Library_Author_IndividualReportModel model = new Library_Author_IndividualReportModel();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Library_Report_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", AuthorType);
                cmd.Parameters.AddWithValue("@AuthId", AuthorId);
                cmd.Parameters.AddWithValue("@TitleId", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.AuthorId = AuthorId; model.AuthorType = AuthorType;
                if (ds.Tables[1].Rows.Count > 0)
                    model.ReportTitle = ds.Tables[1].Rows[0][0].ToString();

                model.BookDetails = ds.Tables[2].AsEnumerable().Select(r => new Library_AuthIndividualReportDetailModel
                {
                    NoofBooks = r.Field<int>("NoofBooks"),
                    Sno = r.Field<long>("Sno"),
                    Title = r.Field<string>("Title"),
                    TitleId = r.Field<string>("TitleId"),
                }).ToList();
                return model;
            }

        }
        public Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string AuthorId, string AuthorType, string TitleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Library_Report_Author", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", AuthorType);
                cmd.Parameters.AddWithValue("@AuthId", AuthorId);
                cmd.Parameters.AddWithValue("@TitleId", TitleId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                string bookTitle = "";

                if (ds.Tables[1].Rows.Count > 0)
                    bookTitle = ds.Tables[1].Rows[0][0].ToString();

                List<Library_Author_BookTitleWiseReportModel> bookDetails = ds.Tables[2].AsEnumerable().Select(r => new Library_Author_BookTitleWiseReportModel
                {
                    AccNo = r.Field<string>("AccNo"),
                    AlmirahNo = r.Field<string>("AlmirahNo"),
                    BookCall = r.Field<string>("BookCall"),
                    ShelfNo = r.Field<string>("ShelfNo"),
                    Sno = r.Field<long>("Sno"),
                    Title = r.Field<string>("Title"),
                }).ToList();
                return new Tuple<List<Library_Author_BookTitleWiseReportModel>, string>(bookDetails, bookTitle);
            }
        }
    }
}
