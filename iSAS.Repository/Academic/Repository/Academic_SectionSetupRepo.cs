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
    public class Academic_SectionSetupRepo : IAcademic_SectionSetupRepo
    {
        public List<SelectListItem> GetTeacherList()
        {
            List<SelectListItem> teacherList = new List<SelectListItem>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ClassSectionSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "StaffList");
                cmd.Parameters.AddWithValue("@ClassId", "");
                cmd.Parameters.AddWithValue("@SectionId", "");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                teacherList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("StaffName"),
                    Value = r.Field<string>("StaffID"),
                }).ToList();
            }
            return teacherList;
        }

        public List<Academic_SectionSetupModels> GetSectionSetupDetailsList(int Class_Strength, string Class_Name, string Class_ClassId, string SectionId)
        {
            List<Academic_SectionSetupModels> sectionSetupDetailsList = new List<Academic_SectionSetupModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ClassSectionSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetClassSectionList");
                cmd.Parameters.AddWithValue("@ClassId", Class_ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                sectionSetupDetailsList = ds.Tables[0].AsEnumerable().Select(r => new Academic_SectionSetupModels
                {
                    Class = r.Field<string>("Class"),
                    ClassId = r.Field<string>("ClassId"),
                    CurrentStrength = r.Field<int>("CurrentStrength"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                    MaxStrength = r.Field<int>("MaxStrength"),
                    SectionId = r.Field<string>("SectionId"),

                    CT1 = r.Field<string>("CT1"),
                    CT2 = r.Field<string>("CT2"),
                    ClassTeacher = r.Field<string>("ClassTeacher"),

                    Class_Strength = Class_Strength,
                    Class_Name = Class_Name,
                    Class_ClassId = Class_ClassId
                }).ToList();
            }
            return sectionSetupDetailsList;
        }
        public Academic_SectionSetupModels GetSectionSetupDetailsById(int Class_Strength, string Class_Name, string Class_ClassId, string SectionId)
        {
            return GetSectionSetupDetailsList(Class_Strength, Class_Name, Class_ClassId, SectionId).FirstOrDefault();
        }

        public Tuple<int, string> Academic_SectionSetup_CRUD(Academic_SectionSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ClassSectionSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@SectionId", model.SectionId);
                cmd.Parameters.AddWithValue("@MaxStrength", model.MaxStrength);
                cmd.Parameters.AddWithValue("@CT1", model.CT1);
                cmd.Parameters.AddWithValue("@CT2", model.CT2);


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

        public Tuple<int, string> Academic_SectionSetup_CRUD(string ClassId, string SectionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ClassSectionSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@SectionId", SectionId);
                cmd.Parameters.AddWithValue("@MaxStrength", "");
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
    }
}
