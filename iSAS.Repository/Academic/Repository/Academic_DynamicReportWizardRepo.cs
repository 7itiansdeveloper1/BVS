using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Academic.Repository
{
    public class Academic_DynamicReportWizardRepo : IAcademic_DynamicReportWizardRepo
    {
        public List<Academic_DynamicReportWizardModels> GetReportWizardList()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                List<Academic_DynamicReportWizardModels> reportWizardList = new List<Academic_DynamicReportWizardModels>();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_CreateDynamicQuery_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "FormLoad");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Academic_DynamicReportWizardModels
                {
                    ModuleId = r.Field<Int16>("ModuleId"),
                    ModuleName = r.Field<string>("ModuleName"),
                    ReportDisplayName = r.Field<string>("ReportDisplayName"),
                    IsActive = r.Field<bool>("IsActive"),
                    ReportId = r.Field<string>("ReportId"),
                    ReportName = r.Field<string>("ReportName"),
                    ReportStatus = r.Field<string>("ReportStatus"),
                    ReportType = r.Field<string>("ReportType"),
                }).ToList();
            }
        }
        public Academic_DynamicReportWizardModels NewReportData()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Academic_DynamicReportWizardModels model = new Academic_DynamicReportWizardModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_CreateDynamicQuery_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryFor", "GetListForNewReport");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model.ModuleList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ModuleName"),
                    Selected = r.Field<bool>("IsSelected"),
                    Value = r.Field<Int16>("ID").ToString(),
                }).ToList();

                model.ReportFeildList = ds.Tables[1].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DisplayFieldName"),
                    Value = r.Field<string>("DisplayFieldName"),
                }).ToList();

                if (model.ModuleList.Where(r => r.Selected).Count() == 1)
                    model.ModuleId = Convert.ToInt16(model.ModuleList.Where(r => r.Selected).FirstOrDefault().Value);

                return model;
            }
        }
        public List<SelectListItem> ReportFeildList(string ModuleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_CreateDynamicQuery_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "GetAdvanceFieldList");
                cmd.Parameters.AddWithValue("@ModuleId", ModuleId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DisplayFieldName"),
                    Value = r.Field<string>("DisplayFieldName"),
                }).ToList();
            }
        }
        public Academic_DynamicReportWizardModels GetReportDetailsbyId(string ReportId, string ModuleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                Academic_DynamicReportWizardModels model = new Academic_DynamicReportWizardModels();
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_CreateDynamicQuery_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@QueryFor", "EditReport");
                cmd.Parameters.AddWithValue("@ReportId", ReportId);
                cmd.Parameters.AddWithValue("@ModuleId", ModuleId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                model = ds.Tables[1].AsEnumerable().Select(r => new Academic_DynamicReportWizardModels
                {
                    ModuleId = r.Field<Int16>("ModuleId"),
                    ModuleName = r.Field<string>("ModuleName"),
                    ReportDisplayName = r.Field<string>("ReportDisplayName"),
                    IsActive = r.Field<bool>("IsActive"),
                    ReportId = r.Field<string>("ReportId"),
                    ReportName = r.Field<string>("ReportName"),
                    ReportCaption = r.Field<string>("ReportCaption"),
                    ReportStatus = r.Field<string>("ReportStatus"),
                    ReportType = r.Field<string>("ReportType"),
                }).FirstOrDefault();

                if (model == null)
                    model = new Academic_DynamicReportWizardModels();

                model.ModuleList = ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("ModuleName"),
                    Selected = r.Field<bool>("IsSelected"),
                    Value = r.Field<Int16>("ID").ToString(),
                }).ToList();

                model.ReportFeildList = ds.Tables[2].AsEnumerable().Select(r => new SelectListItem
                {
                    Text = r.Field<string>("DisplayFieldName"),
                    Value = r.Field<string>("DisplayFieldName"),
                }).ToList();
                return model;
            }
        }
        public Tuple<int, string> Academic_DynamicReportWizard_CRUD(Academic_DynamicReportWizardModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_School_CreateDynamicQueryForStudent_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReportName", model.ReportName);
                cmd.Parameters.AddWithValue("@ReportDisplayName", model.ReportDisplayName);
                cmd.Parameters.AddWithValue("@ModuleId", model.ModuleId);
                cmd.Parameters.AddWithValue("@ReportType", model.ReportType);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@ReportCaption", model.ReportCaption);
                cmd.Parameters.AddWithValue("@Fields", string.Join(",", model.ReportFeildList.Where(r => r.Selected).Select(r=> r.Value).ToList()));
                cmd.Parameters.AddWithValue("@ReportId", model.ReportId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
    }
}
