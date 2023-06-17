using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.Repository
{
    public class Fee_InvoiceCreationRepo : IFee_InvoiceCreationRepo
    {
        public List<SelectListItem> GetFeeCategoryList()
        {
            List<SelectListItem> feeCategoryList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_CreateInvoice_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetFeeCategoryList");
                cmd.Parameters.AddWithValue("@ClassId", "");
                cmd.Parameters.AddWithValue("@SectionId", "");
                cmd.Parameters.AddWithValue("@SessionId", "");
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                feeCategoryList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StructName"),
                    Value = r.Field<string>("StructId"),
                }).ToList();
            }
            return feeCategoryList;
        }
        public List<InvoiceDetails> GetInvoiceDetailsList(string ClassId, string SectionId, string SessionId)
        {
            List<InvoiceDetails> _invoiceDetails = new List<InvoiceDetails>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_CreateInvoice_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetStudentInvoiceList");
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SessionId", SessionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                _invoiceDetails = ds.Tables[0].AsEnumerable().Select(r => new InvoiceDetails
                {
                    AdmNo = r.Field<string>("AdmNo"),
                    ERPNo = r.Field<string>("ERPNo"),
                    FeeCategory = r.Field<string>("FeeStructure"),
                    IsCreateInvoice = r.Field<bool>("IsCreateInvoice"),
                    StudentName = r.Field<string>("Student"),
                }).ToList();
            }
            return _invoiceDetails;
        }

        public List<FeeStructureDetails> GetFeeStrectureDetailsList(string ClassId, string SectionId, string StructId,string sessionId)
        {
            List<FeeStructureDetails> _invoiceDetails = new List<FeeStructureDetails>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_CreateInvoice_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetStructureDetail");
                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                cmd.Parameters.AddWithValue("@StructId", StructId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                _invoiceDetails = ds.Tables[0].AsEnumerable().Select(r => new FeeStructureDetails
                {
                    Due = r.Field<int>("Amount"),
                    Head = r.Field<string>("Head"),
                    Installment = r.Field<string>("InstallName"),
                    DueDate = r.Field<string>("DueDate"),

                }).ToList();
            }
            return _invoiceDetails;
        }


        public Tuple<int, string> Fee_InvoiceCreation_CRUD(string SessionId, string ERPNo, string StructId,string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_CreateStudentInvoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SessionId", SessionId);
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@StructId", StructId);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandTimeout = 0;
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        #region Calcel Invoice
        public List<CancelInvoice_InvoiceDetailsModel> CancelInvoice_InvoiceList(string ERPNo,string sessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_EditInvoice_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new CancelInvoice_InvoiceDetailsModel
                {
                    Amount = r.Field<int>("Amount"),
                    Duration = r.Field<string>("Duration"),
                    ERPNo = r.Field<string>("ERPNo"),
                    InvoiceStatus = r.Field<string>("InvoiceStatus"),
                    IsCancelable = r.Field<bool>("IsCancelable"),
                    TransRefNo = r.Field<string>("TransRefNo"),
                    PaidAmount = r.Field<int>("PaidAmount"),
                }).ToList();
            }
        }
        public List<SelectListItem> ConcessionCategoryList(string ERPNo)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_EditInvoice_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ConcCategoryame"),
                    Value = r.Field<string>("ConcatId"),
                }).ToList();
            }
        }
        public Tuple<int, string> CancelInvoice_CRUD(string ERPNo, string InvoiceNo, string CancelCategory,string UserId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Fee_EditInvoice_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ERPNo", ERPNo);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                cmd.Parameters.AddWithValue("@CancelCategory", CancelCategory);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion

    }
}
