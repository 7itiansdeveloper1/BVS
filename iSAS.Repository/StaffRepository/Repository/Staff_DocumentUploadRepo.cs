using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.StaffEntities;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ISas.Repository.StaffRepository.IRepository;
using System.Web.Mvc;

namespace ISas.Repository.StaffRepository.Repository
{
    public class Staff_DocumentUploadRepo : IStaff_DocumentUploadRepo
    {

        public Staff_DocumentUploadModels Staff_DocumentUpload_Transaction(string staffId, string staffName)
        {
            Staff_DocumentUploadModels model = new Staff_DocumentUploadModels();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Staff_DocumentUpload_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                model.staffId = staffId;
                //model.StaffName = staffName;
                if(ds.Tables[0].Rows.Count>0)
                {
                    model.documentList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("DocName"),
                        Value = r.Field<string>("DocNo")
                }).ToList();
                }

                model.staffDocumentList = ds.Tables[1].AsEnumerable().Select(r => new Staff_DocumentList
                {
                    staffId = r.Field<string>("staffId"),
                    DocId = r.Field<string>("DocId"),
                    DocName = r.Field<string>("DocName"),
                    docAlias = r.Field<string>("docAlias"),
                    DocPath = r.Field<string>("DocPath"),
                    UploadedBy = r.Field<string>("UploadedBy"),
                    UplodedDate = r.Field<string>("UplodedDate"),
                    docfileName = r.Field<string>("docfileName"),
                    docNo = r.Field<int>("docNo"),
                    certificateDate = r.Field<string>("certificateDate"),
                    TrainBy = r.Field<string>("TrainBy"),
                    TrainByName = r.Field<string>("TrainByName")

                }
                ).ToList();
                
            }
            return model;
        }
        //public List<StaffDocumentList> Staff_DocumentUpload_Transaction(string staffId)
        //{
        //    List<StaffDocumentList> staffdocList = new List<StaffDocumentList>();
        //    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_Staff_DocumentUpload_Transaction", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@StaffId", staffId);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);
        //        staffdocList = dt.AsEnumerable().Select(r => new StaffDocumentList
        //        {
        //            DocId = r.Field<string>("DocId"),
        //            DocName = r.Field<string>("DocName"),
        //            docAlias = r.Field<string>("docAlias"),
        //            DocPath = r.Field<string>("DocPath")
        //        }).ToList();
        //    }
        //    return staffdocList;
        //}

        public Tuple<int, string> StaffDocumentUpload_CRUD(string staffId, string docId, string docPath,string userId,string docAlias,string filename,string certificationDate,string trainedby)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_DocumentUpload_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                cmd.Parameters.AddWithValue("@DocId", docId);
                cmd.Parameters.AddWithValue("@docAlias", docAlias);
                cmd.Parameters.AddWithValue("@ImageURL", docPath);
                cmd.Parameters.AddWithValue("@fileName", filename);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@certificateDate",Convert.ToDateTime(certificationDate).Date);
                cmd.Parameters.AddWithValue("@TrainBy", trainedby);
                cmd.Parameters.AddWithValue("@mode", "SAVE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }
        public Tuple<int, string> StaffDocumentUpload_DELETE(int docNo)
        {
            DataTable dt1 = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_Staff_DocumentUpload_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DocNo", docNo);
                cmd.Parameters.AddWithValue("@mode", "DELETE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt1);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt1.Rows[0][0].ToString()), dt1.Rows[0][1].ToString());
        }

    }
}
