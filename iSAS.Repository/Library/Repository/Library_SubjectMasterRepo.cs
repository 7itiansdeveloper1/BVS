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
    public class Library_SubjectMasterRepo : ILibrary_SubjectMasterRepo
    {
        public List<Library_SubjectMasterModels> GetSubjectList(string SubjectId)
        {
            List<Library_SubjectMasterModels> subjectList = new List<Library_SubjectMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_SubjectMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetSubjectList");
                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                subjectList = ds.Tables[0].AsEnumerable().Select(r => new Library_SubjectMasterModels
                {
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    Description = r.Field<string>("Description"),
                    SubjectId = r.Field<string>("SubjectId"),
                    SubjectName = r.Field<string>("SubjectName"),
                    SubjectStrength = r.Field<int>("SubjectStrength"),
                     
                }).ToList();
            }
            return subjectList;
        }
        public Library_SubjectMasterModels GetSubjectById(string SubjectId)
        {
            return GetSubjectList(SubjectId).FirstOrDefault();
        }

        public Tuple<int, string> Library_SubjectMaster_CRUD(Library_SubjectMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_SubjectMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectId", model.SubjectId);
                cmd.Parameters.AddWithValue("@SubjectName", model.SubjectName);
                cmd.Parameters.AddWithValue("@Description", model.Description);
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
        public Tuple<int, string> Library_SubjectMaster_CRUD(string SubjectId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_SubjectMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubjectId", SubjectId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string SubjectId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Library_Report_Subject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SubId", SubjectId);

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
