using ISas.Entities.Examination_Entities;
using ISas.Repository.ExaminationRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.Repository
{
    public class Examination_RemarksCenterRepo : IExamination_RemarksCenterRepo
    {
        #region Remarks Templete
        public List<Examination_RemarksTempleteModels> GetRemarksTempleteList(string TempleteId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarkTemplateSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Remark_TemplateId", TempleteId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Examination_RemarksTempleteModels
                {
                    Classes = r.Field<string>("Classes"),
                    TemplateId = r.Field<string>("TemplateId"),
                    IsAcheivementTemplate = r.Field<bool>("IsAcheivementTemplate"),
                    IsRemarkTemplate = r.Field<bool>("IsRemarkTemplate"),
                    TemplateName = r.Field<string>("TemplateName"),
                }).ToList();
            }
        }
        public Examination_RemarksTempleteModels GetRemarksTempleteById(string TempleId)
        {
            return GetRemarksTempleteList(TempleId).FirstOrDefault();
        }
        public Tuple<int, string> Examination_RemarksTemplete_CRUD(Examination_RemarksTempleteModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarkTemplateSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (!model.IsRemarkTemplate)
                    model.IsAcheivementTemplate = true;

                cmd.Parameters.AddWithValue("@Remark_TemplateId", model.TemplateId);
                cmd.Parameters.AddWithValue("@Remark_TemplateName", model.TemplateName);
                cmd.Parameters.AddWithValue("@IsRemarkTemplate", model.IsRemarkTemplate);
                cmd.Parameters.AddWithValue("@IsAcheivementTemplate", model.IsAcheivementTemplate);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion

        #region Class Setup (Remarks Templete)
        public List<SelectListItem> GetRemarksTempleteClassList(string TempleteId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarkTemplateClassSetup_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RemarkTemplateId", TempleteId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new SelectListItem
                {
                     Selected = r.Field<bool>("IsSelected"),
                     Text = r.Field<string>("ClassName"),
                     Value = r.Field<string>("ClassSectionId"),
                }).ToList();
            }
        }
        public Tuple<int, string> RemarksTempleteClass_CRUD(Examination_TempleteClassSetupModel model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarkTemplateClassSetup_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RemarkTemplateId", model.RemarksTempleteId);
                cmd.Parameters.AddWithValue("@ClassSectionId", string.Join(",", model.ClassList.Where(r=> r.Selected).Select(r=> r.Value).ToList()));
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@TransactionMode", "SAVE");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion

        #region Remarks Center
        public List<Examination_RemarksCenterModels> GetRemarksCenterList(string TempleteId, string RemarkId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarkCenter_Transaction", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RemarkTemplateId", TempleteId);
                cmd.Parameters.AddWithValue("@RemarkId", RemarkId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                return ds.Tables[0].AsEnumerable().Select(r => new Examination_RemarksCenterModels
                {
                    IsActive = r.Field<bool>("IsActive"),
                    IsDeleteable = r.Field<bool>("IsDeleteable"),
                    Remark = r.Field<string>("Remark"),
                    RemarkId = r.Field<string>("RemarkId"),
                }).ToList();
            }
        }
        public Examination_RemarksCenterModels GetRemarksCenterById(string TempleId, string RemarkId)
        {
            return GetRemarksCenterList(TempleId, RemarkId).FirstOrDefault();
        }
        public Tuple<int, string> Examination_RemarksCenter_CRUD(Examination_RemarksCenterModels model)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iSASDB"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExamNew_RemarksCenter_CRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RemarkId", model.RemarkId);
                cmd.Parameters.AddWithValue("@Remark", model.Remark);
                cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmd.Parameters.AddWithValue("@UserId", model.UserId);
                cmd.Parameters.AddWithValue("@TempleteId", model.TempleteId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                return new Tuple<int, string>(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString());
            }
        }
        #endregion
    }
}
