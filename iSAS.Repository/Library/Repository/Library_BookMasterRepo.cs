using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Library.Repository
{
    public class Library_BookMasterRepo : ILibrary_BookMasterRepo
    {
        public Library_BookMasterModels GetBookById(string LibId, string AccNo, string BtnClick)
        {
            Library_BookMasterModels model = new Library_BookMasterModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_Bookmaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetBook");
                cmd.Parameters.AddWithValue("@LibId", LibId);
                cmd.Parameters.AddWithValue("@AccNo", AccNo);
                cmd.Parameters.AddWithValue("@BtnClick", BtnClick);
                

                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[0].AsEnumerable().Select(r => new Library_BookMasterModels
                {
                    IssueStatus = r.Field<bool>("IssueStatus"),
                    TitleID = r.Field<string>("TitleID"),
                    ClassID = r.Field<string>("ClassID"),
                    IsActive = r.Field<bool>("IsActive"),
                    WDRemark = r.Field<string>("WDRemark"),
                    WDDate = r.Field<string>("WDDate"),
                    AccNo = r.Field<string>("AccNo"),
                    AlmirahNo = r.Field<string>("AlmirahNo"),
                    AuthID = r.Field<string>("AuthID"),
                    BillDate = r.Field<string>("BillDate"),
                    BillNo = r.Field<string>("BillNo"),
                    BookCall = r.Field<string>("BookCall"),
                    BookDate = r.Field<string>("BookDate"),
                    BookID = r.Field<string>("BookID"),
                    BookLocation = r.Field<string>("BookLocation"),
                    BookType = r.Field<string>("BookType"),
                    ClassCall = r.Field<string>("ClassCall"),
                    Disc = r.Field<decimal>("Disc"),
                    Edition = r.Field<string>("Edition"),
                    ISBN = r.Field<string>("ISBN"),
                    Lang = r.Field<string>("Lang"),
                    LibID = r.Field<string>("LibID"),
                    NetPrice = r.Field<decimal>("NetPrice"),
                    Pages = r.Field<string>("Pages"),
                    Price = r.Field<decimal>("Price"),
                    PubID = r.Field<string>("PubID"),
                    Recfrom = r.Field<string>("Recfrom"),
                    Reference = r.Field<string>("Reference"),
                    Remark = r.Field<string>("Remark"),
                    ShelfNo = r.Field<string>("ShelfNo"),
                    SubAuthID = r.Field<string>("SubAuthID"),
                    SubID = r.Field<string>("SubID"),
                    SubTitleID = r.Field<string>("SubTitleID"),
                    SupID = r.Field<string>("SupID"),
                    Vol = r.Field<string>("Vol"),
                    WDNo = r.Field<string>("WDNo"),
                    Year = r.Field<string>("Year"),
                }).FirstOrDefault();
            }
            return model;
        }

        public Library_BookMasterModels GetFormLoadDetails()
        {
            Library_BookMasterModels model = new Library_BookMasterModels();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_Bookmaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetFormLoadLists");
                cmd.Parameters.AddWithValue("@LibId", "");
                cmd.Parameters.AddWithValue("@AccNo", "");
                cmd.Parameters.AddWithValue("@BtnClick", "");
                

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.LibraryList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("LibName"),
                    Value = r.Field<string>("LibId"),
                }).ToList();
                model.ClassList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID"),
                }).ToList();
                model.SubjectList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SubjectName"),
                    Value = r.Field<string>("SubjectId"),
                }).ToList();
                model.SupplierList = ds.Tables[3].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SupplierName"),
                    Value = r.Field<string>("SupplierId"),
                }).ToList();
                model.PublisherList = ds.Tables[4].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("PublisherName"),
                    Value = r.Field<string>("PublisherId"),
                }).ToList();
                model.AuthorList = ds.Tables[5].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("AuthName"),
                    Value = r.Field<string>("AuthId"),
                }).ToList();
                model.CoAuthorList = ds.Tables[6].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("AuthName"),
                    Value = r.Field<string>("AuthId"),
                }).ToList();
                model.TitleList = ds.Tables[7].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("BookTitleName"),
                    Value = r.Field<string>("BookTitleId"),
                }).ToList();
                model.SubTitleList = ds.Tables[8].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("BookTitleName"),
                    Value = r.Field<string>("BookTitleId"),
                }).ToList();

                model.AccNo = ds.Tables[9].Rows[0][0].ToString();
            }
            return model;
        }

        public Tuple<int, string> Library_BookMaster_CRUD(Library_BookMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_BookMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookEntry", model.BookEntry);
                cmd.Parameters.AddWithValue("@TotalBook", model.TotalBook);
                cmd.Parameters.AddWithValue("@RefBook", model.RefBook);
                cmd.Parameters.AddWithValue("@TextBook", model.TextBook);
                cmd.Parameters.AddWithValue("@BookID", model.BookID);
                cmd.Parameters.AddWithValue("@LibID", model.LibID);

                if (!string.IsNullOrEmpty(model.BookDate))
                    cmd.Parameters.AddWithValue("@BookDate", Convert.ToDateTime(model.BookDate).Date);
                cmd.Parameters.AddWithValue("@AccNo", model.AccNo);
                cmd.Parameters.AddWithValue("@TitleName", model.TitleID);
                cmd.Parameters.AddWithValue("@SubTitleName", model.SubTitleID);
                cmd.Parameters.AddWithValue("@SubID", model.SubID);
                cmd.Parameters.AddWithValue("@AuthName", model.AuthID);
                cmd.Parameters.AddWithValue("@SubAuthName", model.SubAuthID);
                cmd.Parameters.AddWithValue("@Edition", model.Edition);
                cmd.Parameters.AddWithValue("@Vol", model.Vol);
                cmd.Parameters.AddWithValue("@Year", model.Year);
                cmd.Parameters.AddWithValue("@Pages", model.Pages);
                cmd.Parameters.AddWithValue("@ISBN", model.ISBN);
                cmd.Parameters.AddWithValue("@ClassCall", model.ClassCall);
                cmd.Parameters.AddWithValue("@BookCall", model.BookCall);
                cmd.Parameters.AddWithValue("@ClassID", model.ClassID);
                cmd.Parameters.AddWithValue("@Lang", model.Lang);
                cmd.Parameters.AddWithValue("@BookType", model.BookType);
                cmd.Parameters.AddWithValue("@Reference", model.Reference);
                cmd.Parameters.AddWithValue("@Remark", model.Remark);
                cmd.Parameters.AddWithValue("@BillNo", model.BillNo);

                if (!string.IsNullOrEmpty(model.BillDate))
                    cmd.Parameters.AddWithValue("@BillDate", Convert.ToDateTime(model.BillDate).Date);
                cmd.Parameters.AddWithValue("@Recfrom", model.Recfrom);
                cmd.Parameters.AddWithValue("@PubName", model.PubID);
                cmd.Parameters.AddWithValue("@SupID", model.SupID);
                cmd.Parameters.AddWithValue("@Price", model.Price);
                cmd.Parameters.AddWithValue("@Disc", model.Disc);
                cmd.Parameters.AddWithValue("@NetPrice", model.NetPrice);
                cmd.Parameters.AddWithValue("@BookLocation", model.BookLocation);
                cmd.Parameters.AddWithValue("@AlmirahNo", model.AlmirahNo);
                cmd.Parameters.AddWithValue("@ShelfNo", model.ShelfNo);
                cmd.Parameters.AddWithValue("@WDNo", model.WDNo);

                if (!string.IsNullOrEmpty(model.WDDate))
                    cmd.Parameters.AddWithValue("@WDDate", Convert.ToDateTime(model.WDDate).Date);
                cmd.Parameters.AddWithValue("@WDRemark", model.WDRemark);
                cmd.Parameters.AddWithValue("@IssueStatus", model.IssueStatus);
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
    }
}
