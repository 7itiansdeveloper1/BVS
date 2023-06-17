using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_SectionMasterRepo : IAcademic_SectionMasterRepo
    {
        public List<Academic_SectionMasterModels> GetAcademic_SectionMasterList(string SecId)
        {
            List<Academic_SectionMasterModels> secMstList = new List<Academic_SectionMasterModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Section_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetSectionList");
                cmd.Parameters.AddWithValue("@SecId", SecId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                secMstList = ds.Tables[0].AsEnumerable().Select(r => new Academic_SectionMasterModels
                {
                    Active = r.Field<bool>("Active"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                    PrintOrder = r.Field<int>("PrintOrder"),
                    SecId = r.Field<string>("SecId"),
                    SecName = r.Field<string>("SecName"),
                }).ToList();
            }
            return secMstList;
        }
        public Academic_SectionMasterModels GetAcademic_SectionMasterById(string SecId)
        {
            return GetAcademic_SectionMasterList(SecId).FirstOrDefault();
        }

        public Tuple<int, string> Academic_SectionMaster_CRUD(Academic_SectionMasterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Section_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SecId", model.SecId);
                cmd.Parameters.AddWithValue("@SecName", model.SecName);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@Active", model.Active);
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
        public Tuple<int, string> Academic_SectionMaster_CRUD(string SecId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_Section_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SecId", SecId);
                cmd.Parameters.AddWithValue("@SecName", "");
                cmd.Parameters.AddWithValue("@PrintOrder", "");
                cmd.Parameters.AddWithValue("@Active", "");
                cmd.Parameters.AddWithValue("@UserId", "");
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public List<SelectListItem> getAllSectionByClass(string classId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_getAllSectionListByClassId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@classID", classId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SecName"),
                    Value = r.Field<string>("SecID"),
                }).ToList();
            }
        }

        public List<SelectListItem> getSectionByClass_USer(string classId, string userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetClassWiseSection", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClassID", classId);
                cmd.Parameters.AddWithValue("@UserId", classId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("SecName"),
                    Value = r.Field<string>("SecID"),
                }).ToList();
            }
        }
    }
}

