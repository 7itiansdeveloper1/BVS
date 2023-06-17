using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Library.Repository
{
    public class eLibraryRepo : IeLibraryRepo
    {
        public eLibraryModels eLibrary_Transaction(string classids, int bookno,string function)
        {
            eLibraryModels model = new eLibraryModels();
             if (!string.IsNullOrEmpty(classids))
                model.classIds = (classids.Split(','));

            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("eLibrary_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classIds", classids);
                cmd.Parameters.AddWithValue("@eBookNo", bookno);
                cmd.Parameters.AddWithValue("@function", function);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                model.eLibraryBooks = ds.Tables[0].AsEnumerable().Select(r => new eLibraryBook
                {
                    eBookNo = r.Field<int>("eBookNo"),
                    eBookName = r.Field<string>("eBookName"),
                    eBookSubjectName = r.Field<string>("eBookSubjectName"),
                    eBookClassName = r.Field<string>("eBookClassName"),
                    eBookattachementName = r.Field<string>("eBookattachementName"),
                    eBookattachementPath = r.Field<string>("eBookattachementPath"),
                    totalDownloads = r.Field<int>("totalDownloads"),
                    isActive = r.Field<bool>("isActive"),
                }).ToList();
                model.classList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID")
                }).ToList();

                model.subjectList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId")
                }).ToList();
                eLibraryBook book = new eLibraryBook();
                model.eLibraryBook = book;
                model.Function = function;
            }
            return model;
        }
        public List<eLibraryBook> eLibrary_Student(string userid)
        {
            List<eLibraryBook> eLibraryBooks = new List<eLibraryBook>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("eLibrary_Student", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                if(dt.Rows.Count>0)
                {
                    eLibraryBooks = dt.AsEnumerable().Select(r => new eLibraryBook
                    {
                        eBookNo = r.Field<int>("eBookNo"),
                        eBookName = r.Field<string>("eBookName"),
                        eBookSubjectName = r.Field<string>("eBookSubjectName"),
                        eBookType = r.Field<string>("eBookType"),
                        eBookattachementPath = r.Field<string>("eBookattachementPath"),
                        eBookattachementName = r.Field<string>("eBookattachementName")
                    }).ToList();
                }
                
                
            }
            return eLibraryBooks;
        }
        public eLibraryModels eLibrary_Transaction(int bookno,string function)
        {
            eLibraryModels model = new eLibraryModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("eLibrary_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classIds", null);
                cmd.Parameters.AddWithValue("@eBookNo", bookno);
                cmd.Parameters.AddWithValue("@function", function);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                eLibraryBook book = new eLibraryBook();
                if(ds.Tables[0].Rows.Count>0)
                {
                    book.eBookNo = Convert.ToInt32(ds.Tables[0].Rows[0]["eBookNo"]);
                    book.eBookName = ds.Tables[0].Rows[0]["eBookName"].ToString();
                    book.eBookType = ds.Tables[0].Rows[0]["eBookType"].ToString();
                    book.eBookSubjectId = ds.Tables[0].Rows[0]["eBookSubjectId"].ToString();
                    book.eBookClassId = ds.Tables[0].Rows[0]["eBookClassId"].ToString().Split(',');
                    book.eBookattachementName = ds.Tables[0].Rows[0]["eBookattachementName"].ToString();
                    book.eBookattachementPath = ds.Tables[0].Rows[0]["eBookattachementPath"].ToString();
                    book.isActive = Convert.ToBoolean(ds.Tables[0].Rows[0]["isActive"]);
                    book.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                model.eLibraryBook = book;
                model.classList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID")
                }).ToList();

                model.subjectList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId")
                }).ToList();
                model.Function = "UPDATE";
            }
            return model;
        }


        public Tuple<int, string> eLibrary_CRUD(eLibraryBook model,string function,string userid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("eLibrary_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eBookNo", model.eBookNo);
                cmd.Parameters.AddWithValue("@eBookName", model.eBookName);
                cmd.Parameters.AddWithValue("@eBookType", model.eBookType);
                cmd.Parameters.AddWithValue("@eBookSubjectId", model.eBookSubjectId);
                if(model.eBookClassId==null)
                    cmd.Parameters.AddWithValue("@eBookClassId", "");
                else
                cmd.Parameters.AddWithValue("@eBookClassId", string.Join(",",model.eBookClassId));
                cmd.Parameters.AddWithValue("@eBookattachementName", model.eBookattachementName);
                cmd.Parameters.AddWithValue("@eBookattachementPath", model.eBookattachementPath);
                cmd.Parameters.AddWithValue("@isActive", model.isActive);
                cmd.Parameters.AddWithValue("@remark", model.remark);
                cmd.Parameters.AddWithValue("@userId", userid);
                cmd.Parameters.AddWithValue("@function", function);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }

        public Tuple<int, string> Download_Count(int ebookno, string userid)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("eBook_Download", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eBookNo", ebookno);
                cmd.Parameters.AddWithValue("@userId", userid);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
