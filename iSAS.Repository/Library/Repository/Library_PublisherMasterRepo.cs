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
    public class Library_PublisherMasterRepo : ILibrary_PublisherMasterRepo
    {
        public List<Library_PublisherMasterModels> GetPublisherList(string PublisherId)
        {
            List<Library_PublisherMasterModels> publisherList = new List<Library_PublisherMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_PublisherMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetPublisherList");
                cmd.Parameters.AddWithValue("@PublisherId", PublisherId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                publisherList = ds.Tables[0].AsEnumerable().Select(r => new Library_PublisherMasterModels
                {
                    PublishStrength = r.Field<int>("PublishStrength"),
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    PublisherAdd = r.Field<string>("PublisherAdd"),
                    PublisherCity = r.Field<string>("PublisherCity"),
                    PublisherContact = r.Field<string>("PublisherContact"),
                    PublisherCountry = r.Field<string>("PublisherCountry"),
                    PublisherId = r.Field<string>("PublisherId"),
                    PublisherName = r.Field<string>("PublisherName"),
                    PublisherState = r.Field<string>("PublisherState"),
                }).ToList();
            }
            return publisherList;
        }
        public Library_PublisherMasterModels GetPublisherById(string PublisherId)
        {
            return GetPublisherList(PublisherId).FirstOrDefault();
        }

        public Tuple<int, string> Library_PublisherMaster_CRUD(Library_PublisherMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_PublisherMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PublisherId", model.PublisherId);
                cmd.Parameters.AddWithValue("@PublisherName", model.PublisherName);
                cmd.Parameters.AddWithValue("@PublisherAdd", model.PublisherAdd);
                cmd.Parameters.AddWithValue("@PublisherCity", model.PublisherCity);
                cmd.Parameters.AddWithValue("@PublisherState", model.PublisherState);
                cmd.Parameters.AddWithValue("@PublisherCountry", model.PublisherCountry);
                cmd.Parameters.AddWithValue("@PublisherContact", model.PublisherContact);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
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
        public Tuple<int, string> Library_PublisherMaster_CRUD(string PublisherId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_PublisherMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PublisherId", PublisherId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string PublisherId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Library_Report_Publisher", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PubId", PublisherId);

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
