using System.Linq;
using ISas.Entities.Academic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.Repository
{
   public class Academic_DocumentMappingRepo: IAcademic_DocumentMappingRepo
    {
        public List<SelectListItem> DocumentMapping_Transaction_DepartmentList()
        {
            List<SelectListItem> departmentList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_DocumentMapping_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DEptId", null);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                departmentList = dt.AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DeptName"),
                    Value = r.Field<string>("DeptID"),
                }).ToList();
                //model.DocumentList = ds.Tables[1].AsEnumerable().Select(r => new DocumentListModel
                //{
                //    DocumentId = r.Field<string>("DocNo"),
                //    DocumentName = r.Field<string>("DocName"),
                //    Selected = r.Field<bool>("IsSelected"),
                //}).ToList();
                return departmentList;
            }
        }

        public Academic_DocumentMappingModel DocumentMapping_Transaction_DocumentList(string deptId)
        {
            Academic_DocumentMappingModel model = new Academic_DocumentMappingModel();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_DocumentMapping_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DEptId", deptId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                model.DocumentList = dt.AsEnumerable().Select(r => new DocumentListModel
                {
                    DocumentId = r.Field<string>("DocNo"),
                    DocumentName = r.Field<string>("DocName"),
                    Selected = r.Field<bool>("IsSelected"),
                }).ToList();
                return model;
            }
        }

        public Tuple<int,string> DocumentMapping_CRUD(string departmentId, string documentsIds, string userId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_DocumentMapping_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                cmd.Parameters.AddWithValue("@DocumentIds", documentsIds);
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
        }
    }
}
