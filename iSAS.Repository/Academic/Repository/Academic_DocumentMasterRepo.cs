using System;
using System.Collections.Generic;
using System.Linq;
using ISas.Entities.Academic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ISas.Repository.Academic.IRepository;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_DocumentMasterRepo:IAcademic_DocumentMasterRepo
    {

        public List<Academic_DocumentMasterModel> GetDocumentList(string docno)
        {
            List<Academic_DocumentMasterModel> documentList = new List<Academic_DocumentMasterModel>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_DocumentMaster_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocNo", docno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                documentList = ds.Tables[0].AsEnumerable().Select(r => new Academic_DocumentMasterModel
                {
                    DocNo = r.Field<string>("DocNo"),
                    DocName = r.Field<string>("DocName"),
                    PrintOrder= r.Field<int>("PrintOrder"),
                    Active = r.Field<bool>("Active"),
                    DocForStaff = r.Field<bool>("DocForStaff"),
                    DocForStudent = r.Field<bool>("DocForStudent"),
                    IsDeletable = r.Field<bool>("IsDeletable")
                }).ToList();
            }
            return documentList;
        }
        public Academic_DocumentMasterModel GetDocumentByDocNo(string docno)
        {
            return GetDocumentList(docno).FirstOrDefault();
        }
        public Tuple<int, string> Academic_DocumentMaster_CRUD(Academic_DocumentMasterModel model )
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_DocumentMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocNo", model.DocNo);
                cmd.Parameters.AddWithValue("@DocName", model.DocName);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Active", model.Active);
                cmd.Parameters.AddWithValue("@DocForStaff", model.DocForStaff);
                cmd.Parameters.AddWithValue("@DocForStudent", model.DocForStudent);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Academic_DocumentMaster_CRUD(string  docNo,string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_DocumentMaster_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocNo", docNo);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        //public Tuple<int, string> Academic_BankMaster_CRUD(string BankId)
        //{
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("sp_Staff_BankMaster_CRUD", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@BankID", BankId);
        //        cmd.Parameters.AddWithValue("@BankName", "");
        //        cmd.Parameters.AddWithValue("@UserId", "");
        //        cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        //cmd.ExecuteNonQuery();
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        con.Close();
        //        return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
        //    }
        //}
    }
}
