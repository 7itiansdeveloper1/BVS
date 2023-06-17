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
    public class Library_BookTitleMasterRepo : ILibrary_BookTitleMasterRepo
    {
        public List<Library_BookTitleMasterModels> GetBookTitleList(string BookTitleId, string TitleType)
        {
            List<Library_BookTitleMasterModels> titleList = new List<Library_BookTitleMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_BookTitleMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetTitleList");
                cmd.Parameters.AddWithValue("@BookTitleId", BookTitleId);
                cmd.Parameters.AddWithValue("@TitleType", TitleType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                titleList = ds.Tables[0].AsEnumerable().Select(r => new Library_BookTitleMasterModels
                {
                     BookStrength = r.Field<int>("BookStrength"),
                     BooktitleId = r.Field<string>("BooktitleId"),
                     BookTitleName = r.Field<string>("BookTitleName"),
                     Description = r.Field<string>("Description"),
                     IsActive = r.Field<bool>("IsActive"),
                     IsDeleteable = r.Field<bool>("IsDeleteable"),
                     TitleType = r.Field<string>("TitleType"),
                }).ToList();
            }
            return titleList;
        }
        public Library_BookTitleMasterModels GetBookTitleById(string BookTitleId, string TitleType)
        {
            return GetBookTitleList(BookTitleId, TitleType).FirstOrDefault();
        }

        public Tuple<int, string> Library_BookTitleMaster_CRUD(Library_BookTitleMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_BookTitleMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BooktitleId", model.BooktitleId);
                cmd.Parameters.AddWithValue("@BookTitleName", model.BookTitleName);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@TitleType", model.TitleType);
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
        public Tuple<int, string> Library_BookTitleMaster_CRUD(string BookTitleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_BookTitleMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BooktitleId", BookTitleId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string TitleId, string TitleType)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Library_Report_Title", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Mode", TitleType);
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
