using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ISas.Repository.Library.Repository
{
    public class Library_TransactionRepo : ILibrary_TransactionRepo
    {
        public Library_Transaction_BookDetailsModels GetBookDetails(string AccNo, string LibID)
        {
            Library_Transaction_BookDetailsModels bookDetails = new Library_Transaction_BookDetailsModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from [GetBookByAccNo]('" + AccNo + "','" + LibID + "')", con);
                cmd.CommandType = CommandType.Text;

                //cmd.Parameters.AddWithValue("@QueryFor", "GetAuthorList");
                //cmd.Parameters.AddWithValue("@AuthorId", AuthorId);
                //cmd.Parameters.AddWithValue("@AuthorType", AuthorType);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                bookDetails = ds.Tables[0].AsEnumerable().Select(r => new Library_Transaction_BookDetailsModels
                {
                    AccNo = r.Field<string>("AccNo"),
                    AuthorName = r.Field<string>("authname"),
                    BookName = r.Field<string>("booktitlename"),
                    //IssuedDays = r.Field<int>("Description"),
                    //TransDate = r.Field<string>("IsActive"),
                    //TransID = r.Field<string>("IsDeleteable"),
                    IssueStatus = r.Field<bool>("IssueStatus"),
                }).FirstOrDefault();
            }
            return bookDetails;
        }

        public Library_TransactionModels GetReturnBookDetails(string LibId, string ERPNo, string ReturnDate)
        {
            Library_TransactionModels model = new Library_TransactionModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_Transaction_ReturnBookList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LibraryId", LibId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);

                if (string.IsNullOrEmpty(ReturnDate))
                    cmd.Parameters.AddWithValue("@RetrunDate", Convert.ToDateTime(ReturnDate).Date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.BookReturnDetails = ds.Tables[0].AsEnumerable().Select(r => new Library_Transaction_BookDetailsModels
                {
                    AccNo = r.Field<string>("AccNo"),
                    BookName = r.Field<string>("BookTitleName"),
                    IssuedDays = r.Field<int>("Issuedays"),
                    //TransDate = r.Field<string>("IsActive"),
                    TransID = r.Field<int>("TransID"),
                    //IssueStatus = r.Field<bool>("IssueStatus"),
                    AuthorName = r.Field<string>("AuthName"),
                    DueDate = r.Field<string>("DueDate"),
                    Fine = r.Field<int>("Fine"),
                    IssueDate = r.Field<string>("IssueDate"),
                    OverDueDays = r.Field<int>("OverDueDays"),
                     
                }).ToList();
            }
            return model;
        }


        public Tuple<int, string> Library_Transaction_CRUD(Library_TransactionModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                string accNos = "";
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Library_Transaction_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@LibId", model.LibraryId);
                cmd.Parameters.AddWithValue("@TransactionType", model.TransactionType);
                cmd.Parameters.AddWithValue("@ERPNo", model.StudentDetails.StudentId);

                if (!string.IsNullOrEmpty(model.TransDate))
                    cmd.Parameters.AddWithValue("@TransDate", Convert.ToDateTime(model.TransDate).Date);


                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                if (model.TransactionType == "I")
                    accNos = string.Join(",", model.BookIssueDetails.BookDetails.Select(r => r.AccNo + "|" + r.IssuedDays).ToList());
                else
                    accNos = string.Join(",", model.BookReturnDetails.Where(r=> r.Selected).Select(r => r.AccNo + "|" + r.TransID).ToList());

                cmd.Parameters.AddWithValue("@AccNo", accNos);

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
