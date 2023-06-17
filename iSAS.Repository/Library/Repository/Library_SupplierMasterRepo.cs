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
    public class Library_SupplierMasterRepo : ILibrary_SupplierMasterRepo
    {
        public List<Library_SupplierMasterModels> GetSupplierList(string SupplierId)
        {
            List<Library_SupplierMasterModels> supplierList = new List<Library_SupplierMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_SupplierMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetSupplierList");
                cmd.Parameters.AddWithValue("@SupplierId", SupplierId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                supplierList = ds.Tables[0].AsEnumerable().Select(r => new Library_SupplierMasterModels
                {
                    PublishStrength = r.Field<int>("PublishStrength"),
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    SupplierAdd = r.Field<string>("SupplierAdd"),
                    SupplierCity = r.Field<string>("SupplierCity"),
                    SupplierContact = r.Field<string>("SupplierContact"),
                    SupplierCountry = r.Field<string>("SupplierCountry"),
                    SupplierId = r.Field<string>("SupplierId"),
                    SupplierName = r.Field<string>("SupplierName"),
                    SupplierState = r.Field<string>("SupplierState"),
                }).ToList();
            }
            return supplierList;
        }
        public Library_SupplierMasterModels GetSupplierById(string SupplierId)
        {
            return GetSupplierList(SupplierId).FirstOrDefault();
        }

        public Tuple<int, string> Library_SupplierMaster_CRUD(Library_SupplierMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_SupplierMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", model.SupplierId);
                cmd.Parameters.AddWithValue("@SupplierName", model.SupplierName);
                cmd.Parameters.AddWithValue("@SupplierAdd", model.SupplierAdd);
                cmd.Parameters.AddWithValue("@SupplierCity", model.SupplierCity);
                cmd.Parameters.AddWithValue("@SupplierState", model.SupplierState);
                cmd.Parameters.AddWithValue("@SupplierCountry", model.SupplierCountry);
                cmd.Parameters.AddWithValue("@SupplierContact", model.SupplierContact);
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
        public Tuple<int, string> Library_SupplierMaster_CRUD(string SupplierId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Lib_SupplierMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public Tuple<List<Library_Author_BookTitleWiseReportModel>, string> Get_BookTitleWiseReport(string SupplierId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Library_Report_Supplier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupId", SupplierId);

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
