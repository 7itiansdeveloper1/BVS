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
    public class Academic_ClassSetupRepo : IAcademic_ClassSetupRepo
    {
        public List<Academic_ClassSetupModels> GetClassList(string WingName, string WingId, string ClassId)
        {
            List<Academic_ClassSetupModels> classList = new List<Academic_ClassSetupModels>();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ClassSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetWingClassList");
                cmd.Parameters.AddWithValue("@WingId", WingId);
                cmd.Parameters.AddWithValue("@ClassId", ClassId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                classList = ds.Tables[0].AsEnumerable().Select(r => new Academic_ClassSetupModels
                {
                    AdmOpenDate = r.Field<string>("AdmOpenDate"),
                    Active = r.Field<bool>("Active"),
                    AdmCloseDate = r.Field<string>("AdmCloseDate"),
                    AdmOpen = r.Field<bool>("AdmOpen"),
                    Classcode = r.Field<string>("Classcode"),
                    ClassId = r.Field<string>("ClassId"),
                    ClassName = r.Field<string>("ClassName"),

                    IsAlumniClass = r.Field<bool>("IsAlumniClass"),
                    IsDeletable = r.Field<bool>("IsDeletable"),
                    MaxStrength = r.Field<int>("MaxStrength"),
                    PrintOrder = r.Field<int>("PrintOrder"),

                    RegCloseDate = r.Field<string>("RegCloseDate"),
                    RegOpen = r.Field<bool>("RegOpen"),
                    RegOpenDate = r.Field<string>("RegOpenDate"),
                    PromotedClass = r.Field<string>("PromotedClass"),
                    WingId = WingId,
                    WingName = WingName,
                    SchoolId = r.Field<int>("SchoolId").ToString(),
                    SchoolName = r.Field<string>("SchoolName"),
                }).ToList();
            }
            return classList;
        }
        public Academic_ClassSetupModels GetClassById(string WingName, string WingId, string ClassId)
        {
            return GetClassList(WingName, WingId, ClassId).FirstOrDefault();
        }

        public Tuple<int, string> Academic_ClassSetup_CRUD(Academic_ClassSetupModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ClassSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassId", model.ClassId);
                cmd.Parameters.AddWithValue("@ClassName", model.ClassName);
                cmd.Parameters.AddWithValue("@Classcode", model.Classcode);
                cmd.Parameters.AddWithValue("@PrintOrder", model.PrintOrder);
                cmd.Parameters.AddWithValue("@MaxStrength", model.MaxStrength);
                cmd.Parameters.AddWithValue("@Active", model.Active);
                cmd.Parameters.AddWithValue("@IsAlumniClass", model.IsAlumniClass);
                cmd.Parameters.AddWithValue("@RegOpen", model.RegOpen);

                if (!string.IsNullOrEmpty(model.RegOpenDate))
                    cmd.Parameters.AddWithValue("@RegOpenDate", Convert.ToDateTime(model.RegOpenDate).Date);

                if (!string.IsNullOrEmpty(model.RegCloseDate))
                    cmd.Parameters.AddWithValue("@RegCloseDate", Convert.ToDateTime(model.RegCloseDate));
                cmd.Parameters.AddWithValue("@AdmOpen", model.AdmOpen);

                if (!string.IsNullOrEmpty(model.AdmOpenDate))
                    cmd.Parameters.AddWithValue("@AdmOpenDate", Convert.ToDateTime(model.AdmOpenDate));

                if (!string.IsNullOrEmpty(model.AdmCloseDate))
                    cmd.Parameters.AddWithValue("@AdmCloseDAte", Convert.ToDateTime(model.AdmCloseDate));

                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@CRUDMode", model.CRUDMode);

                cmd.Parameters.AddWithValue("@PromotedClass", model.PromotedClass);
                cmd.Parameters.AddWithValue("@WingID", model.WingId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        public Tuple<int, string> Academic_ClassSetup_CRUD(string ClassId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_ClassSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                cmd.Parameters.AddWithValue("@CRUDMode", "DELETE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }


        public List<SelectListItem> All_ClassList_DropDown()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.GetAllClassSection() order by ClassPO, SectionPO", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassID"),
                }).ToList();
            }
        }
        public List<SelectListItem> All_ClassWithSectionList_DropDown()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from dbo.GetAllClassSection() order by ClassPO, SectionPO", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("FullClassName"),
                    Value = r.Field<string>("ClassID") + r.Field<string>("SecID"),
                }).ToList();
            }
        }
        public List<SelectListItem> RegOpen_ClassList_DropDown()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct ClassId,ClassName,ClassPO from dbo.GetAllClassSection() where IsRegOpen=1 and Active=1 order by ClassPO", con);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassId"),
                }).ToList();
            }
        }

        public List<SelectListItem> All_ClassWithSectionList_DropDown(string userId)
        {
            List<SelectListItem> classSectionList = new List<SelectListItem>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataTable dt = new DataTable();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_GetClassDropDownList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@ListFor", "ClassSectionList");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                    classSectionList = dt.AsEnumerable().Select(r => new SelectListItem
                    {
                        Text = r.Field<string>("FullClassName"),
                        Value = r.Field<string>("ClassID") + r.Field<string>("SecID"),
                    }).ToList();
            }
            return classSectionList;
        }
        public List<SelectListItem> Board_ClassSecList_DropDown(string SessionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("School_BoardRegistration_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SessoinId", SessionId);
                cmd.Parameters.AddWithValue("@Mode", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ClassName"),
                    Value = r.Field<string>("ClassSectionId"),
                }).ToList();
            }
        }
    }
}
