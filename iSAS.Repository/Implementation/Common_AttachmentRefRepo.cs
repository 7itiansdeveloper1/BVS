using ISas.Entities.CommonEntities;
using ISas.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Implementation
{
    public class Common_AttachmentRefRepo : ICommon_AttachmentRefRepo
    {
        public List<Common_AttachemntRefrence> Common_AttachmentRef_List(string refId, string refId1, string filterBy)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_common_attachmentRef_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@refValue", refId);
                cmd.Parameters.AddWithValue("@refValue1", refId1);
                cmd.Parameters.AddWithValue("@filterBy", filterBy);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Common_AttachemntRefrence
                {
                    docType = r.Field<string>("docType"),
                    fileName = r.Field<string>("fileName"),
                    filePath = r.Field<string>("filePath"),
                    formName = r.Field<string>("formName"),
                    refrenceId = r.Field<string>("refrenceId"),
                    remarks = r.Field<string>("remarks"),
                    transKey = r.Field<string>("transKey"),
                    uniqueKey = r.Field<string>("uniqueKey"),
                    updatedBy = r.Field<string>("updatedBy"),
                    updatedDate = r.Field<string>("updatedDate"),
                }).ToList();
            }
        }

        public List<Common_AttachemntRefrence> Common_AttachmentRef_Stud_List(string refId, string refId1, string filterBy)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_common_attachmentRef_Stud_List", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@refValue", refId);
                cmd.Parameters.AddWithValue("@refValue1", refId1);
                cmd.Parameters.AddWithValue("@filterBy", filterBy);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Common_AttachemntRefrence
                {
                    docType = r.Field<string>("docType"),
                    fileName = r.Field<string>("fileName"),
                    filePath = r.Field<string>("filePath"),
                    formName = r.Field<string>("formName"),
                    refrenceId = r.Field<string>("refrenceId"),
                    remarks = r.Field<string>("remarks"),
                    transKey = r.Field<string>("transKey"),
                    uniqueKey = r.Field<string>("uniqueKey"),
                    updatedBy = r.Field<string>("updatedBy"),
                    updatedDate = r.Field<string>("updatedDate"),
                }).ToList();
            }
        }

        public Tuple<int, string> Common_AttachmentRef_ADD(Common_AttachemntRefrence model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_common_attachmentRef_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@uniqueKey", model.uniqueKey);
                cmd.Parameters.AddWithValue("@refrenceId", model.refrenceId);
                cmd.Parameters.AddWithValue("@docType", model.docType);
                cmd.Parameters.AddWithValue("@filePath", model.filePath);
                cmd.Parameters.AddWithValue("@fileName", model.fileName);
                cmd.Parameters.AddWithValue("@remarks", model.remarks);
                cmd.Parameters.AddWithValue("@formName", model.formName);
                cmd.Parameters.AddWithValue("@transKey", model.transKey);
                cmd.Parameters.AddWithValue("@updatedBy", model.updatedBy);
                cmd.Parameters.AddWithValue("@function", "SAVE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Common_AttachmentRef_DELETE(string key)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_common_attachmentRef_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@uniqueKey", key);
                cmd.Parameters.AddWithValue("@function", "DELETE");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
